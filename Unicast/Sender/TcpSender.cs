using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Recaster.Multicast;
using Recaster.Endpoint;
using Recaster.Configuration;

namespace Recaster.Unicast.Sender
{
    public class TcpSender : ISender, IDisposable
    {
        private TcpClient _client;
        private NetworkStream _stream;
        private IPEndPoint _endpoint;

        public TcpSender(IConfigManager config)
        {
            var ip = config.UnicastSndSettings.IP;
            var port = config.UnicastSndSettings.Port;
            _endpoint = new IPEndPoint(IPAddress.Parse(ip), port);
        }

        private async Task<bool> Connect()
        {
            try
            {
                await _client.ConnectAsync(_endpoint.Address, _endpoint.Port);
                _stream = _client.GetStream();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception rised in Connect: {0}", ex.ToString());
                return false;
            }
            return _client.Connected;
        }

        private void Disconnect()
        {
            if (_client != null)
                _client.Close();
            if (_stream != null)
                _stream.Close();
        }

        public async Task SendAsync(MulticastMessage message, CancellationToken ct)
        {
            if (_client == null)
                _client = new TcpClient();
            if (!_client.Connected)
                if (!await Connect())
                    return;
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
                if (!_client.Connected)
                    Disconnect();
            }
        }

        public void Dispose()
        {
            Disconnect();
        }
    }
}
