using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SE3314Assignment1
{
    class RTPmodel
    {
        Socket udpSocket;
        IPEndPoint EP;

        public RTPmodel(int port)
        {
            IPAddress destAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint destEndPoint = new IPEndPoint(destAddress, port);
            EP = destEndPoint;
            udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        }

        public Socket getSocket()
        {
            return udpSocket;
        }

        public IPEndPoint getIPEP()
        {
            return EP;
        }

        internal void sendStuff(byte[] p)
        {
            udpSocket.SendTo(p, EP);
        }
    }
}
