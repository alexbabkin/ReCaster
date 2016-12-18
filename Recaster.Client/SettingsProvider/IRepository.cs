using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recaster.Common;

namespace Recaster.Client.SettingsProvider
{
    public interface IProvider
    {
        UnicastSettings GetUnicastServerSettings();
        void SetUnicastServerSettings(UnicastSettings settings);

        List<MulticastGroupSettings> GetMulticastSourceSettings();
        void SetMulticastSourceSettings(List<MulticastGroupSettings> settings);

        UnicastSettings GetUnicastClientSettings();
        void SetUnicastClientSettings(UnicastSettings settings);
    }
}
