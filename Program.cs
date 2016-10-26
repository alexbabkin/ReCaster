using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Recaster.IPv6Network;
using System.Net.Sockets;
using System.Net;

namespace Recaster
{
    class Program
    {
        static void Main(string[] args)
        {
            var udpListener = new UdpListener();
            udpListener.Starter();
        }
    }
}
