using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Recaster.Client.Utility;
using Recaster.Client.Settings;

namespace Recaster.Client.ViewModels
{
    public class SettingsViewModel : ObservableElement
    {
        private readonly ReadOnlyCollection<SettingsViewModel> _childs;
        private readonly Setting _setting;
        private bool _isSelected;
        private bool _isExpanded;

        private SettingsViewModel(Setting setting)
        {
            _setting = setting;
            _childs = new ReadOnlyCollection<SettingsViewModel>(
                (from child in _setting.ChildSettings
                 select new SettingsViewModel(child)).ToList());                                
        }

        public ReadOnlyCollection<SettingsViewModel> Childs { get { return _childs; } }

        public SettingType SettingType { get { return _setting.Type; } }

        public string SettingTitle { get { return _setting.Title; } }

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (value != _isExpanded)
                {
                    _isExpanded = value;
                    OnPropertyChanged("IsExpanded");
                }
            }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    OnPropertyChanged("IsSelected");
                    Messenger.Default.Send<SettingsViewModel>(this);
                }
            }
        }

        public static ReadOnlyCollection<SettingsViewModel> GetTopLevelSettings()
        {
            Setting receiverSettings = new Setting(SettingType.ReceiverSettings);
            Setting child = new Setting(SettingType.UdpClientSettings);
            receiverSettings.ChildSettings.Add(child);
            child = new Setting(SettingType.MulticastSourceSettings);
            receiverSettings.ChildSettings.Add(child);

            Setting senderSettings = new Setting(SettingType.SenderSettings);
            child = new Setting(SettingType.UdpServerSettings);
            senderSettings.ChildSettings.Add(child);

            var topLevelSettings = new ReadOnlyCollection<SettingsViewModel>(
                new SettingsViewModel[]
                {
                    new SettingsViewModel(receiverSettings),
                    new SettingsViewModel(senderSettings)
                });
            return topLevelSettings;
        }
    }
}
