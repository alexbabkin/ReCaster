using System;
using System.Collections.Generic;
using Recaster.Common;
using Recaster.Client.RecasterService;

namespace Recaster.Client.SettingsProvider
{
    public class WcfProvider : IProvider, IDisposable
    {
        private readonly WcfServiceClient _senderProxy;
        private readonly WcfServiceClient _receiverProxy;

        public WcfProvider()
        {
            _senderProxy = new WcfServiceClient("NetTcpBindingSender");
            _receiverProxy = new WcfServiceClient("NetTcpBindingReceiver");
        }

        public List<MulticastGroupSettings> GetMulticastSourceSettings()
        {
            return _receiverProxy.GetMulticastRcvSettings();
        }
        public void SetMulticastSourceSettings(List<MulticastGroupSettings> settings)
        {
            _receiverProxy.SetMulticastRcvSettings(settings);
        }

        public UnicastSettings GetUnicastClientSettings()
        {
           return _receiverProxy.GetUnicastClientSettings();
        }

        public void SetUnicastClientSettings(UnicastSettings settings)
        {
            _receiverProxy.SetUnicastClientSettings(settings);
        }

        public UnicastSettings GetUnicastServerSettings()
        {
            return _senderProxy.GetUnicastClientSettings();
        }
        
        public void SetUnicastServerSettings(UnicastSettings settings)
        {
            _senderProxy.SetUnicastServerSettings(settings);
        }

        public void Dispose()
        {
            _senderProxy.Close();
            _receiverProxy.Close();
        }

        public void StartEndpoint(EndpointType endpointType)
        {
            switch (endpointType)
            {
                case EndpointType.MulitcastSender:
                    _senderProxy.StartEndpoint(EndpointType.MulitcastSender);
                    break;
                case EndpointType.MulticastCatcher:
                    _receiverProxy.StartEndpoint(EndpointType.MulticastCatcher);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(endpointType), endpointType, null);
            }
        }

        public void StopEndpoint(EndpointType endpointType)
        {
            switch (endpointType)
            {
                case EndpointType.MulitcastSender:
                    _senderProxy.StopEndpoint();
                    break;
                case EndpointType.MulticastCatcher:
                    _receiverProxy.StopEndpoint();
                    break;
            }
        }
    }
}
