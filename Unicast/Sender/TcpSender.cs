using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using log4net;
using Recaster.Multicast;
using Recaster.Endpoint;
using Recaster.Configuration;

namespace Recaster.Unicast.Sender
{
    public class TcpSender : ISender, IDisposable
    {
        private static readonly ILog log = LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private TcpClient _client;
        private NetworkStream _stream;
        private IPEndPoint _endpoint;

        public TcpSender(IConfigManager config)
        {
            var settigs = config.UnicastClientSettings;
            var ip = config.UnicastClientSettings.IP;
            var port = config.UnicastClientSettings.Port;
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
                log.Error("Exception rised in Connect", ex);
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
                log.Error("Exception rised in SendAsync", ex);
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
