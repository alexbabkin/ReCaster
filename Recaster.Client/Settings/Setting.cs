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
        private readonly List<Setting> _childSettings = new List<Setting>();
        public SettingType Type { get; set; }
        public List<Setting> ChildSettings { get { return _childSettings; } }

        public static Setting GetReceiverRoot()
        {
            Setting parent = new Setting() { Type = SettingType.ReceiverSettings };
            Setting child = new Setting() { Type = SettingType.UdpClientSettings };
            parent.ChildSettings.Add(child);
            child = new Setting() { Type = SettingType.MulticastSourceSettings };
            parent.ChildSettings.Add(child);
            return parent;
        }

        public static Setting GetSenderRoot()
        {
            Setting parent = new Setting() { Type = SettingType.SenderSettings };
            Setting child = new Setting() { Type = SettingType.UdpServerSettings};
            parent.ChildSettings.Add(child);
            return parent;
        }
    }
}
