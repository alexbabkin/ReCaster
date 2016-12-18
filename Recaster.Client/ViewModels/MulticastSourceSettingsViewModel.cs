using Recaster.Common;
using Recaster.Client.SettingsProvider;
using System.Collections.Generic;

namespace Recaster.Client.ViewModels
{
    public class MulticastSourcesSettingsViewModel : BaseViewModel, ISettingsPageViewModel
    {
        List<MulticastGroupSettings> _settings;
        public MulticastSourcesSettingsViewModel(IProvider settingsProvider)
        {
            _settings = settingsProvider.GetMulticastSourceSettings();
        }
        public string Title
        {
            get { return "Multicast Sources"; }
        }
    }
}
