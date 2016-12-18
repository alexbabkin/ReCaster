using System;
using System.Collections.Generic;
using Recaster.Common;

namespace Recaster.Client.SettingsProvider
{
    public class TestProvider : IProvider
    {
        public List<MulticastGroupSettings> GetMulticastSourceSettings()
        {
            return null;
        }

        public UnicastSettings GetUnicastClientSettings()
        {
            return new UnicastSettings() { IP = "127.0.0.1", Port = 557 };
        }

        public UnicastSettings GetUnicastServerSettings()
        {
            return new UnicastSettings() { IP = "127.0.0.1", Port = 558 };
        }

        public void SetMulticastSourceSettings(List<MulticastGroupSettings> settings)
        {
            throw new NotImplementedException();
        }

        public void SetUnicastClientSettings(UnicastSettings settings)
        {
            throw new NotImplementedException();
        }

        public void SetUnicastServerSettings(UnicastSettings settings)
        {
            throw new NotImplementedException();
        }
    }
}
