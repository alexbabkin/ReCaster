using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

using Recaster.Multicast;
using Recaster.Unicast.Receiver;

namespace Recaster.MDistributor
{
    class MulticastDistributor : IMulticastDistributor
    {
        private async Task MSender()
        {
            var udpClient = new UdpClient(AddressFamily.InterNetworkV6);
            udpClient.Client.SetSocketOption(SocketOptionLevel.Socket,
                SocketOptionName.ReuseAddress, true);
            udpClient.Client.SetSocketOption(SocketOptionLevel.Socket,
                SocketOptionName.ExclusiveAddressUse, false);
            udpClient.Client.Bind(new IPEndPoint(IPAddress.IPv6Any, 55555));
            while (true)
            {
                var multicastMessage = await _tcpReceiver.GetMessage();
                await udpClient.SendAsync(multicastMessage.Buffer, 96, multicastMessage.MCastEndpoint);
            }
        }
        private ITcpReceiver _tcpReceiver;
        public MulticastDistributor(ITcpReceiver tcpReceiver)
        {
            _tcpReceiver = tcpReceiver;
        }
        public void Start()
        {
            Task rcvTask = _tcpReceiver.Start();
            var tasks = new Task[] { rcvTask, MSender() };
            Task.WaitAll(tasks);
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
