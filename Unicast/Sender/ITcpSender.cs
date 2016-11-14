using System.Threading;
using System.Threading.Tasks;
using Recaster.Multicast;
using Recaster.Endpoint;

namespace Recaster.Unicast.Sender
{
    interface ITcpSender: ISender
    {
        bool Connect();
        void Disconnect();
        new Task SendAsync(MulticastMessage message, CancellationToken ct);
    }
}
