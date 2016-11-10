using System.Net;
using System.Threading.Tasks;
using Recaster.Multicast;

namespace Recaster.Unicast.Sender
{
    interface ITcpSender
    {
        bool Connect(IPEndPoint endpoint);
        void Disconnect();
        Task SendAsync(MulticastMessage message);
    }
}
