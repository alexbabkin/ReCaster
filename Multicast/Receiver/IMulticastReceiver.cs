using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

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
        public MulticastMsgEventArgs(IPEndPoint mcastEndpoint,
            IPEndPoint remoteEndpoint, byte[] data)
        {
            Data = data;
            RemoteEndpoint = remoteEndpoint;
            MCastEndpoint = mcastEndpoint;
        }

        public IPEndPoint RemoteEndpoint { get; }
        public IPEndPoint MCastEndpoint { get; }
        public byte[] Data { get; }
    }
}
