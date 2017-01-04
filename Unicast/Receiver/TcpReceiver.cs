using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks.Dataflow;
using log4net;
using Recaster.Multicast;
using Recaster.Utils;
using Recaster.Endpoint;
using Recaster.Configuration;

namespace Recaster.Unicast.Receiver
{
    public class TcpReceiver : IReceiver
    {
        private const int BufferSize = 4096;
        private const int ByteCountToReceive = 1024;

        private static readonly ILog Log = LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly TcpListener _listener;
        private readonly BufferBlock<MulticastMessage> _recvQueue;

        public TcpReceiver(IConfigManager config)
        {
            var ip = config.UnicastServerSettings.Ip;
            var port = config.UnicastServerSettings.Port;
            var endpoint = new IPEndPoint(IPAddress.Parse(ip), port);
            _listener = new TcpListener(endpoint);
            _recvQueue = new BufferBlock<MulticastMessage>();
            config.UnicastRcvSettingsChanged += SettigsChanged;
        }

        private void SettigsChanged(object sender, UnicastRcvSettingsEventArgs e)
        {
            Log.Debug("TcpReceiver: settings changed");
        }

        public async Task StartAsync(CancellationToken ct)
        {
            try
            {
                Log.Debug($"Starting tcp server on {_listener.LocalEndpoint}");
                _listener.Start();
            }
            catch (Exception ex)
            {
                Log.Error("Exception in TcpReceiver.Start", ex);
                return;
            }
            while (true)
            {
                ct.ThrowIfCancellationRequested();
                Log.Debug("waitting for connection");
                var client = await _listener.AcceptTcpClientAsync().WithCancellation(ct);
                Log.Info($"Client has connected: {client.Client.RemoteEndPoint}");
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
            var binaryFormatter = new BinaryFormatter();
            long msgLength = 0;
            int bufferOffset = 0;
            using (var stream = client.GetStream())
            {
                while (true)
                {
                    ct.ThrowIfCancellationRequested();
                    await Task.Yield();
                    var buffer = new byte[BufferSize];
                    Log.Info($"[Server] Reading from client: {client.Client.RemoteEndPoint}");
                    try
                    {
                        var byteCount = await stream.ReadAsync(buffer, bufferOffset, ByteCountToReceive, ct);
                        if (byteCount == 0)
                        {
                            Log.Info($"Client {client.Client.RemoteEndPoint} disconnected");
                            client.Close();
                            return;
                        }
                        bufferOffset += byteCount;
                        
                    }
                    catch (Exception ex)
                    {
                        Log.Info("Exception while reading data", ex);
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
                            Log.Error("Exception while deserializing object", ex);
                        }
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
            var msg = await _recvQueue.ReceiveAsync(ct);
            return msg;
        }
    }
}
