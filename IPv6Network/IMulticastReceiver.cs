using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Recaster.IPv6Network
{
    interface IMulticastReceiver
    {
        Task Start();
        void Stop();
        void SetSourceQualifier(ISourceQualifier sourceQualifier);

        event EventHandler<MulticastMsgEventArgs> MessageReceived;
    }

    public class MulticastMsgEventArgs : EventArgs
    {
        private IPEndPoint _remoteEndpoint;
        private byte[] _data;

        public MulticastMsgEventArgs(IPEndPoint remoteEndpoint, byte[] data)
        {
            _data = data;
            _remoteEndpoint = remoteEndpoint;
        }

        public IPEndPoint RemoteEndpoint { get { return _remoteEndpoint; } }
        public byte[] Data { get { return _data; } }
    }
}
