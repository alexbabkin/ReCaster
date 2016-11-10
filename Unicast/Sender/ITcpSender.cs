using System.Net;
using System.Threading.Tasks;
using Recaster.Multicast;

namespace Recaster.Unicast.Sender
{
    interface ITcpSender
    {
        bool Connect();
        void Disconnect();
        Task SendAsync(MulticastMessage message);
    }
}
