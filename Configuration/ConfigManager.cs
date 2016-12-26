using System;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json;
using Recaster.Common;

namespace Recaster.Configuration
{
    public class ConfigManager : IConfigManager
    {
        public List<MulticastGroupSettings> MCastRecvSettings
        {
            get
            {
                var confString = Properties.Settings.Default.MulticastReceiverSettings;
                var settings = JsonConvert.DeserializeObject<List<MulticastGroupSettings>>(confString);
                if (settings == null)
                {
                    settings = new List<MulticastGroupSettings>();
                }
                return settings;
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

        public UnicastSettings UnicastServerSettings
        {
            get
            {
                var confString = Properties.Settings.Default.UnicastReceiverSettings;
                var settings = JsonConvert.DeserializeObject<UnicastSettings>(confString);
                if (settings == null)
                {
                    settings = new UnicastSettings();
                }
                return settings;
            }
        }

        public UnicastSettings UnicastClientSettings
        {
            get
            {
                var confString = Properties.Settings.Default.UnicastSenderSettings;
                var settings = JsonConvert.DeserializeObject<UnicastSettings>(confString);
                if (settings == null)
                {
                    settings = new UnicastSettings();
                }
                return settings;
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
            Properties.Settings.Default.Save();

            var e = new MulticastRcvSettingsEventArgs(newSettings);
            OnMulticastRcvSettingsChanged(e);
        }

        public void ApplyUnicastRcvSettings(UnicastSettings newSettings)
        {
            var confString = JsonConvert.SerializeObject(newSettings);
            Properties.Settings.Default.UnicastReceiverSettings = confString;
            Properties.Settings.Default.Save();

            var e = new UnicastRcvSettingsEventArgs(newSettings);
            OnUnicastRcvSettingsChanged(e);
        }

        public void ApplyUnicastSndSettings(UnicastSettings newSettings)
        {
            var confString = JsonConvert.SerializeObject(newSettings);
            Properties.Settings.Default.UnicastSenderSettings = confString;
            Properties.Settings.Default.Save();

            var e = new UnicastSndSettingsEventArgs(newSettings);
            OnUnicastSndeSettingsChanged(e);
        }
    }
}
