using System.Threading.Tasks;

namespace Recaster.Endpoint
{
    public interface IEndpoint
    {
        Task StartAsync();
        void Start();
        void Stop();
    }
}
