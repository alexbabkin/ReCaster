using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;

namespace Recaster.IPv6Network
{
    interface ISourceQualifier
    {
        bool IsSourceQualified(IPEndPoint sourceEndpoint);
    }
}
