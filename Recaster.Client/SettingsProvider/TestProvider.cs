using System;
using System.Collections.Generic;
using Recaster.Common;

namespace Recaster.Client.SettingsProvider
{
    public class TestProvider : IProvider
    {
        public List<MulticastGroupSettings> GetMulticastSourceSettings()
        {
            var settings = new List<MulticastGroupSettings>();
            var q = new List<QualifierSettings>();
            q.Add(new QualifierSettings() { sourceIP = "::1", Port = 0, Discard = true });
            var s = new MulticastGroupSettings() { Name = "Name", GroupAdreass = "ff3e::ffff:ff01", Port = 57125, Qualifier = q };
            settings.Add(s);
            return settings;
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
