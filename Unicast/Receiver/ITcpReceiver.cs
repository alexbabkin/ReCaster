using System.Threading.Tasks;
using System.Threading;
using Recaster.Multicast;
using Recaster.Endpoint;

namespace Recaster.Unicast.Receiver
{
    interface ITcpReceiver : IReceiver
    {
        new Task StartAsync(CancellationToken ct);
        new void Stop();
        new Task<MulticastMessage> GetMessageAsync(CancellationToken ct);
    }
}
