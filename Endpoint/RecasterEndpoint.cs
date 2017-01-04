using System;
using log4net;
using System.Threading;
using System.Threading.Tasks;

namespace Recaster.Endpoint
{
    public class RecasterEndpoint : IEndpoint, IDisposable
    {
        private static readonly ILog log = LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IReceiver _receiver;
        private readonly ISender _sender;
        private readonly CancellationTokenSource _cts;

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

        public async Task StartAsync()
        {
            var sendingTask = SendMessageAsync(_cts.Token);
            var receivingTask = _receiver.StartAsync(_cts.Token);
            Task[] tasks = new Task[] { receivingTask, sendingTask };
            try
            {
                log.Info("Starting Endpoint async...");
                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                log.Error("Exception in endpoint", ex);  
            }
            Console.ReadLine();
        }

        public void Start()
        {
            var sendingTask = SendMessageAsync(_cts.Token);
            var receivingTask = _receiver.StartAsync(_cts.Token);
            Task[] tasks = new Task[] { receivingTask, sendingTask };
            try
            {
                log.Info("Starting Endpoint...");
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

        public void Dispose()
        {
            (_sender as IDisposable)?.Dispose();
            (_receiver as IDisposable)?.Dispose();
            (_cts as IDisposable)?.Dispose();
        }
    }
}
