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
            IMulticastReceiver udpListener1 = new MulticastReceiver(57125, IPAddress.Parse("ff3e::ffff:ff01"));
            udpListener1.SetSourceQualifier(new SourceQualifier(IPAddress.Parse("::1"), QualifierOption.Discard));
            udpListener1.MessageReceived += 
                (object sender, MulticastMsgEventArgs mMsg) => 
                Console.WriteLine("Receved message from {0}:{1}. Message length is {2}", mMsg.RemoteEndpoint.Address, mMsg.RemoteEndpoint.Port, mMsg.Data.Length);
            IMulticastReceiver udpListener2 = new MulticastReceiver(57126, IPAddress.Parse("ff15:1c6f:6581:33d8::fffe:b90b"));
            udpListener2.MessageReceived +=
                (object sender, MulticastMsgEventArgs mMsg) =>
                Console.WriteLine("Receved message from {0}:{1}. Message length is {2}", mMsg.RemoteEndpoint.Address, mMsg.RemoteEndpoint.Port, mMsg.Data.Length);

            Task[] taskArray = new Task[] { udpListener1.Start()
                ,udpListener2.Start()
            };
            try
            {
                Task.WaitAll(taskArray);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occured while awaiting: {0}", ex.ToString());
            }
        }
    }
}
