using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace SE3314Assignment1
{
    class RTSPModel
    {
        private int portNo;
        public IPAddress ServerIP;
        byte[] receiveBuffer = new byte[1024];
        Socket RTSPSocket = null;
        Socket tcpClient = null;
        IPEndPoint serverEP = null;

        public RTSPModel(int p)
        {
            // TODO: Complete member initialization
            this.portNo = p;
            try
            {
                this.ServerIP = IPAddress.Parse("127.0.0.1");
            }
            catch (FormatException err)
            {
                MessageBox.Show("Invalid IP address: {0}", err.Message);
            }

            //Create TCP socket
            serverEP = new IPEndPoint(ServerIP, portNo);

            try
            {
                RTSPSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
    ProtocolType.Tcp);
                RTSPSocket.Bind(serverEP);
            }
            catch (SocketException err)
            {
                if (RTSPSocket != null)
                    RTSPSocket.Close();
            }

            RTSPSocket.Listen(int.MaxValue);
            
        }

        internal System.Net.Sockets.Socket AcceptOneClient()
        {
            tcpClient = RTSPSocket.Accept(); //the process holds until
            // a client connects
            //MessageBox.Show("Accepted connection from: {0}", tcpClient.RemoteEndPoint.ToString());
            return tcpClient;
        }
    }
}
