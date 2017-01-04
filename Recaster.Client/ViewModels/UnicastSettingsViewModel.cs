using System;
using Recaster.Common;

namespace Recaster.Client.ViewModels
{
    public abstract class UnicastSettingsViewModel : ObservableElement, ISettingsPageViewModel
    {
        protected UnicastSettings _settings;
        protected string _title;

        public string Title
        {
            get { return _title; }
        }

        public string Ip
        {
            get { return _settings.IP; }
            set
            {
                if (_settings.IP != value)
                {
                    _settings.IP = value;
                    OnPropertyChanged("Ip");
                }
            }
        }

        public int Port
        {
            get { return _settings.Port;}
            set
            {
                if (_settings.Port != value)
                {
                    _settings.Port = value;
                    OnPropertyChanged("Port");
                }
            }
        }

        public object GetSettings()
        {
            return new UnicastSettings()
            {
                IP = _settings.IP,
                Port = _settings.Port
            };
        }
    }
}
