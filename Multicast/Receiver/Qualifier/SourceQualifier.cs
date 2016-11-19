using System.Net;

namespace Recaster.Multicast.Receiver.Qualifier
{
    public enum QualifierOption
    {
        Accept,
        Discard
    }
    class SourceQualifier : ISourceQualifier
    {
        private readonly IPAddress _sourceIP = null;
        private readonly int _sourcePort = 0;
        private readonly QualifierOption _option;
        public SourceQualifier(IPAddress sourceIP, int sourcePort, QualifierOption option)
        {
            _sourceIP = sourceIP;
            _sourcePort = sourcePort;
            _option = option;
        }

        public SourceQualifier(IPAddress sourceIP, QualifierOption option)
        {
            _sourceIP = sourceIP;
            _option = option;
        }

        public SourceQualifier(int sourcePort, QualifierOption option)
        {
            _sourcePort = sourcePort;
            _option = option;
        }

        public bool IsSourceQualified(IPEndPoint sourceEndpoint)
        {
            if ((_sourceIP != null) && (_sourcePort != 0))
            {
                if (_option == QualifierOption.Accept)
                {
                    return _sourceIP.Equals(sourceEndpoint.Address) && (_sourcePort == sourceEndpoint.Port);
                }
                else
                {
                    return !(_sourceIP.Equals(sourceEndpoint.Address) && (_sourcePort == sourceEndpoint.Port));
                }
            }
            else if (_sourceIP != null)
            {
                if (_option == QualifierOption.Accept)
                {
                    return (_sourceIP == sourceEndpoint.Address);
                }
                else
                {
                    return !_sourceIP.Equals(sourceEndpoint.Address);
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
                    return !(_sourcePort == sourceEndpoint.Port);
                }
            }
            return true;

        }
    }
}
