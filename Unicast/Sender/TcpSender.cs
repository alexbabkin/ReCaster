﻿using System;
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
        private static readonly ILog Log = LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private TcpClient _client;
        private NetworkStream _stream;
        private IPEndPoint _endpoint;

        public TcpSender(IConfigManager config)
        {
            config.UnicastSndeSettingsChanged += SettingsChanged;
            var ip = config.UnicastClientSettings.Ip;
            var port = config.UnicastClientSettings.Port;
            _endpoint = new IPEndPoint(IPAddress.Parse(ip), port);
        }

        private void SettingsChanged(object sender, UnicastSndSettingsEventArgs e)
        {
            var newIp = e.UCastRcvSettings.Ip;
            var newPort = e.UCastRcvSettings.Port;
            var newEndpoint = new IPEndPoint(IPAddress.Parse(newIp), newPort);
            if (!newEndpoint.Equals(_endpoint))
            {
                Log.Debug("TcpSender: Settings changed");
                Disconnect();
                _endpoint = newEndpoint;
            }
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
                Log.Error("Exception rised in Connect", ex);
                return false;
            }
            return _client.Connected;
        }

        private void Disconnect()
        {
            _client?.Close();
            _stream?.Close();
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
                using (var ms = new MemoryStream())
                {
                    var binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(ms, message);
                    byte[] msgLength = BitConverter.GetBytes(ms.Length);

                    _stream.Write(msgLength, 0, msgLength.Length);

                    ms.Position = 0;
                    ms.CopyTo(_stream);
                }
                await _stream.FlushAsync(ct);
            }
            catch (Exception ex)
            {
                Log.Error("Exception rised in SendAsync", ex);
                Disconnect();
            }
        }

        public void Dispose()
        {
            Disconnect();
        }
    }
}
