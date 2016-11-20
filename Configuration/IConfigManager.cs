using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recaster.Configuration
{
    public enum EndpointType
    {        
        MulticastCatcher,
        MulitcastReceivar
    }
    public interface IConfigManager
    {
        EndpointType AppType { get; set; }
        List<MulticastGroupSettings> MCastRecvSettings { get; }
        UnicastSettings UnicastRcvSettings { get;}
        UnicastSettings UnicastSndSettings { get;}
        void ApplyMulticastRcvSettings(List<MulticastGroupSettings> newSettings);
        event EventHandler<MulticastRcvSettingsEventArgs> MulticastRcvSettingsChanged;
        void ApplyUnicastRcvSettings(UnicastSettings newSettings);
        event EventHandler<UnicastRcvSettingsEventArgs> UnicastRcvSettingsChanged;
        void ApplyUnicastSndSettings(UnicastSettings newSettings);
        event EventHandler<UnicastSndSettingsEventArgs> UnicastSndeSettingsChanged;
    }

    public class MulticastRcvSettingsEventArgs : EventArgs
    {
        private readonly List<MulticastGroupSettings> _mcastGroups;
        public MulticastRcvSettingsEventArgs(List<MulticastGroupSettings> mcastGroups)
        {
            _mcastGroups = mcastGroups;
        }
        public List<MulticastGroupSettings> MCastGroups { get { return _mcastGroups; } }
    }

    public class UnicastRcvSettingsEventArgs : EventArgs
    {
        private readonly UnicastSettings _ucastRcvSettings;
        public UnicastRcvSettingsEventArgs(UnicastSettings ucastRcvSettings)
        {
            _ucastRcvSettings = ucastRcvSettings;
        }
        public UnicastSettings UCastRcvSettings { get { return _ucastRcvSettings; } }
    }

    public class UnicastSndSettingsEventArgs : EventArgs
    {
        private readonly UnicastSettings _ucastSndSettings;
        public UnicastSndSettingsEventArgs(UnicastSettings ucastSndSettings)
        {
            _ucastSndSettings = ucastSndSettings;
        }
        public UnicastSettings UCastRcvSettings { get { return _ucastSndSettings; } }
    }
}
