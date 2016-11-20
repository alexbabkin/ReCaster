using System;
using System.Collections.Generic;
using System.ServiceModel;
using Recaster.Common;

namespace Recaster.Service
{
    [ServiceContract]
    public interface IWCFService
    {
        void Start();

        void Stop();

        [OperationContract]
        void StartEndpoint();        
        event Action EndpointStarted;

        [OperationContract]
        void StopEndpoint();
        event Action EndpointStopped;

        [OperationContract]
        void SetMulticastRcvSettings(List<MulticastGroupSettings> settings);

        [OperationContract]
        List<MulticastGroupSettings> GetMulticastRcvSettings();

    }
}
