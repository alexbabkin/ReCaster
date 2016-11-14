using System.Threading;
using System.Threading.Tasks;
using Recaster.Multicast;

namespace Recaster.Endpoint
{
    public interface ISender
    {
        Task SendAsync(MulticastMessage message, CancellationToken ct);
    }
}
