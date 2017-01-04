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
        private static readonly int BUFFER_SIZE = 4096;
        private static readonly int BYTE_COUNT_TO_RECEIVE = 1024;

        private static readonly ILog log = LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private TcpListener _listener;
        private BufferBlock<MulticastMessage> _recvQueue;

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
            log.Debug("TcpReceiver: settings changed");
        }

        public async Task StartAsync(CancellationToken ct)
        {
            try
            {
                log.Debug($"Starting tcp server on {_listener.LocalEndpoint}");
                _listener.Start();
            }
            catch (Exception ex)
            {
                log.Error("Exception in TcpReceiver.Start", ex);
                return;
            }
            while (true)
            {
                ct.ThrowIfCancellationRequested();
                log.Debug("waitting for connection");
                TcpClient client = await _listener.AcceptTcpClientAsync().WithCancellation(ct);
                log.Info($"Client has connected: {client.Client.RemoteEndPoint}");
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
                    log.Info($"[Server] Reading from client: {client.Client.RemoteEndPoint}");
                    try
                    {
                        var byteCount = await stream.ReadAsync(buffer, bufferOffset, BYTE_COUNT_TO_RECEIVE, ct);
                        if (byteCount == 0)
                        {
                            log.Info($"Client {client.Client.RemoteEndPoint} disconnected");
                            client.Close();
                            return;
                        }
                        bufferOffset += byteCount;
                        
                    }
                    catch (Exception ex)
                    {
                        log.Info("Exception while reading data", ex);
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
                            log.Error("Exception while deserializing object", ex);
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
            MulticastMessage msg = null;
            msg = await _recvQueue.ReceiveAsync();
            return msg;
        }
    }
}
