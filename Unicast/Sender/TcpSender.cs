using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Recaster.Multicast;


namespace Recaster.Unicast.Sender
{
    public class TcpSender : ITcpSender
    {
        private TcpClient _client;
        private NetworkStream _stream;
        private IPEndPoint _endpoint;

        public TcpSender(IPEndPoint endPoint)
        {
            _endpoint = endPoint;
        }

        public bool Connect()
        {
            try
            {
                _client = new TcpClient();
                _client.Connect(_endpoint.Address, _endpoint.Port);
                _stream = _client.GetStream();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception rised in Connect: {0}", ex.ToString());
                return false;
            }
            return _client.Connected;
        }

        public void Disconnect()
        {
            if (_client != null)
                _client.Close();
            if (_stream != null)
                _stream.Close();
        }

        public async Task SendAsync(MulticastMessage message, CancellationToken ct)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(ms, message);
                    byte[] msgLength = BitConverter.GetBytes(ms.Length);

                    _stream.Write(msgLength, 0, msgLength.Length);

                    ms.Position = 0;
                    ms.CopyTo(_stream);
                }
                await _stream.FlushAsync();
            }
            catch (Exception ex)
            {
                _stream.Position = 0;
                Console.WriteLine("Exception rised in SendAsync: {0}", ex.ToString());
            }
        }
    }
}
