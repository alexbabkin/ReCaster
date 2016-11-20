using System;
using log4net;
using System.Threading;
using System.Threading.Tasks;

namespace Recaster.Endpoint
{
    public class RecasterEndpoint : IEndpoint
    {
        private static readonly ILog log = LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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
                log.Error("Exception in endpoint", ex);  
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
