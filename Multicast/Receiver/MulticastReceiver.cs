using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using Recaster.Multicast.Receiver.Qualifier;
using Recaster.Utils;
using Recaster.Common;

namespace Recaster.Multicast.Receiver
{
    internal class MulticastReceiver: IMulticastReceiver
    {
        private readonly IPAddress _mcastGroup;
        private readonly int _localPort;
        private readonly List<ISourceQualifier> _qualifiers;

        private static IEnumerable<int> CollectNetworkInterfaceIndexes()
        {
            var result = new List<int>();

            var nics = NetworkInterface.GetAllNetworkInterfaces();

            if (nics.Length < 1)
                return result;

            result.AddRange(from adapter in nics
                            where adapter.OperationalStatus == OperationalStatus.Up && 
                                  adapter.Supports(NetworkInterfaceComponent.IPv6)
                            select adapter.GetIPProperties().GetIPv6Properties() into properties
                            select properties.Index);
            return result;
        }

        public MulticastReceiver(MulticastGroupSettings settings)
        {
            _mcastGroup = IPAddress.Parse(settings.GroupAdreass);
            _localPort = settings.GroupPort;
            _qualifiers = new List<ISourceQualifier>();
            foreach(var q in settings.Qualifier)
            {
                var qOption = QualifierOption.Accept;
                if (q.Discard)
                    qOption = QualifierOption.Discard;

                IPAddress qIp = null;
                if (q.SourceIp != string.Empty)
                    qIp = IPAddress.Parse(q.SourceIp);

                var qualifier = new SourceQualifier(qIp, q.Port, qOption);
                _qualifiers.Add(qualifier);
            }
        }

        public async Task StartAsync(CancellationToken ct)
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
                        udpClient.JoinMulticastGroup(nicIndex, _mcastGroup);
                }
                while (true)
                {
                    ct.ThrowIfCancellationRequested();
                    var result = await udpClient.ReceiveAsync()
                        .WithCancellation(ct)
                        .ConfigureAwait(false);
                    if (_qualifiers.All(q => q.IsSourceQualified(result.RemoteEndPoint)))
                    {
                        var e = new MulticastMsgEventArgs(
                            new IPEndPoint(_mcastGroup, _localPort), result.RemoteEndPoint, result.Buffer);
                        OnMessageReceived(e);
                    }
                }
            }            
        }

        public void Stop()
        {            
            throw new NotImplementedException();
        }

        protected void OnMessageReceived(MulticastMsgEventArgs e)
        {
            Volatile.Read(ref MessageReceived)?.Invoke(this, e);
        }

        public event EventHandler<MulticastMsgEventArgs> MessageReceived;
    }
}
