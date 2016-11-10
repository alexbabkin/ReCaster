using System;
using System.Threading.Tasks;
using Recaster.Multicast.Sender;
using Recaster.Unicast.Receiver;

namespace Recaster.MDistributor
{
    class MulticastDistributor : IMulticastDistributor
    {
        private ITcpReceiver _tcpReceiver;
        private IMulticastSender _multicastSender;

        private async Task MSender()
        {
            while (true)
            {
                var multicastMessage = await _tcpReceiver.GetMessage();
                await _multicastSender.SendAsync(multicastMessage.Buffer, multicastMessage.MCastEndpoint);
            }
        }
        public MulticastDistributor(IMulticastSender multicastSender, ITcpReceiver tcpReceiver)
        {
            _tcpReceiver = tcpReceiver;
            _multicastSender = multicastSender;
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
