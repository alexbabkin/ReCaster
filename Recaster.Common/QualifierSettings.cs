using System;
using System.Collections.Generic;

namespace Recaster.Common
{
    public class QualifierSettings
    {
        public string sourceIP { get; set; } = String.Empty;
        public int Port { get; set; } = 0;
        public bool Discard { get; set; }
    }
}
