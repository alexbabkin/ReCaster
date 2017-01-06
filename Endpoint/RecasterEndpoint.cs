using System;
using log4net;
using System.Threading;
using System.Threading.Tasks;

namespace Recaster.Endpoint
{
    /// <summary>
    /// RecasterEndpoint encapsulates root logic. 
    /// Asynchronously receives and resends messages.
    /// Depending on configuration, receiver and sender can either work
    /// with IPv6-multicasts or IPv4-unicasts
    /// </summary>
    public class RecasterEndpoint : IEndpoint, IDisposable
    {
        private static readonly ILog Log = LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// IReceiver catches multicast messages (primary or re-sended)
        /// </summary>
        private readonly IReceiver _receiver;
        /// <summary>
        /// ISender sends the messages either via IPv6-multicast or IPv4-unicast
        /// </summary>
        private readonly ISender _sender;
        /// <summary>
        /// CancellationToken to stop both sender and receiver
        /// </summary>
        private readonly CancellationTokenSource _cts;

        /// <summary>
        /// sends messages from receiver queue until cancellation
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        private async Task SendMessageAsync(CancellationToken ct)
        {
            while (true)
            {
                try
                {
                    ct.ThrowIfCancellationRequested();
                    var message = await _receiver.GetMessageAsync(ct);
                    await _sender.SendAsync(message, ct);
                }
                catch (OperationCanceledException ex)
                {
                    Log.Info($"Sending process cancelled: {ex}");
                    return;
                }
            }
        }

        /// <summary>
        /// prepare receiving and sending tasks to start
        /// </summary>
        /// <returns></returns>
        private Task[] PrepareTasks()
        {
            var sendingTask = SendMessageAsync(_cts.Token);
            var receivingTask = _receiver.StartAsync(_cts.Token);
            Task[] tasks = { receivingTask, sendingTask };
            return tasks;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="reveiver">IReceiver instance</param>
        /// <param name="sender">ISender insstance</param>
        public RecasterEndpoint(IReceiver reveiver, ISender sender)
        {
            _cts = new CancellationTokenSource();
            _receiver = reveiver;
            _sender = sender;
        }

        /// <summary>
        /// start tasks asynchronously
        /// </summary>
        /// <returns></returns>
        public async Task StartAsync()
        {
            var tasks = PrepareTasks();
            try
            {
                Log.Info("Starting Endpoint async...");
                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                Log.Error("Exception in endpoint", ex);  
            }
            Console.ReadLine();
        }

        /// <summary>
        /// start tasks synchronously (blocking call)
        /// </summary>
        public void Start()
        {
            var tasks = PrepareTasks();
            try
            {
                Log.Info("Starting Endpoint...");
                Task.WaitAll(tasks);
            }
            catch (Exception ex)
            {
                Log.Error("Exception in endpoint", ex);
            }
            Console.ReadLine();
        }

        /// <summary>
        /// Stop tasks
        /// </summary>
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
