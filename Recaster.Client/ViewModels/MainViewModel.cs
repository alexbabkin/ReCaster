using Recaster.Client.Settings;
using System.Collections.ObjectModel;
using Recaster.Client.Utility;
using System.ComponentModel;
using Recaster.Client.SettingsProvider;
using System.Windows.Input;
using System;

namespace Recaster.Client.ViewModels
{
    public class MainViewModel : ObservableElement
    {
        private readonly IProvider _settingsProvider;
        private readonly ReadOnlyCollection<SettingsViewModel> _topLevelSettings;
        private readonly ISettingsPageViewModel _unicastServerSettingsVM;
        private readonly ISettingsPageViewModel _unicastClientSettingsVM;
        private readonly ISettingsPageViewModel _multicastSourcesSettingsVM;
        private ISettingsPageViewModel _currentPage;

        private void LoadCommands()
        {
            ChangeReceiverStateCoammnd = new CustomCommand(ChangeReceiverState, CanChangeReiverState);
            ChangeSenderStateCommand = new CustomCommand(ChangeSenderState, CanChangeSenderState);
        }

        private bool CanChangeSenderState(object obj)
        {
            return false;
        }

        private bool CanChangeReiverState(object obj)
        {
            return false;
        }

        private void ChangeSenderState(object obj)
        {
            throw new NotImplementedException();
        }

        private void ChangeReceiverState(object obj)
        {
            throw new NotImplementedException();
        }

        public MainViewModel()
        {
            _settingsProvider = new TestProvider();
            _unicastServerSettingsVM = new UnicastServerSettingsViewModel(_settingsProvider);
            _unicastClientSettingsVM = new UnicastClientSettingsViewModel(_settingsProvider);
            _multicastSourcesSettingsVM = new MulticastSourcesSettingsViewModel(_settingsProvider);

            var receiverSettings = Setting.GetReceiverRoot();
            var senderSettings = Setting.GetSenderRoot();

            _topLevelSettings = new ReadOnlyCollection<SettingsViewModel>(
                new SettingsViewModel[]
                {
                    new SettingsViewModel(receiverSettings),
                    new SettingsViewModel(senderSettings)
                });

            LoadCommands();
            Messenger.Default.Register<SettingsViewModel>(this, OnPageSelected);
        }

        private void OnPageSelected(SettingsViewModel selectedPage)
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

        public ReadOnlyCollection<SettingsViewModel> TopLevelSettings { get { return _topLevelSettings; } }
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

        public ICommand ChangeReceiverStateCoammnd { get; set; }
        public ICommand ChangeSenderStateCommand { get; set; }
    }
}
