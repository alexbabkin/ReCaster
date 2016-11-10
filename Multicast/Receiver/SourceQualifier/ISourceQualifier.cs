using System;
using System.Net;

namespace Recaster.Multicast.Receiver.SourceQualifier
{
    interface ISourceQualifier
    {
        bool IsSourceQualified(IPEndPoint sourceEndpoint);
    }
}
