using System.Collections.Generic;
using Recaster.Common;

namespace Recaster.Client.SettingsProvider
{
    public interface IProvider
    {
        UnicastSettings GetUnicastServerSettings();
        void SetUnicastServerSettings(UnicastSettings settings);

        List<MulticastGroupSettings> GetMulticastSourceSettings();
        void SetMulticastSourceSettings(List<MulticastGroupSettings> settings);

        UnicastSettings GetUnicastClientSettings();
        void SetUnicastClientSettings(UnicastSettings settings);

        void StartEndpoint(EndpointType endpointType);
        void StopEndpoint(EndpointType endpointType);
    }
}
