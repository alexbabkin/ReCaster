using System;
using System.Net;

namespace Recaster.Multicast
{
    [Serializable]
    public class MulticastMessage
    {
        public MulticastMessage()
        {
            
        }
        public MulticastMessage(byte[] buffer, IPEndPoint mcastEndpoint)
        {
            Buffer = buffer;
            MCastEndpoint = mcastEndpoint;

        }
        public byte[] Buffer { get; set; }
        public IPEndPoint MCastEndpoint { get; set; }
    }
}
