using System;
using System.Collections.Generic;

namespace Recaster.Common
{
    public class QualifierSettings
    {
        public string SourceIp { get; set; } = "::1";
        public int Port { get; set; } = 0;
        public bool Discard { get; set; } = true;
    }
}
