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
    class MulticastReceiver: IMulticastReceiver
    {
        private IPAddress _mcastGroup;
        private int _localPort;
        private List<ISourceQualifier> _qualifier;

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

        public MulticastReceiver(int port, IPAddress mcastGroup)
        {
            _mcastGroup = mcastGroup;
            _localPort = port;
            _qualifier = new List<ISourceQualifier>();
        }

        public async Task Start()
        {
            using (var udpClient = new UdpClient(AddressFamily.InterNetworkV6))
            {
                udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, 
                    SocketOptionName.ReuseAddress, true);
                udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, 
                    SocketOptionName.ExclusiveAddressUse, false);
                udpClient.Client.Bind(new IPEndPoint(IPAddress.IPv6Any, _localPort));
                foreach (var nicIndex in CollectNetworkInterfaceIndexes())
                {
                    try
                    {
                        udpClient.JoinMulticastGroup(nicIndex, _mcastGroup);
                    }
                    catch
                    { }
                }
                while (true)
                {
                    UdpReceiveResult result = await udpClient.ReceiveAsync().ConfigureAwait(false);
                    if (_qualifier.All(q => q.IsSourceQualified(result.RemoteEndPoint)))
                    {
                        MulticastMsgEventArgs e = new MulticastMsgEventArgs(result.RemoteEndPoint, result.Buffer);
                        OnMessageReceived(e);
                    }
                }
            }            
        }

        public void Stop()
        {
            throw new NotFiniteNumberException();
        }

        public void SetSourceQualifier(ISourceQualifier sourceQualifier)
        {
            _qualifier.Add(sourceQualifier);
        }

        protected void OnMessageReceived(MulticastMsgEventArgs e)
        {
            Volatile.Read(ref MessageReceived)?.Invoke(this, e);
        }

        public event EventHandler<MulticastMsgEventArgs> MessageReceived;
    }
}
