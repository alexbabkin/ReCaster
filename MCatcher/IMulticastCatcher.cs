using System.Threading;

namespace Recaster.MCatcher
{
    interface IMulticastCatcher
    {
        void Start(CancellationToken ct);
        void Stop();
    }
}
