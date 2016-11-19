using System;
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
        private static readonly ILog log = LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private List<IMulticastReceiver> _receivers;
        private BufferBlock<MulticastMessage> _mcastQueue;

        private void MessageReceived(object sender, MulticastMsgEventArgs mMsg)
        {
            log.Info($"Receved message from {mMsg.RemoteEndpoint.Address}:{mMsg.RemoteEndpoint.Port}. Message length is {mMsg.Data.Length}");
            var message = new MulticastMessage(mMsg.Data, mMsg.MCastEndpoint);
            _mcastQueue.Post(message); 
        }

        public MulticastReceiveManager(IConfigManager config)
        {
            _receivers = new List<IMulticastReceiver>();
            foreach (var mgroupSetting in config.MCastRecvSettings)
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
