using System;
using System.Collections.Generic;
using System.ServiceModel;
using Recaster.Common;

namespace Recaster.Service
{
    [ServiceContract]
    public interface IWcfService
    {
        void Start();

        void Stop();

        [OperationContract]
        void StartEndpoint(EndpointType endpointType);        
        event Action EndpointStarted;

        [OperationContract]
        void StopEndpoint();
        event Action EndpointStopped;

        [OperationContract]
        void SetMulticastRcvSettings(List<MulticastGroupSettings> settings);

        [OperationContract]
        List<MulticastGroupSettings> GetMulticastRcvSettings();

        [OperationContract]
        void SetUnicastServerSettings(UnicastSettings settings);

        [OperationContract]
        UnicastSettings GetUnicastServerSettings();

        [OperationContract]
        void SetUnicastClientSettings(UnicastSettings settings);

        [OperationContract]
        UnicastSettings GetUnicastClientSettings();
    }
}
