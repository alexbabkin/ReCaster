using Recaster.Common;
using Recaster.Client.SettingsProvider;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Recaster.Client.ViewModels.ObservableSrcSettings;
using System.Windows.Input;
using Recaster.Client.Utility;
using System.Linq;

namespace Recaster.Client.ViewModels
{
    public class MulticastSourcesSettingsViewModel : ObservableElement, ISettingsPageViewModel
    {
        private ObservableCollection<ObservableMulticastGroupSettings> _settings;
        private ObservableMulticastGroupSettings _selectedSource;

        private void LoadCommand()
        {
            AddSourceCommand = new CustomCommand(AddSource, CanAddSource);
            DeleteSourceCommand = new CustomCommand(DeleteSource, CanDeleteSource);
            AddQualifierCommand = new CustomCommand(AddQualifier, CanAddQualifier);
            DeleteQualifierCommand = new CustomCommand(DeleteQualifier, CanDeleteQualifier);
        }

        private bool CanDeleteQualifier(object obj)
        {
            return (_selectedSource != null) && (SelectedQualifier != null);
        }

        private void DeleteQualifier(object obj)
        {
            if (SelectedQualifier != null)
                _selectedSource.Qualifiers.Remove(SelectedQualifier);
            SelectedQualifier = null;
        }

        private bool CanAddQualifier(object obj)
        {
            return _selectedSource != null;
        }

        private void AddQualifier(object obj)
        {
            var newQualifier = new QualifierSettings()
            {
                sourceIP = "::1",
                Port = 0,
                Discard = true
            };
            _selectedSource.Qualifiers.Add(new ObservableQualifierSettings(newQualifier));
        }

        private bool CanDeleteSource(object obj)
        {
            return _selectedSource != null;
        }

        private void DeleteSource(object obj)
        {
            _settings.Remove(_selectedSource);
            SelectedSource = null;
        }

        private void AddSource(object obj)
        {
            var newSource = new MulticastGroupSettings()
            {
                Name = "Name",
                GroupAdreass = "::1",
                Port = 0,
                Qualifier = new List<QualifierSettings>()
            };
            _settings.Add(new ObservableMulticastGroupSettings(newSource));
        }

        private bool CanAddSource(object obj)
        {
            return true;
        }

        public object GetSettings()
        {
            var settings = new List<MulticastGroupSettings>();
            foreach (var source in _settings)
            {
                var s = new MulticastGroupSettings()
                {
                    Name = source.Name,
                    GroupAdreass = source.GroupAdreass,
                    Port = source.Port,
                    Qualifier = (from q in source.Qualifiers
                                 select new QualifierSettings()
                                 {
                                     sourceIP = q.SourceIP,
                                     Port = q.Port,
                                     Discard = q.Discard
                                 }).ToList()                 
                };
                settings.Add(s);
            }
            return settings;
        }

        public MulticastSourcesSettingsViewModel(IProvider settingsProvider)
        {
            _settings = new ObservableCollection<ObservableMulticastGroupSettings>();
            var sourcesSettings = settingsProvider.GetMulticastSourceSettings();
            
            foreach (var sourceSetting in sourcesSettings)
            {
                var s = new ObservableMulticastGroupSettings(sourceSetting);
                _settings.Add(s);
            }
            LoadCommand();
        }

        public string Title
        {
            get { return "Multicast Sources"; }
        }

        public ObservableCollection<ObservableMulticastGroupSettings> Settings
        {
            get { return _settings; }
        }

        public ObservableMulticastGroupSettings SelectedSource
        {
            get { return _selectedSource; }
            set
            {
                if (_selectedSource != value)
                {
                    _selectedSource = value;
                    OnPropertyChanged("SelectedSource");
                }
            }
        }
        
        public ObservableQualifierSettings SelectedQualifier { get; set; }

        public ICommand AddSourceCommand { get; set; }
        public ICommand DeleteSourceCommand { get; set; }
        public ICommand AddQualifierCommand { get; set; }
        public ICommand DeleteQualifierCommand { get; set; }
    }    
}
