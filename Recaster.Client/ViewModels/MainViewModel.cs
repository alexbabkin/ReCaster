using Recaster.Client.Settings;
using System.Collections.ObjectModel;
using Recaster.Client.Utility;
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
        private readonly ReadOnlyCollection<SettingsViewModel> _topLevelSettings;
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

        public MainViewModel(IProvider settingsProvider,
            ReadOnlyCollection<SettingsViewModel> topLevelSettingsVM,
            UnicastClientSettingsViewModel unicastClientSettingsVM, 
            UnicastServerSettingsViewModel unicastServerSettingsVM, 
            MulticastSourcesSettingsViewModel multicastSourceSettingsVM)
        {
            _settingsProvider = settingsProvider;
            _unicastServerSettingsVM = unicastServerSettingsVM;
            _unicastClientSettingsVM = unicastClientSettingsVM;
            _multicastSourceSettingsVM = multicastSourceSettingsVM;
            _topLevelSettings = topLevelSettingsVM;

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
