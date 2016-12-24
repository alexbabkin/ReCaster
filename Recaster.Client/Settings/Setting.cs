using System.Collections.Generic;

namespace Recaster.Client.Settings
{
    public enum SettingType
    {
        ReceiverSettings,
        UdpClientSettings,
        MulticastSourceSettings,
        SenderSettings,
        UdpServerSettings
    }
    public class Setting
    {
        private readonly List<Setting> _childSettings;
        private readonly string _title;
        private readonly SettingType _type;

        public string Title { get { return _title; } }

        public SettingType Type { get { return _type; } }

        public List<Setting> ChildSettings { get { return _childSettings; } }

        public Setting(SettingType type)
        {
            _childSettings = new List<Setting>();
            _type = type;
            switch (_type)
            {
                case SettingType.ReceiverSettings:
                    _title = "Receiver Settings";
                    break;
                case SettingType.UdpClientSettings:
                    _title = "Udp Client";
                    break;
                case SettingType.MulticastSourceSettings:
                    _title = "Multicast Sources";
                    break;
                case SettingType.SenderSettings:
                    _title = "Sender Settings";
                    break;
                case SettingType.UdpServerSettings:
                    _title = "Udp Server";
                    break;
            }
        }


        public static Setting GetReceiverRoot()
        {
            Setting parent = new Setting(SettingType.ReceiverSettings);
            Setting child = new Setting(SettingType.UdpClientSettings);
            parent.ChildSettings.Add(child);
            child = new Setting(SettingType.MulticastSourceSettings);
            parent.ChildSettings.Add(child);
            return parent;
        }

        public static Setting GetSenderRoot()
        {
            Setting parent = new Setting(SettingType.SenderSettings);
            Setting child = new Setting(SettingType.UdpServerSettings);
            parent.ChildSettings.Add(child);
            return parent;
        }
    }
}
