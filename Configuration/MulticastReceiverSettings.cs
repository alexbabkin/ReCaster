using System;
using System.Collections.Generic;

namespace Recaster.Configuration
{
    public class QualifierSettings
    {
        public string sourceIP { get; set; } = String.Empty;
        public int Port { get; set; } = 0;
        public bool Disacard { get; set; }
    }
    public class MulticastGroupSettings
    {
        public string Name;
        public string GroupAdreass { get; set; } = String.Empty;
        public int Port { get; set; } = 0;
        public List<QualifierSettings> Qualifier { get; set; }
    }
}
