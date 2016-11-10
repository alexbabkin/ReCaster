using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Recaster.Multicast;
using System.Threading.Tasks.Dataflow;
using Nito.AsyncEx;

namespace Recaster.Unicast.Receiver
{
    public class TcpReceiver: ITcpReceiver
    {
        private TcpListener _listener;
        private BufferBlock<MulticastMessage> _recvQueue;
        private AsyncLock _asyncMutex;

        public TcpReceiver(IPEndPoint endPoint)
        {
            _listener = new TcpListener(endPoint);
            _recvQueue = new BufferBlock<MulticastMessage>();
            _asyncMutex = new AsyncLock();
        }

        public async Task Start()
        {
            _listener.Start();
            while (true)
            {
                TcpClient client = await _listener.AcceptTcpClientAsync();
                Console.WriteLine("Client has connected");
                var task = StartConnectionAsync(client);
            }
        }

        private async Task StartConnectionAsync(TcpClient client)
        {
            var connectionTask = HandleConnectionAsync(client);
            try
            {
                await connectionTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private async Task HandleConnectionAsync(TcpClient client)
        {
            var ms = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            long msgLength = 0;
            int bufferOffset = 0;
            using (var stream = client.GetStream())
            {
                while (true)
                {
                    await Task.Yield();
                    var buffer = new byte[4096];
                    Console.WriteLine("[Server] Reading from client");
                    var byteCount = await stream.ReadAsync(buffer, bufferOffset, 1024);
                    bufferOffset += byteCount;
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
                            using (await _asyncMutex.LockAsync())
                                _recvQueue.Post(message);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
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
            throw new NotImplementedException();
        }

        public async Task<MulticastMessage> GetMessage()
        {
            MulticastMessage msg = null;
            using (await _asyncMutex.LockAsync())
                msg = await _recvQueue.ReceiveAsync();
            return msg;
        }
    }
}
