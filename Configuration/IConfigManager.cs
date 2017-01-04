using System;
using System.Collections.Generic;
using Recaster.Common;
namespace Recaster.Configuration
{
    public interface IConfigManager
    {
        EndpointType AppType { get; set; }

        List<MulticastGroupSettings> MCastRecvSettings { get; }

        UnicastSettings UnicastServerSettings { get;}

        UnicastSettings UnicastClientSettings { get;}

        void ApplyMulticastRcvSettings(List<MulticastGroupSettings> newSettings);

        event EventHandler<MulticastRcvSettingsEventArgs> MulticastRcvSettingsChanged;

        void ApplyUnicastRcvSettings(UnicastSettings newSettings);

        event EventHandler<UnicastRcvSettingsEventArgs> UnicastRcvSettingsChanged;

        void ApplyUnicastSndSettings(UnicastSettings newSettings);

        event EventHandler<UnicastSndSettingsEventArgs> UnicastSndeSettingsChanged;
    }

    public class MulticastRcvSettingsEventArgs : EventArgs
    {
        public MulticastRcvSettingsEventArgs(List<MulticastGroupSettings> mcastGroups)
        {
            MCastGroups = mcastGroups;
        }
        public List<MulticastGroupSettings> MCastGroups { get; }
    }

    public class UnicastRcvSettingsEventArgs : EventArgs
    {
        public UnicastRcvSettingsEventArgs(UnicastSettings ucastRcvSettings)
        {
            UCastRcvSettings = ucastRcvSettings;
        }
        public UnicastSettings UCastRcvSettings { get; }
    }

    public class UnicastSndSettingsEventArgs : EventArgs
    {
        public UnicastSndSettingsEventArgs(UnicastSettings ucastSndSettings)
        {
            UCastRcvSettings = ucastSndSettings;
        }
        public UnicastSettings UCastRcvSettings { get; }
    }
}
