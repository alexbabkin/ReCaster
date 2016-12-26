using Recaster.Client.SettingsProvider;

namespace Recaster.Client.ViewModels
{
    public class UnicastServerSettingsViewModel : UnicastSettingsViewModel
    {
        public UnicastServerSettingsViewModel(IProvider settingsProvider)
        {
            _settings = settingsProvider.GetUnicastServerSettings();
            _title = "Tcp Server";
        }
    }
}
