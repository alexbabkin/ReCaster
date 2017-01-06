using Recaster.Common;

namespace Recaster.Client.ViewModels
{
    public abstract class UnicastSettingsViewModel : ObservableElement, ISettingsPageViewModel
    {
        protected UnicastSettings Settings;
        protected string _title;

        public string Title => _title;

        public string Ip
        {
            get { return Settings.Ip; }
            set
            {
                if (Settings.Ip != value)
                {
                    Settings.Ip = value;
                    OnPropertyChanged("Ip");
                }
            }
        }

        public int Port
        {
            get { return Settings.Port;}
            set
            {
                if (Settings.Port != value)
                {
                    Settings.Port = value;
                    OnPropertyChanged("Port");
                }
            }
        }

        public object GetSettings()
        {
            return new UnicastSettings()
            {
                Ip = Settings.Ip,
                Port = Settings.Port
            };
        }
    }
}
