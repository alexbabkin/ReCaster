using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Recaster.IPv6Network
{
    class UdpListener
    {
        private IPAddress _mcastGroup;
        private int _localPort;

        public UdpListener(int port, IPAddress mcastGroup)
        {
            _mcastGroup = mcastGroup;
            _localPort = port;
        }

        public async Task Starter()
        {
            using (var udpClient = new UdpClient(_localPort, AddressFamily.InterNetworkV6))
            {
                udpClient.JoinMulticastGroup(11, _mcastGroup);
                while (true)
                {
                    UdpReceiveResult result = await udpClient.ReceiveAsync().ConfigureAwait(false);
                    HandleUdpdata(result);
                }
            }
            
        }

        private void HandleUdpdata(UdpReceiveResult result)
        {
            Console.WriteLine("Receved message from {0}:{1}. Message length is {2}", 
                result.RemoteEndPoint.Address, 
                result.RemoteEndPoint.Port,
                result.Buffer.Length);
        }

        private List<int> CollectNetworkInterfaceIndexes()
        {
            var result = new List<int>();

            NetworkInterface[] nics =
                NetworkInterface.GetAllNetworkInterfaces();

            if (nics.Length < 1)
                return result;

            foreach (NetworkInterface adapter in nics)
            {
                if (adapter.OperationalStatus == OperationalStatus.Up &&
                    adapter.Supports(NetworkInterfaceComponent.IPv6))
                {
                    IPv6InterfaceProperties properties =
                        adapter.GetIPProperties().GetIPv6Properties();
                    result.Add(properties.Index);
                }
            }
            return result;
        }
    }
}
