using System;
using System.Threading.Tasks;
using System.Threading;
using Recaster.Multicast.Sender;
using Recaster.Unicast.Receiver;

namespace Recaster.Endpoint
{
    class MulticastDistributor : TaskAggregator
    {
        public MulticastDistributor(IMulticastSender multicastSender, ITcpReceiver tcpReceiver)
            :base(tcpReceiver, multicastSender)
        {
            
        }
    }
}
