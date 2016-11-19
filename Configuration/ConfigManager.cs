using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Recaster.Configuration
{
    public class ConfigManager : IConfigManager
    {
        public List<MulticastGroupSettings> MCastRecvSettings
        {
            get
            {
                var confString = Properties.Settings.Default.MulticastReceiverSettings;
                return JsonConvert.DeserializeObject<List<MulticastGroupSettings>>(confString);
            }
        }

        public EndpointType AppType
        {
            get
            {
                return (EndpointType)Properties.Settings.Default.EndpointType;
            }

            set
            {
                Properties.Settings.Default.EndpointType = (byte)value;
            }
        }

        public UnicastSettings UnicastRecvSettings
        {
            get
            {
                var confString = Properties.Settings.Default.UnicastReceiverSettings;
                return JsonConvert.DeserializeObject<UnicastSettings>(confString);
            }
        }

        public UnicastSettings UnicastSndSettings
        {
            get
            {
                var confString = Properties.Settings.Default.UnicastSenderSettings;
                return JsonConvert.DeserializeObject<UnicastSettings>(confString);
            }
        }

        public event EventHandler<MulticastRcvSettingsEventArgs> MulticastRcvSettingsChanged;
        public event EventHandler<UnicastRcvSettingsEventArgs> UnicastRcvSettingsChanged;
        public event EventHandler<UnicastSndSettingsEventArgs> UnicastSndeSettingsChanged;

        protected void OnMulticastRcvSettingsChanged(MulticastRcvSettingsEventArgs e)
        {
            Volatile.Read(ref MulticastRcvSettingsChanged)?.Invoke(this, e);
        }

        protected void OnUnicastRcvSettingsChanged(UnicastRcvSettingsEventArgs e)
        {
            Volatile.Read(ref UnicastRcvSettingsChanged)?.Invoke(this, e);
        }

        protected void OnUnicastSndeSettingsChanged(UnicastSndSettingsEventArgs e)
        {
            Volatile.Read(ref UnicastSndeSettingsChanged)?.Invoke(this, e);
        }

        public void ApplyMulticastRcvSettings(List<MulticastGroupSettings> newSettings)
        {
            var confString = JsonConvert.SerializeObject(newSettings);
            Properties.Settings.Default.MulticastReceiverSettings = confString;

            var e = new MulticastRcvSettingsEventArgs(newSettings);
            OnMulticastRcvSettingsChanged(e);
        }

        public void ApplyUnicastRcvSettings(UnicastSettings newSettings)
        {
            var confString = JsonConvert.SerializeObject(newSettings);
            Properties.Settings.Default.UnicastReceiverSettings = confString;

            var e = new UnicastRcvSettingsEventArgs(newSettings);
            OnUnicastRcvSettingsChanged(e);
        }

        public void ApplyUnicastSndeSettings(UnicastSettings newSettings)
        {
            var confString = JsonConvert.SerializeObject(newSettings);
            Properties.Settings.Default.UnicastSenderSettings = confString;

            var e = new UnicastSndSettingsEventArgs(newSettings);
            OnUnicastSndeSettingsChanged(e);
        }
    }
}
