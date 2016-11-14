using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using Recaster.Multicast.Receiver;
using Recaster.Multicast.Receiver.SourceQualifier;
using Recaster.Unicast.Sender;

namespace Recaster.Endpoint
{
    class MulticastCatcher : TaskAggregator
    {
        private IMulticastReceiveManager _multicastManager;
        public MulticastCatcher(IMulticastReceiveManager multicastManager, ITcpSender tcpSender)
            :base(multicastManager, tcpSender)
        {
            _multicastManager = multicastManager;
            tcpSender.Connect();

            IMulticastReceiver udpListener1 = new MulticastReceiver(57125,
               IPAddress.Parse("ff3e::ffff:ff01"));
            udpListener1.SetSourceQualifier(
                new SourceQualifier(IPAddress.Parse("::1"), QualifierOption.Discard));
            IMulticastReceiver udpListener2 = new MulticastReceiver(57325,
                IPAddress.Parse("ff3e::ffff:ff01"));
            udpListener1.SetSourceQualifier(
                new SourceQualifier(IPAddress.Parse("::1"), QualifierOption.Discard));
            _multicastManager.AddReceiver(udpListener2);
            _multicastManager.AddReceiver(udpListener1);
        }
    }
}
