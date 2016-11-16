using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Recaster.Multicast.Receiver.SourceQualifier;
using Newtonsoft.Json;

namespace Recaster.Configuration
{
    public class QualifierSettings
    {
        public string sourceIP { get; set; } = String.Empty;
        public int Port { get; set; } = 0;
        public bool Disacard { get; set; }
    }
    public class MulticastGroupSettings
    {
        public string Name;
        public string GroupAdreass { get; set; } = String.Empty;
        public int Port { get; set; } = 0;
        public QualifierSettings Qualifier { get; set; }
    }

    public class MulticastReceiverSetting
    {
        private void Load()
        {
            /* var q = new QualifierSettings() { sourceIP = "::1", Disacard = true};
             var mg = new MulticastGroupSettings()
             {
                 GroupAdreass = "ff3e::ffff:ff01",
                 Port = 57125,
                 Qualifier = q
             };
             var lst = new List<MulticastGroupSettings>();
             lst.Add(mg);
             Properties.Settings.Default.MulticastReceiverSettings = JsonConvert.SerializeObject(lst);
             Properties.Settings.Default.Save();*/

            string serializedSettings = Properties.Settings.Default.MulticastReceiverSettings;
            _receivers = JsonConvert.DeserializeObject<List<MulticastGroupSettings>>(serializedSettings);
        }
        public List<MulticastGroupSettings> _receivers { get; private set; }
        public MulticastReceiverSetting()
        {
            Load();
        }        
    }
}
