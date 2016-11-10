using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recaster.MDistributor
{
    interface IMulticastDistributor
    {
        void Start();
        void Stop();
    }
}
