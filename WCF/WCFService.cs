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
    public class WCFService : IWCFService
    {
        private static readonly ILog log = LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        ServiceHost _host;
        private IConfigManager _config;

        public WCFService(IConfigManager config)
        {
            _config = config;
        }

        public List<MulticastGroupSettings> GetMulticastRcvSettings()
        {
            return _config.MCastRecvSettings;
        }

        public void SetMulticastRcvSettings(List<MulticastGroupSettings> settings)
        {
            throw new NotImplementedException();
        }

        public void StartEndpoint()
        {
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
            log.Debug("starting WCF service");
            _host = new ServiceHost(this);
            _host.Open();
            log.Debug($"WCF service started on {_host.Description.Endpoints[0].Address}");
        }        
        public void Stop()
        {
            log.Debug($"stopping WCF service {_host.Description.Endpoints[0].Address}");
            _host.Close();
            log.Debug("WCF service stopped");
        }
    }
}
