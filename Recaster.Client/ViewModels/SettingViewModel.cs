using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Recaster.Client.Helpers;
using Recaster.Client.Settings;

namespace Recaster.Client.ViewModels
{
    public class SettingViewModel : BaseViewModel
    {
        private readonly ReadOnlyCollection<SettingViewModel> _childs;
        private readonly Setting _setting;
        private bool _isSelected;
        private bool _isExpanded;

        public SettingViewModel(Setting setting)
        {
            _setting = setting;
            _childs = new ReadOnlyCollection<SettingViewModel>(
                (from child in _setting.ChildSettings
                 select new SettingViewModel(child)).ToList());                                
        }

        public ReadOnlyCollection<SettingViewModel> Childs { get { return _childs; } }

        public string Title { get { return _setting.Title; } }

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
                    Messenger.Default.Send<SettingViewModel>(this);
                }
            }
        }
    }
}
