using System;
using System.Threading;
using System.Threading.Tasks;

namespace Recaster.Endpoint
{
    public class RecasterEndpoint : IEndpoint
    {
        private IReceiver _receiver;
        private ISender _sender;
        private CancellationTokenSource _cts;

        private async Task SendMessageAsync(CancellationToken ct)
        {
            while (true)
            {
                ct.ThrowIfCancellationRequested();
                var message = await _receiver.GetMessageAsync(ct);
                await _sender.SendAsync(message, ct);
            }
        }

        public RecasterEndpoint(IReceiver reveiver, ISender sender)
        {
            _cts = new CancellationTokenSource();
            _receiver = reveiver;
            _sender = sender;
        }

        public void Start()
        {
            var sendingTask = SendMessageAsync(_cts.Token);
            var receivingTask = _receiver.StartAsync(_cts.Token);
            Task[] tasks = new Task[] { receivingTask, sendingTask };
            try
            {
                Task.WaitAll(tasks);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Eexception: {0}", ex.ToString());
            }
            Console.ReadLine();
        }

        public void Stop()
        {
            _cts.Cancel();
            _receiver.Stop();
        }
    }
}
