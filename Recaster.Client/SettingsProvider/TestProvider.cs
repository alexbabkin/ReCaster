using System;
using System.Collections.Generic;
using Recaster.Common;

namespace Recaster.Client.SettingsProvider
{
    public class TestProvider : IProvider
    {
        public List<MulticastGroupSettings> GetMulticastSourceSettings()
        {
            var q = new List<QualifierSettings>
            {
                new QualifierSettings() {SourceIp = "::1", SourcePort = 0, Discard = true}
            };
            var settings = new List<MulticastGroupSettings>
            {
                new MulticastGroupSettings()
                {
                    Name = "Name",
                    GroupAdreass = "ff3e::ffff:ff01",
                    GroupPort = 57125,
                    Qualifier = q
                }
            };
            return settings;
        }

        public UnicastSettings GetUnicastClientSettings()
        {
            return new UnicastSettings() { Ip = "127.0.0.1", Port = 557 };
        }

        public UnicastSettings GetUnicastServerSettings()
        {
            return new UnicastSettings() { Ip = "127.0.0.1", Port = 558 };
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

        public void StartEndpoint(EndpointType endpointType)
        {
            throw new NotImplementedException();
        }

        public void StopEndpoint(EndpointType endpointType)
        {
            throw new NotImplementedException();
        }
    }
}
