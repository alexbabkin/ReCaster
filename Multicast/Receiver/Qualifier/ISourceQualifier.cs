using System;
using System.Net;

namespace Recaster.Multicast.Receiver.Qualifier
{
    interface ISourceQualifier
    {
        bool IsSourceQualified(IPEndPoint sourceEndpoint);
    }
}
