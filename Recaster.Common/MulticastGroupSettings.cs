using System;
using System.Collections.Generic;

namespace Recaster.Common
{
    public class MulticastGroupSettings
    {
        public string Name { get; set; } = "DefaultName";
        public string GroupAdreass { get; set; } = "::1";
        public int Port { get; set; } = 0;
        public List<QualifierSettings> Qualifier { get; set; }
    }
}
