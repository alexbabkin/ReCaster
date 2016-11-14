using System.Threading;
using System.Threading.Tasks;
using Recaster.Multicast;

namespace Recaster.Endpoint
{
    public interface IReceiver
    {
        Task StartAsync(CancellationToken ct);
        void Stop();
        Task<MulticastMessage> GetMessageAsync(CancellationToken ct);
    }
}
