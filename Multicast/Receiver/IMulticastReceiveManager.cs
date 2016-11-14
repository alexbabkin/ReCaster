using System.Threading;
using System.Threading.Tasks;
using Recaster.Endpoint;

namespace Recaster.Multicast.Receiver
{
    interface IMulticastReceiveManager: IReceiver
    {
        new Task StartAsync(CancellationToken ct);
        new void Stop();
        new Task<MulticastMessage> GetMessageAsync(CancellationToken ct);
        void AddReceiver(IMulticastReceiver receiver);
    }
}
