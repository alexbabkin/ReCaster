using System;
using System.Threading;
using System.Linq;
using Recaster.Dependency;
using Recaster.Endpoint;
using Recaster.Service;
using log4net;
using System.ServiceModel;
using Recaster.WCF;
using Recaster.Common;

namespace Recaster
{
    class Program
    {

        private static ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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
            if (args.Contains("-s"))
            {
                try
                {
                    _endpoint = DependencyResolver.Get<IEndpoint>();
                    _endpoint.Start();
                }
                catch (Exception ex)
                {                    
                    log.Error("Exception ", ex);
                    StopEndpoint();
                }
            }
            exitSignal.WaitOne();
            //host.Close();
            wcfService.Stop();
        }

        private static async void StartEdpoint()
        {
            if (_endpoint != null)
            {
                log.Info("Endpoint is running. Stopping and starting again");
                StopEndpoint();
            }
            _endpoint = DependencyResolver.Get<IEndpoint>();
            try
            {
                await _endpoint.StartAsync();
            }
            catch (Exception ex)
            {
                log.Error("Exception ", ex);
                (_endpoint as IDisposable).Dispose();
                _endpoint = null;
            }           
        }

        private static void StopEndpoint()
        {
            _endpoint.Stop();
            (_endpoint as IDisposable).Dispose();
            _endpoint = null;
        }
    }
}
