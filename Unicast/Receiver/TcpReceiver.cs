﻿using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Recaster.Multicast;
using System.Threading.Tasks.Dataflow;
using Recaster.Utils;
using Recaster.Endpoint;
using Recaster.Configuration;

namespace Recaster.Unicast.Receiver
{
    public class TcpReceiver : IReceiver
    {
        private static readonly int BUFFER_SIZE = 4096;
        private static readonly int BYTE_COUNT_TO_RECEIVE = 1024;

        private TcpListener _listener;
        private BufferBlock<MulticastMessage> _recvQueue;

        public TcpReceiver(IConfigManager config)
        {
            var ip = config.UnicastRecvSettings.IP;
            var port = config.UnicastRecvSettings.Port;
            var endpoint = new IPEndPoint(IPAddress.Parse(ip), port);
            _listener = new TcpListener(endpoint);
            _recvQueue = new BufferBlock<MulticastMessage>();
        }

        public async Task StartAsync(CancellationToken ct)
        {
            try
            {
                _listener.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in TcpReceiver.Start: {0}", ex.ToString());
            }
            while (true)
            {
                ct.ThrowIfCancellationRequested();
                TcpClient client = await _listener.AcceptTcpClientAsync().WithCancellation(ct);
                Console.WriteLine("Client has connected: {0}", client.Client.RemoteEndPoint);
                var task = StartConnectionAsync(client, ct);
            }
        }

        private async Task StartConnectionAsync(TcpClient client, CancellationToken ct)
        {
            var connectionTask = HandleConnectionAsync(client, ct);
            try
            {
                await connectionTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private async Task HandleConnectionAsync(TcpClient client, CancellationToken ct)
        {
            var ms = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            long msgLength = 0;
            int bufferOffset = 0;
            using (var stream = client.GetStream())
            {
                while (true)
                {
                    ct.ThrowIfCancellationRequested();
                    await Task.Yield();
                    var buffer = new byte[BUFFER_SIZE];
                    Console.WriteLine("[Server] Reading from client: {0}", client.Client.RemoteEndPoint);
                    try
                    {
                        var byteCount = await stream.ReadAsync(buffer, bufferOffset, BYTE_COUNT_TO_RECEIVE, ct);
                        if (byteCount == 0)
                        {
                            Console.WriteLine("Client {0} disconnected", client.Client.RemoteEndPoint);
                            client.Close();
                            return;
                        }
                        bufferOffset += byteCount;
                        
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception while reading data: {0}", ex.ToString());
                        client.Close();
                        return;
                    }
                    
                    if (bufferOffset >= sizeof(long) && msgLength == 0)
                    {
                        msgLength = BitConverter.ToInt64(buffer, 0);
                    }                                    
                    if (msgLength > 0 && bufferOffset - sizeof(long) >= msgLength)
                    {
                        ms.Write(buffer, sizeof(long), (int)msgLength);
                        ms.Position = 0;
                        try
                        {
                            var message = binaryFormatter.Deserialize(ms) as MulticastMessage;
                            _recvQueue.Post(message);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Exception while deserializing the object: {0}", ex.ToString());
                        }
                        Console.WriteLine("Got a message");
                        Array.ConstrainedCopy(buffer, sizeof(long) + (int)msgLength, 
                            buffer, 0, 
                            bufferOffset - sizeof(long) - (int)msgLength);
                        bufferOffset -= sizeof(long) + (int)msgLength;
                        msgLength = 0;
                    }
                }               
            }
        }

        public void Stop()
        {
            _listener.Stop();
        }

        public async Task<MulticastMessage> GetMessageAsync(CancellationToken ct)
        {
            MulticastMessage msg = null;
            msg = await _recvQueue.ReceiveAsync();
            return msg;
        }
    }
}
