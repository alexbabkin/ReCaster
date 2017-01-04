using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Recaster.Utils;
using Recaster.Endpoint;
using Recaster.Configuration;
using System;

namespace Recaster.Multicast.Sender
{
    class MulticastSender : ISender, IDisposable
    {
        private UdpClient _sender;

        private void CreateSender()
        {
            _sender = new UdpClient(AddressFamily.InterNetworkV6);
            _sender.Client.SetSocketOption(SocketOptionLevel.Socket,
                SocketOptionName.ReuseAddress, true);
            _sender.Client.SetSocketOption(SocketOptionLevel.Socket,
                SocketOptionName.ExclusiveAddressUse, false);
        }

        public MulticastSender(IConfigManager config)
        {
            CreateSender();
            _sender.Client.Bind(new IPEndPoint(IPAddress.IPv6Any, 55555));              
        }

        public async Task SendAsync(MulticastMessage message, CancellationToken ct)
        {
            await _sender
                .SendAsync(message.Buffer, message.Buffer.Length, message.MCastEndpoint)
                .WithCancellation(ct);
        }

        public void Dispose()
        {
            if (_sender != null)
                _sender.Dispose();
        }
    }
}
