using System;
using System.Threading.Tasks;
using System.Threading;
using Recaster.Multicast.Sender;
using Recaster.Unicast.Receiver;

namespace Recaster.MDistributor
{
    class MulticastDistributor : IMulticastDistributor
    {
        private ITcpReceiver _tcpReceiver;
        private IMulticastSender _multicastSender;
        private CancellationTokenSource _cts;

        private async Task mSender(CancellationToken ct)
        {
            while (true)
            {
                ct.ThrowIfCancellationRequested();
                var multicastMessage = await _tcpReceiver.GetMessage();
                await _multicastSender.SendAsync(multicastMessage.Buffer, 
                    multicastMessage.MCastEndpoint, ct);
            }
        }
        public MulticastDistributor(IMulticastSender multicastSender, ITcpReceiver tcpReceiver)
        {
            _tcpReceiver = tcpReceiver;
            _multicastSender = multicastSender;
            _cts = new CancellationTokenSource();
        }
        public void Start()
        {
            Task receivingTask = _tcpReceiver.Start(_cts.Token);
            Task sendingTask = mSender(_cts.Token);
            var tasks = new Task[] { receivingTask, sendingTask };
            try
            {
                Task.WaitAll(tasks);
            }
            catch (Exception ex)
            {
                Console.WriteLine("MulticastDistributor exception: {0}", ex.ToString());
            }
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
