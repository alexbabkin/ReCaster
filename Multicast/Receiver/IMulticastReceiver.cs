using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Recaster.Multicast.Receiver.Qualifier;

namespace Recaster.Multicast.Receiver
{
    interface IMulticastReceiver
    {
        Task StartAsync(CancellationToken ct);
        void Stop();

        event EventHandler<MulticastMsgEventArgs> MessageReceived;
    }

    public class MulticastMsgEventArgs : EventArgs
    {
        private readonly IPEndPoint _remoteEndpoint;
        private readonly IPEndPoint _mcastEndpoint;
        private readonly byte[] _data;

        public MulticastMsgEventArgs(IPEndPoint mcastEndpoint,
            IPEndPoint remoteEndpoint, byte[] data)
        {
            _data = data;
            _remoteEndpoint = remoteEndpoint;
            _mcastEndpoint = mcastEndpoint;
        }

        public IPEndPoint RemoteEndpoint { get { return _remoteEndpoint; } }
        public IPEndPoint MCastEndpoint { get { return _mcastEndpoint; } }
        public byte[] Data { get { return _data; } }

    }
}
