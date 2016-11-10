using Recaster.Multicast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recaster.Unicast.Receiver
{
    interface ITcpReceiver
    {
        Task Start();
        void Stop();
        Task<MulticastMessage> GetMessage();
    }
}
