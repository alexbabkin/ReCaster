using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using Recaster.Multicast.Receiver;
using Recaster.Multicast.Receiver.SourceQualifier;
using Recaster.Unicast.Sender;

namespace Recaster.MCatcher
{
    class MulticastCatcher : IMulticastCatcher
    {
        private IMulticastReceiveManager _multicastManager;
        private ITcpSender _tcpSender;
        private CancellationTokenSource _cts;
        public MulticastCatcher(IMulticastReceiveManager multicastManager, ITcpSender tcpSender)
        {
            _multicastManager = multicastManager;
            _tcpSender = tcpSender;
            _cts = new CancellationTokenSource();

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

        private async Task SendMessageAsync(CancellationToken ct)
        {
            while (true)
            {
                ct.ThrowIfCancellationRequested();
                var message = await _multicastManager.GetMulticastMessageAsync(ct);
                Console.WriteLine("Got message from the queue. Sending to {0}:{1}",
                        message.MCastEndpoint.Address, message.MCastEndpoint.Port);
                await _tcpSender.SendAsync(message);
            }
        }

        public void Start()
        {            
            _tcpSender.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 13001));
            var receivingTask = _multicastManager.StartReceiversAsync(_cts.Token);
            var sendingTask = SendMessageAsync(_cts.Token);
            Task[] tasks = new Task[] { receivingTask , sendingTask };
            try
            {
                Task.WaitAll(tasks);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
                       
            Console.WriteLine("receivers stopped");
            Console.ReadLine();
        }

        public void Stop()
        {
            _cts.Cancel();
            _multicastManager.StopReceivers();
        }
    }
}
