using System.Threading;
using Recaster.Dependency;
using Recaster.Endpoint;
using Recaster.Service;
using System.ServiceModel;
using Recaster.WCF;

namespace Recaster
{
    class Program
    {
        private static IEndpoint _endpoint;
        private static AutoResetEvent exitSignal = new AutoResetEvent(false);

        static void Main(string[] args)
        {
            IWCFService  wcfService = DependencyResolver.Get<IWCFService>();
           // ServiceHost host = new ServiceHost(typeof(WCFService));
            wcfService.EndpointStarted += StartEdpoint;
            wcfService.EndpointStopped += StopEndpoint;
            //host.Open();
            wcfService.Start();
            //_endpoint = DependencyResolver.Get<IEndpoint>();
            //_endpoint.Start();
            exitSignal.WaitOne();
            //host.Close();
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
