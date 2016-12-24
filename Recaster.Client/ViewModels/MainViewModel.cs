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
        private readonly ISettingsPageViewModel _unicastServerSettingsVM;
        private readonly ISettingsPageViewModel _unicastClientSettingsVM;
        private readonly ISettingsPageViewModel _multicastSourceSettingsVM;

        private ISettingsPageViewModel _currentPage;
        private ReadOnlyCollection<SettingsViewModel> _topLevelSettings;

        private void LoadTopLevelSettings()
        {
            var receiverSettings = Setting.GetReceiverRoot();
            var senderSettings = Setting.GetSenderRoot();

            _topLevelSettings = new ReadOnlyCollection<SettingsViewModel>(
                new SettingsViewModel[]
                {
                    new SettingsViewModel(receiverSettings),
                    new SettingsViewModel(senderSettings)
                });
        }

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

        public MainViewModel(IProvider settingsProvider, 
            UnicastClientSettingsViewModel unicastClientSettingsVM, 
            UnicastServerSettingsViewModel unicastServerSettingsVM, 
            MulticastSourcesSettingsViewModel multicastSourceSettingsVM)
        {
            _settingsProvider = settingsProvider;
            _unicastServerSettingsVM = unicastServerSettingsVM;
            _unicastClientSettingsVM = unicastClientSettingsVM;
            _multicastSourceSettingsVM = multicastSourceSettingsVM;

            LoadTopLevelSettings();
            LoadCommands();

            Messenger.Default.Register<SettingsViewModel>(this, OnSelectedSetting);
        }

        private void OnSelectedSetting(SettingsViewModel selectedSetting)
        {
            switch (selectedSetting.SettingType)
            {
                case SettingType.UdpServerSettings:
                    CurrentPage = _unicastServerSettingsVM;
                    break;
                case SettingType.UdpClientSettings:
                    CurrentPage = _unicastClientSettingsVM;
                    break;
                case SettingType.MulticastSourceSettings:
                    CurrentPage = _multicastSourceSettingsVM;
                    break;
                default:
                    CurrentPage = null;
                    break;
            }
        }

        public ReadOnlyCollection<SettingsViewModel> TopLevelSettings { get { return _topLevelSettings; } }
        public ISettingsPageViewModel CurrentPage
        {
            get { return _currentPage; }
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
