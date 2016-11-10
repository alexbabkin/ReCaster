using System;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using Recaster.Multicast;
using System.IO;

namespace Recaster.Unicast.Sender
{
    public class TcpSender : ITcpSender
    {
        private TcpClient _client;
        private NetworkStream _stream;

        public bool Connect(IPEndPoint endpoint)
        {
            try
            {
                _client = new TcpClient();
                _client.Connect(endpoint);
                _stream = _client.GetStream();
            }
            catch (Exception ex)
            {
                return false;
            }
            return _client.Connected;
        }

        public void Disconnect()
        {
            if (_client != null)
                _client.Close();
            if (_stream != null)
                _stream.Dispose();
        }

        public async Task SendAsync(MulticastMessage message)
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
    }
}
