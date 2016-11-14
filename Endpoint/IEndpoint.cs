using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recaster.Endpoint
{
    public interface IEndpoint
    {
        void Start();
        void Stop();
    }
}
