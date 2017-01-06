using System;
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
        public List<Setting> ChildSettings { get; }

        public string Title { get; }

        public SettingType Type { get; }

        public Setting(SettingType type)
        {
            ChildSettings = new List<Setting>();
            Type = type;
            switch (Type)
            {
                case SettingType.ReceiverSettings:
                    Title = "Receiver Settings";
                    break;
                case SettingType.UdpClientSettings:
                    Title = "Udp Client";
                    break;
                case SettingType.MulticastSourceSettings:
                    Title = "Multicast Sources";
                    break;
                case SettingType.SenderSettings:
                    Title = "Sender Settings";
                    break;
                case SettingType.UdpServerSettings:
                    Title = "Udp Server";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
