using System;
using System.Collections.Generic;
using Recaster.Common;
using Recaster.Client.RecasterService;

namespace Recaster.Client.SettingsProvider
{
    public class WCFProvider : IProvider, IDisposable
    {
        private WCFServiceClient senderProxy;
        private WCFServiceClient receiverProxy;

        public WCFProvider()
        {
            senderProxy = new WCFServiceClient("NetTcpBindingSender");
            receiverProxy = new WCFServiceClient("NetTcpBindingReceiver");
        }

        public List<MulticastGroupSettings> GetMulticastSourceSettings()
        {
            return receiverProxy.GetMulticastRcvSettings();
        }
        public void SetMulticastSourceSettings(List<MulticastGroupSettings> settings)
        {
            receiverProxy.SetMulticastRcvSettings(settings);
        }

        public UnicastSettings GetUnicastClientSettings()
        {
           return receiverProxy.GetUnicastClientSettings();
        }

        public void SetUnicastClientSettings(UnicastSettings settings)
        {
            receiverProxy.SetUnicastClientSettings(settings);
        }

        public UnicastSettings GetUnicastServerSettings()
        {
            return senderProxy.GetUnicastClientSettings();
        }
        
        public void SetUnicastServerSettings(UnicastSettings settings)
        {
            senderProxy.SetUnicastServerSettings(settings);
        }

        public void Dispose()
        {
            senderProxy.Close();
            receiverProxy.Close();
        }

        public void StartEndpoint(EndpointType endpointType)
        {
            switch (endpointType)
            {
                case EndpointType.MulitcastSender:
                    senderProxy.StartEndpoint(EndpointType.MulitcastSender);
                    break;
                case EndpointType.MulticastCatcher:
                    receiverProxy.StartEndpoint(EndpointType.MulticastCatcher);
                    break;
            }
        }

        public void StopEndpoint(EndpointType endpointType)
        {
            switch (endpointType)
            {
                case EndpointType.MulitcastSender:
                    senderProxy.StopEndpoint();
                    break;
                case EndpointType.MulticastCatcher:
                    receiverProxy.StopEndpoint();
                    break;
            }
        }
    }
}
