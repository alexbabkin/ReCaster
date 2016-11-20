using System.Threading;
using Recaster.Dependency;
using Recaster.Endpoint;
using Recaster.RemoteControl.WCF;

namespace Recaster
{
    class Program
    {
        private static IEndpoint _endpoint;
        private static AutoResetEvent exitSignal = new AutoResetEvent(false);

        static void Main(string[] args)
        {
            IWCFService  wcfService = DependencyResolver.Get<IWCFService>();
            wcfService.EndpointStarted += StartEdpoint;
            wcfService.EndpointStopped += StopEndpoint;
            wcfService.Start();
            _endpoint = DependencyResolver.Get<IEndpoint>();
            _endpoint.Start();
            exitSignal.WaitOne();
            wcfService.Stop();
        }

        private static void StartEdpoint()
        {
            _endpoint = DependencyResolver.Get<IEndpoint>();
            _endpoint.Start();
        }

        private static void StopEndpoint()
        {
            _endpoint.Stop();
            _endpoint = null;
        }


    }
}
