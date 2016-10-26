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
            var udpListener1 = new UdpListener(57125, IPAddress.Parse("ff3e::ffff:ff01"));
            var udpListener2 = new UdpListener(57126, IPAddress.Parse("ff15:1c6f:6581:33d8::fffe:b90b"));

            Task[] taskArray = new Task[] { udpListener1.Starter(),
                udpListener2.Starter() };
            while (true)
                ;
        }
    }
}
