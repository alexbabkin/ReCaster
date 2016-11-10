using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Recaster.Multicast.Receiver
{
    class MulticastReceiveManager : IMulticastReceiveManager
    {
        private List<IMulticastReceiver> _receivers;
        private BufferBlock<MulticastMessage> _mcastQueue;

        private void MessageReceived(object sender, MulticastMsgEventArgs mMsg)
        {
            Console.WriteLine("Receved message from {0}:{1}. Message length is {2}",
                mMsg.RemoteEndpoint.Address, mMsg.RemoteEndpoint.Port, mMsg.Data.Length);
            var message = new MulticastMessage(mMsg.Data, mMsg.MCastEndpoint);
            _mcastQueue.Post(message); 
        }

        public MulticastReceiveManager()
        {
            _receivers = new List<IMulticastReceiver>();
            _mcastQueue = new BufferBlock<MulticastMessage>();
        }

        public void AddReceiver(IMulticastReceiver receiver)
        {
            receiver.MessageReceived += MessageReceived;
            _receivers.Add(receiver);
        }

        public async Task<MulticastMessage> GetMulticastMessageAsync(CancellationToken ct)
        {
            var message = await _mcastQueue.ReceiveAsync(ct);
            return message;
        }

        public async Task StartReceiversAsync(CancellationToken ct)
        {
            Task[] tasks = new Task[_receivers.Count];
            for (int i = 0; i < _receivers.Count; i++)
            {
                tasks[i] = _receivers[i].StartAsync(ct);
            }
            await Task.WhenAll(tasks);
        }

        public void StopReceivers()
        {
            foreach (var rcv in _receivers)
                rcv.MessageReceived -= MessageReceived;
        }
    }
}
