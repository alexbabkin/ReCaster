using Recaster.Client.SettingsProvider;

namespace Recaster.Client.ViewModels
{
    public class UnicastClientSettingsViewModel : UnicastSettingsViewModel
    {
        public UnicastClientSettingsViewModel(IProvider settingsProvider)
        {
            Settings = settingsProvider.GetUnicastClientSettings();
            _title = "Tcp Client";
        }
    }
}
