using Recaster.Client.Settings;
using System.Collections.ObjectModel;
using Recaster.Client.Utility;
using Recaster.Client.SettingsProvider;
using System.Windows.Input;
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
        private readonly ISettingsPageViewModel _unicastServerSettingsVm;
        private readonly ISettingsPageViewModel _unicastClientSettingsVm;
        private readonly ISettingsPageViewModel _multicastSourceSettingsVm;
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
                UnicastSettings serverSettings = _unicastServerSettingsVm.GetSettings()
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
                UnicastSettings clientSettings = _unicastClientSettingsVm.GetSettings()
                    as UnicastSettings;
                _settingsProvider.SetUnicastClientSettings(clientSettings);

                List<MulticastGroupSettings> mSourceSettings = _multicastSourceSettingsVm.GetSettings()
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
            ReadOnlyCollection<SettingsViewModel> topLevelSettingsVm,
            UnicastClientSettingsViewModel unicastClientSettingsVm, 
            UnicastServerSettingsViewModel unicastServerSettingsVm, 
            MulticastSourcesSettingsViewModel multicastSourceSettingsVm)
        {
            _settingsProvider = settingsProvider;
            _unicastServerSettingsVm = unicastServerSettingsVm;
            _unicastClientSettingsVm = unicastClientSettingsVm;
            _multicastSourceSettingsVm = multicastSourceSettingsVm;
            TopLevelSettings = topLevelSettingsVm;

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
                    CurrentPage = _unicastServerSettingsVm;
                    break;
                case SettingType.UdpClientSettings:
                    CurrentPage = _unicastClientSettingsVm;
                    break;
                case SettingType.MulticastSourceSettings:
                    CurrentPage = _multicastSourceSettingsVm;
                    break;
                default:
                    CurrentPage = null;
                    break;
            }
        }

        public ReadOnlyCollection<SettingsViewModel> TopLevelSettings { get; }

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
