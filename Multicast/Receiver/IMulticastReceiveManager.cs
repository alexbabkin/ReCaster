using System.Threading;
using System.Threading.Tasks;

namespace Recaster.Multicast.Receiver
{
    interface IMulticastReceiveManager
    {
        Task StartReceiversAsync(CancellationToken ct);
        void StopReceivers();
        void AddReceiver(IMulticastReceiver receiver);
        Task<MulticastMessage> GetMulticastMessageAsync(CancellationToken ct);
    }
}
