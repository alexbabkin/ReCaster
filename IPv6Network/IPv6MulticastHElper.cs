using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;

namespace Recaster.IPv6Network
{
    public static class IPv6MulticastHelper
    {
        public static Socket CreateMulticastSocket()
        {
            Socket socket;
            try
            {
                socket = new Socket(AddressFamily.InterNetworkV6,
                    SocketType.Dgram,
                    ProtocolType.Udp);
                socket.MulticastLoopback = true;
                socket.ExclusiveAddressUse = true;

                EndPoint multicastEndpoint = new IPEndPoint(IPAddress.IPv6Any, 57125);

            }
            catch (SocketException ex)
            {
                socket = null;
            }
            return socket;
        }
    }
}
