using Recaster.Client.Settings;
using System.Collections.ObjectModel;
using Recaster.Client.Helpers;
using System.ComponentModel;
using Recaster.Client.SettingsProvider;

namespace Recaster.Client.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IProvider _settingsProvider;
        private readonly ReadOnlyCollection<SettingViewModel> _topLevelSettings;
        private readonly ISettingsPageViewModel _unicastServerSettingsVM;
        private readonly ISettingsPageViewModel _unicastClientSettingsVM;
        private readonly ISettingsPageViewModel _multicastSourcesSettingsVM;

        private ISettingsPageViewModel _currentPage;
        

        public MainViewModel()
        {
            _settingsProvider = new TestProvider();
            _unicastServerSettingsVM = new UnicastServerSettingsViewModel(_settingsProvider);
            _unicastClientSettingsVM = new UnicastClientSettingsViewModel(_settingsProvider);
            _multicastSourcesSettingsVM = new MulticastSourcesSettingsViewModel(_settingsProvider);

            var receiverSettings = Setting.GetReceiverRoot();
            var senderSettings = Setting.GetSenderRoot();

            _topLevelSettings = new ReadOnlyCollection<SettingViewModel>(
                new SettingViewModel[]
                {
                    new SettingViewModel(receiverSettings),
                    new SettingViewModel(senderSettings)
                });
            
            Messenger.Default.Register<SettingViewModel>(this, OnPageSelected);
        }

        private void OnPageSelected(SettingViewModel selectedPage)
        {
            switch (selectedPage.Title)
            {
                case "Tcp Server":
                    CurrentPage = _unicastServerSettingsVM;
                    break;
                case "Tcp Client":
                    CurrentPage = _unicastClientSettingsVM;
                    break;
                case "Multicast Sources":
                    CurrentPage = _multicastSourcesSettingsVM;
                    break;
                default:
                    CurrentPage = null;
                    break;
            }
        }

        public ReadOnlyCollection<SettingViewModel> TopLevelSettings { get { return _topLevelSettings; } }
        public ISettingsPageViewModel CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                if (value != _currentPage)
                {
                    _currentPage = value;
                    OnPropertyChanged("CurrentPage");
                }
            }
        }
    }
}
