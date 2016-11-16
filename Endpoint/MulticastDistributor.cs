using System;
using System.Threading.Tasks;
using System.Threading;
using Recaster.Multicast.Sender;
using Recaster.Unicast.Receiver;
using Recaster.Configuration;

namespace Recaster.Endpoint
{
    class MulticastDistributor : TaskAggregator
    {
        public MulticastDistributor(IMulticastSender multicastSender, 
            ITcpReceiver tcpReceiver,
            IConfigManager configManager) :base(tcpReceiver, multicastSender)
        {
            
        }
    }
}
