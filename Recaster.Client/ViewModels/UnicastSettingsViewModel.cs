using Recaster.Common;

namespace Recaster.Client.ViewModels
{
    public abstract class UnicastSettingsViewModel : ObservableElement, ISettingsPageViewModel
    {
        public string Title { get; protected set; }
        protected UnicastSettings Settings { private get;  set; }

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
