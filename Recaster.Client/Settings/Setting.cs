using System.Collections.Generic;

namespace Recaster.Client.Settings
{
    public class Setting
    {
        private readonly List<Setting> _childSettings = new List<Setting>();
        public string Title { get; set; }
        public List<Setting> ChildSettings { get { return _childSettings; } }

        public static Setting GetReceiverRoot()
        {
            Setting parent = new Setting() { Title = "Receiver Settings" };
            Setting child = new Setting() { Title = "Tcp Client" };
            parent.ChildSettings.Add(child);
            child = new Setting() { Title = "Multicast Sources" };
            parent.ChildSettings.Add(child);
            return parent;
        }

        public static Setting GetSenderRoot()
        {
            Setting parent = new Setting() { Title = "Sender Settings" };
            Setting child = new Setting() { Title = "Tcp Server" };
            parent.ChildSettings.Add(child);
            return parent;
        }
    }
}
