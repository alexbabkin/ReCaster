﻿using Recaster.Client.SettingsProvider;

namespace Recaster.Client.ViewModels
{
    public class UnicastServerSettingsViewModel : UnicastSettingsViewModel
    {
        public UnicastServerSettingsViewModel(IProvider settingsProvider)
        {
            Settings = settingsProvider.GetUnicastServerSettings();
            Title = "Tcp Server";
        }
    }
}
