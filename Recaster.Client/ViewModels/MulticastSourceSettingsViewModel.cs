using Recaster.Common;
using Recaster.Client.SettingsProvider;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Recaster.Client.ViewModels.ObservableSrcSettings;
using System.Collections.Specialized;

namespace Recaster.Client.ViewModels
{
    public class MulticastSourcesSettingsViewModel : ObservableElement, ISettingsPageViewModel
    {
        private ObservableCollection<ObservableMulticastGroupSettings> _settings;
        private ObservableMulticastGroupSettings _selectedItem;

        public MulticastSourcesSettingsViewModel(IProvider settingsProvider)
        {
            _settings = new ObservableCollection<ObservableMulticastGroupSettings>();
            var sourcesSettings = settingsProvider.GetMulticastSourceSettings();
            
            foreach (var sourceSetting in sourcesSettings)
            {
                var s = new ObservableMulticastGroupSettings(sourceSetting);
                _settings.Add(s);
            }
            _selectedItem = _settings[0];
        }

        public string Title
        {
            get { return "Multicast Sources"; }
        }

        public ObservableCollection<ObservableMulticastGroupSettings> Settings
        {
            get { return _settings; }
        }

        public ObservableMulticastGroupSettings SelectedItem
        {
            get { return _selectedItem; }
        }
    }    
}
