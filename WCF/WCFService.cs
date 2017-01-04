using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using log4net;
using Recaster.Configuration;
using Recaster.Common;
using Recaster.Service;


namespace Recaster.WCF
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single)]
    public class WcfService : IWcfService
    {
        private static readonly ILog Log = LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        ServiceHost _host;
        private readonly IConfigManager _config;

        public WcfService(IConfigManager config)
        {
            _config = config;
        }

        public List<MulticastGroupSettings> GetMulticastRcvSettings()
        {
            return _config.MCastRecvSettings;
        }

        public void SetMulticastRcvSettings(List<MulticastGroupSettings> settings)
        {
            _config.ApplyMulticastRcvSettings(settings);
        }

        public void SetUnicastServerSettings(UnicastSettings settings)
        {
            _config.ApplyUnicastRcvSettings(settings);
        }

        public UnicastSettings GetUnicastServerSettings()
        {
            return _config.UnicastServerSettings;
        }

        public void SetUnicastClientSettings(UnicastSettings settings)
        {
            _config.ApplyUnicastSndSettings(settings);
        }

        public UnicastSettings GetUnicastClientSettings()
        {
            return _config.UnicastClientSettings;
        }

        public void StartEndpoint(EndpointType endpointType)
        {
            _config.AppType = endpointType;
            OnEndpointStarted();
        }

        public event Action EndpointStarted;

        protected void OnEndpointStarted()
        {
            Volatile.Read(ref EndpointStarted)?.Invoke();
        }

        public void StopEndpoint()
        {
            OnEndpointStopped();
        }

        public event Action EndpointStopped;

        protected void OnEndpointStopped()
        {
            Volatile.Read(ref EndpointStopped)?.Invoke();
        }

        public void Start()
        {
            Log.Debug("starting WCF service");
            _host = new ServiceHost(this);
            _host.Open();
            Log.Debug($"WCF service started on {_host.Description.Endpoints[0].Address}");
        }        
        public void Stop()
        {
            Log.Debug($"stopping WCF service {_host.Description.Endpoints[0].Address}");
            _host.Close();
            Log.Debug("WCF service stopped");
        }
    }
}
