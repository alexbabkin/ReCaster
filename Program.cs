using System;
using System.Threading;
using System.Linq;
using Recaster.Dependency;
using Recaster.Endpoint;
using Recaster.Service;
using log4net;

namespace Recaster
{
    class Program
    {

        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static IEndpoint _endpoint;
        private static readonly AutoResetEvent ExitSignal = new AutoResetEvent(false);

        static void Main(string[] args)
        {
             IWcfService  wcfService = DependencyResolver.Get<IWcfService>();
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
                    Log.Error("Exception ", ex);
                    StopEndpoint();
                }
            }
            ExitSignal.WaitOne();
            //host.Close();
            wcfService.Stop();
        }

        private static async void StartEdpoint()
        {
            if (_endpoint != null)
            {
                Log.Info("Endpoint is running. Stopping and starting again");
                StopEndpoint();
            }
            _endpoint = DependencyResolver.Get<IEndpoint>();
            try
            {
                await _endpoint.StartAsync();
            }
            catch (Exception ex)
            {
                Log.Error("Exception ", ex);
                (_endpoint as IDisposable)?.Dispose();
                _endpoint = null;
            }           
        }

        private static void StopEndpoint()
        {
            _endpoint.Stop();
            (_endpoint as IDisposable)?.Dispose();
            _endpoint = null;
        }
    }
}
