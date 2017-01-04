using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using log4net;
using Recaster.Endpoint;
using Recaster.Configuration;

namespace Recaster.Multicast.Receiver
{
    class MulticastReceiveManager : IReceiver
    {
        private static readonly ILog Log = LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly List<IMulticastReceiver> _receivers;
        private readonly BufferBlock<MulticastMessage> _mcastQueue;

        private void MessageReceived(object sender, MulticastMsgEventArgs mMsg)
        {
            Log.Info($"Receved message from {mMsg.RemoteEndpoint.Address}:{mMsg.RemoteEndpoint.Port}. Message length is {mMsg.Data.Length}");
            var message = new MulticastMessage(mMsg.Data, mMsg.MCastEndpoint);
            _mcastQueue.Post(message); 
        }

        private void SettingsChanged(object sender, MulticastRcvSettingsEventArgs e)
        {
            Log.Debug("MulticastReceiveManager: settings changed");
        }

        public MulticastReceiveManager(IConfigManager config)
        {
            _receivers = new List<IMulticastReceiver>();
            var settings = config.MCastRecvSettings;
            config.MulticastRcvSettingsChanged += SettingsChanged;
            foreach (var mgroupSetting in settings)
            {
                var receiver = new MulticastReceiver(mgroupSetting);
                receiver.MessageReceived += MessageReceived;
                _receivers.Add(receiver);
            }
            _mcastQueue = new BufferBlock<MulticastMessage>();
        }

        public async Task<MulticastMessage> GetMessageAsync(CancellationToken ct)
        {
            var message = await _mcastQueue.ReceiveAsync(ct);
            return message;
        }

        public async Task StartAsync(CancellationToken ct)
        {
            Task[] tasks = new Task[_receivers.Count];
            for (int i = 0; i < _receivers.Count; i++)
            {
                tasks[i] = _receivers[i].StartAsync(ct);
            }
            await Task.WhenAll(tasks);
        }

        public void Stop()
        {
            foreach (var rcv in _receivers)
                rcv.MessageReceived -= MessageReceived;
        }
    }
}
