using System.Threading.Tasks;
using System.Threading;
using System.Net;
using Recaster.Multicast;
using Recaster.Endpoint;

namespace Recaster.Multicast.Sender
{
    interface IMulticastSender: ISender
    {
        new Task SendAsync(MulticastMessage message, CancellationToken ct);
    }
}
