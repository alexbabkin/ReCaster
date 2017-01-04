using Recaster.Client.Settings;
using System.Collections.ObjectModel;
using Recaster.Client.Utility;
using Recaster.Client.SettingsProvider;
using System.Windows.Input;
using System;
using Recaster.Common;
using System.Collections.Generic;

namespace Recaster.Client.ViewModels
{
    public class MainViewModel : ObservableElement
    {
        private const string ReceiverTitleToStart = "Start Receiver";
        private const string ReceiverTitleToStop = "Stop Receiver";
        private const string SenderTitleToStart = "Start Sender";
        private const string SenderTitleToStop = "Start Sender";

        private string _receverStateTitle;
        private string _senderStateTitle;

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
            return true;
        }

        private bool CanChangeReiverState(object obj)
        {
            return true;
        }

        private void ChangeSenderState(object obj)
        {
            if (SenderStateTitle == SenderTitleToStart)
            {
                UnicastSettings serverSettings = _unicastServerSettingsVM.GetSettings()
                    as UnicastSettings;
                _settingsProvider.SetUnicastServerSettings(serverSettings);

                _settingsProvider.StartEndpoint(EndpointType.MulitcastSender);

                SenderStateTitle = SenderTitleToStop;
            }
            else if (SenderStateTitle == SenderTitleToStop)
            {
                _settingsProvider.StopEndpoint(EndpointType.MulitcastSender);
                SenderStateTitle = SenderTitleToStart;
            }            
        }

        private void ChangeReceiverState(object obj)
        {
            if (ReceiverStateTitle == ReceiverTitleToStart)
            {
                UnicastSettings clientSettings = _unicastClientSettingsVM.GetSettings()
                    as UnicastSettings;
                _settingsProvider.SetUnicastClientSettings(clientSettings);

                List<MulticastGroupSettings> mSourceSettings = _multicastSourceSettingsVM.GetSettings()
                    as List<MulticastGroupSettings>;
                _settingsProvider.SetMulticastSourceSettings(mSourceSettings);

                _settingsProvider.StartEndpoint(EndpointType.MulticastCatcher);

                ReceiverStateTitle = ReceiverTitleToStop;
            }
            else if (ReceiverStateTitle == ReceiverTitleToStop)
            {
                _settingsProvider.StopEndpoint(EndpointType.MulticastCatcher);
                ReceiverStateTitle = ReceiverTitleToStart;
            }
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
            ReceiverStateTitle = ReceiverTitleToStart;
            SenderStateTitle = SenderTitleToStart;

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

        public string ReceiverStateTitle
        {
            get { return _receverStateTitle; }
            private set
            {
                if (value != _receverStateTitle)
                {
                    _receverStateTitle = value;
                    OnPropertyChanged("ReceiverStateTitle");
                }
            }
        }

        public string SenderStateTitle
        {
            get { return _senderStateTitle; }
            private set
            {
                if (value != _senderStateTitle)
                {
                    _senderStateTitle = value;
                    OnPropertyChanged("SenderStateTitle");
                }
            }
        }
    }
}
