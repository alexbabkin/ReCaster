using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Recaster.Utils;

namespace Recaster.Multicast.Sender
{
    class MulticastSender : IMulticastSender
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

        public MulticastSender(int port)
        {
            CreateSender();
            _sender.Client.Bind(new IPEndPoint(IPAddress.IPv6Any, port));              
        }

        public MulticastSender(IPAddress adress, int port)
        {
            CreateSender();
            _sender.Client.Bind(new IPEndPoint(adress, port));
        }

        public async Task SendAsync(byte[] data, IPEndPoint endPoint, CancellationToken ct)
        {
            await _sender.SendAsync(data, data.Length, endPoint).WithCancellation(ct);
        }
    }
}
