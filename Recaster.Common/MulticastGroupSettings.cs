using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recaster.Common
{
    public class MulticastGroupSettings
    {
        public string Name;
        public string GroupAdreass { get; set; } = String.Empty;
        public int Port { get; set; } = 0;
        public List<QualifierSettings> Qualifier { get; set; }
    }
}
