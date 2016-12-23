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

        public SettingsViewModel(Setting setting)
        {
            _setting = setting;
            _childs = new ReadOnlyCollection<SettingsViewModel>(
                (from child in _setting.ChildSettings
                 select new SettingsViewModel(child)).ToList());                                
        }

        public ReadOnlyCollection<SettingsViewModel> Childs { get { return _childs; } }

        public SettingType Title { get { return _setting.Type; } }

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
    }
}
