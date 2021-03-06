﻿using System.Net;

namespace Recaster.Multicast.Receiver.Qualifier
{
    public enum QualifierOption
    {
        Accept,
        Discard
    }
    class SourceQualifier : ISourceQualifier
    {
        private readonly IPAddress _sourceIp;
        private readonly int _sourcePort;
        private readonly QualifierOption _option;
        public SourceQualifier(IPAddress sourceIp, int sourcePort, QualifierOption option)
        {
            _sourceIp = sourceIp;
            _sourcePort = sourcePort;
            _option = option;
        }

        public SourceQualifier(IPAddress sourceIp, QualifierOption option)
        {
            _sourceIp = sourceIp;
            _option = option;
        }

        public SourceQualifier(int sourcePort, QualifierOption option)
        {
            _sourcePort = sourcePort;
            _option = option;
        }

        public bool IsSourceQualified(IPEndPoint sourceEndpoint)
        {
            if ((_sourceIp != null) && (_sourcePort != 0))
            {
                if (_option == QualifierOption.Accept)
                {
                    return _sourceIp.Equals(sourceEndpoint.Address) && (_sourcePort == sourceEndpoint.Port);
                }
                else
                {
                    return !(_sourceIp.Equals(sourceEndpoint.Address) && (_sourcePort == sourceEndpoint.Port));
                }
            }
            else if (_sourceIp != null)
            {
                if (_option == QualifierOption.Accept)
                {
                    return (Equals(_sourceIp, sourceEndpoint.Address));
                }
                else
                {
                    return !_sourceIp.Equals(sourceEndpoint.Address);
                }
            }
            else if (_sourcePort != 0)
            {
                if (_option == QualifierOption.Accept)
                {
                    return (_sourcePort == sourceEndpoint.Port);
                }
                else
                {
                    return _sourcePort != sourceEndpoint.Port;
                }
            }
            return true;

        }
    }
}
