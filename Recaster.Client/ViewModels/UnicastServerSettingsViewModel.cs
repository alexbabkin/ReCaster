using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recaster.Common;
using System.ComponentModel;
using Recaster.Client.ViewModels;
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
