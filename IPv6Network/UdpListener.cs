using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

namespace Recaster.IPv6Network
{
    class UdpListener
    {
        private Socket udpSock;
        private byte[] buffer;
        private ManualResetEvent _invokedCallbackReceive;

        public void Starter()
        {
            _invokedCallbackReceive = new ManualResetEvent(false);
            //Setup the socket and message buffer
            udpSock = new Socket(AddressFamily.InterNetworkV6, 
                SocketType.Dgram, 
                ProtocolType.Udp);
            udpSock.ExclusiveAddressUse = false;
            udpSock.MulticastLoopback = true;
            udpSock.Bind(new IPEndPoint(IPAddress.IPv6Any, 57125));

            foreach (var i in CollectNetworkInterfaceIndexes())
           {
                udpSock.SetSocketOption(SocketOptionLevel.IPv6,
                                     SocketOptionName.AddMembership,
                                     new IPv6MulticastOption(IPAddress.Parse("ff3e::ffff:ff01"), i));
             }



            buffer = new byte[64 * 1024 - 4096];

            //Start listening for a new message.
            EndPoint newClientEP = new IPEndPoint(IPAddress.IPv6Any, 57125);
            while (true)
            {
                //_invokedCallbackReceive.Reset();
                udpSock.BeginReceiveFrom(buffer, 
                    0, 
                    buffer.Length, 
                    SocketFlags.None, 
                    ref newClientEP, 
                    DoReceiveFrom, 
                    udpSock);
                Thread.Sleep(10);
              //  _invokedCallbackReceive.WaitOne();
            }
        }

        private void DoReceiveFrom(IAsyncResult iar)
        {
            try
            {
                //Get the received message.
                Socket recvSock = (Socket)iar.AsyncState;
                EndPoint clientEP = new IPEndPoint(IPAddress.Parse("ff3e::ffff:ff01"), 57125);
                int msgLen = recvSock.EndReceiveFrom(iar, ref clientEP);
                byte[] localMsg = new byte[msgLen];
                Array.Copy(buffer, localMsg, msgLen);

                //Start listening for a new message.
                EndPoint newClientEP = new IPEndPoint(IPAddress.IPv6Any, 0);
                udpSock.BeginReceiveFrom(buffer, 
                    0, 
                    buffer.Length, 
                    SocketFlags.None, 
                    ref newClientEP, 
                    DoReceiveFrom, 
                    udpSock);

                //Handle the received message
                Console.WriteLine("Recieved {0} bytes from {1}:{2}",
                                  msgLen,
                                  ((IPEndPoint)clientEP).Address,
                                  ((IPEndPoint)clientEP).Port);
                //Do other, more interesting, things with the received message.
            }
            catch (ObjectDisposedException)
            {
                //expected termination exception on a closed socket.
                // ...I'm open to suggestions on a better way of doing this.
            }
        }

        private List<int> CollectNetworkInterfaceIndexes()
        {
            var result = new List<int>();

            NetworkInterface[] nics =
                NetworkInterface.GetAllNetworkInterfaces();

            if (nics.Length < 1)
                return result;

            foreach (NetworkInterface adapter in nics)
            {
                if (adapter.OperationalStatus == OperationalStatus.Up &&
                    adapter.Supports(NetworkInterfaceComponent.IPv6))
                {
                    IPv6InterfaceProperties properties =
                        adapter.GetIPProperties().GetIPv6Properties();
                    result.Add(properties.Index);
                }
            }
            return result;
        }
    }
}
