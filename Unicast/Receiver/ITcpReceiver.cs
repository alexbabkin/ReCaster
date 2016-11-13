using Recaster.Multicast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Recaster.Unicast.Receiver
{
    interface ITcpReceiver
    {
        Task Start(CancellationToken ct);
        void Stop();
        Task<MulticastMessage> GetMessage();
    }
}
