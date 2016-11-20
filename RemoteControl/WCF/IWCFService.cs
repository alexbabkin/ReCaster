using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Recaster.Configuration;

namespace Recaster.RemoteControl.WCF
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
