using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using Recaster.Multicast.Receiver;
using Recaster.Multicast.Receiver.SourceQualifier;
using Recaster.Unicast.Sender;
using Recaster.Configuration;

namespace Recaster.Endpoint
{
    class MulticastCatcher : TaskAggregator
    {
        private IMulticastReceiveManager _multicastManager;
        public MulticastCatcher(IMulticastReceiveManager multicastManager, 
            ITcpSender tcpSender,
            IConfigManager configManager) :base(multicastManager, tcpSender)
        {
            _multicastManager = multicastManager;
            tcpSender.Connect();            
        }
    }
}
