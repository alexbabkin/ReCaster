using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Recaster.Multicast.Sender
{
    interface IMulticastSender
    {
        Task SendAsync(byte[] data, IPEndPoint endPoint);        
    }
}
