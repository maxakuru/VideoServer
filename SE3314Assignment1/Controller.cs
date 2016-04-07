using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Timers;

namespace SE3314Assignment1
{
    class Controller
    {
        List<ClientModel> _clientModel = new List<ClientModel>();
        //public ClientModel _clientModel = null;
        Thread listenThread;
        private static View _view;
        RTSPModel _RTSPModel = null;
        Thread clientThread;
        List<int> usedIds = new List<int>();
        int cnt = 0;
        
        private static readonly Random getrandom = new Random();

        public void listenButton_btn_Click(object sender, EventArgs e)
        {
            _view = (View)((Button)sender).FindForm();
            _view.Disable_listenButton();

            this.listenThread = new Thread(ListenForClients);
            listenThread.IsBackground = true;
            this.listenThread.Start();
        }

        public void ListenForClients()
        {
            _RTSPModel = new RTSPModel(Int32.Parse(_view.GetPortNumber()));

            UpdatedServerIP(_RTSPModel.ServerIP.ToString());
     
            while (true)
            {
                updateInfoBox("Server is waiting for new connection" + "\r\n");

                Socket RTSPsocket = _RTSPModel.AcceptOneClient();
                updateInfoBox("The client "+ RTSPsocket.RemoteEndPoint.ToString() +" has joined"+ "\r\n");

                clientThread = new Thread(new ParameterizedThreadStart(Communications));
                clientThread.IsBackground = true;
                clientThread.Start(RTSPsocket);
            }
        }

        
        private void Communications(object obj)
        {
           
            byte[] receiveBuffer = new byte[256];
            Socket s = (Socket)obj;
            //Console.Write("In Communications");
            int rand = 0;
            MJPEGVideo video = null;
            bool needsSetup = true;
            RTPmodel _RTPModel = null;
            int n = 0;
            ClientModel newClient = null;
            try
            {
                //ClientModel _clientModel = new ClientModel();
                while (needsSetup)
                {
                    //byte[] receiveBuffer = new byte[100];
                    bool getOut = false;
                    int rc = s.Receive(receiveBuffer);
                    if (rc == 0)
                        break;
                    string str = System.Text.Encoding.UTF8.GetString(receiveBuffer, 0, rc);
                    _view.SetClientBox(str);
                    string cs = System.Text.Encoding.UTF8.GetString(receiveBuffer, 0, 4);
                    int session = 0;
                    //if(!needsSetup)
                        //session = Int32.Parse(System.Text.Encoding.UTF8.GetString(receiveBuffer, rc-13, rc));

                    if (cs == "SETU" && needsSetup) //if command is SETUP
                    {
                        Console.Write("in setup \r\n");
                        //make RTP object
                        string[] ss = str.Split(new char[] { '=' });
                        int port = Int32.Parse(ss[1]);
                        _RTPModel = new RTPmodel(port);

                        //create new MJPEGVideo instance
                        string name = System.Text.Encoding.UTF8.GetString(receiveBuffer, 28, 12);
                        video = new MJPEGVideo(name);
                        //generate random number to identify client
                        rand = GetUniqueRandom();
                        
                        //_clientModel = new ClientModel(rand, video, port);
                        newClient = new ClientModel(rand, video, port);
                        _clientModel.Add(newClient);
                        n = cnt++;

                        //update the view server status box
                        updateInfoBox("The client " + _clientModel.ElementAt(n).getRtpModel().getIPEP().ToString() + " is setting up" + "\r\n");

                        _clientModel.ElementAt(n).getTimer().Elapsed += TimerEventProcessor;

                        needsSetup = false;
                        //response
                        //create lines of message
                        string requestLine = "RTSP/1.0 200 OK\r\n";
                        string cseqLine = "CSeq: 1\r\n";
                        string sessionLine = "Session: " + _clientModel.ElementAt(n).getId() + "\r\n";
                        string message = requestLine + cseqLine + sessionLine;
                        byte[] messageBytes = System.Text.Encoding.ASCII.GetBytes(message);
                        s.Send(messageBytes);
                    }

                    //else if (_clientModel.getId() == session) //otherwise the command is not SETUP
                    while(!needsSetup){
                        int rc2 = s.Receive(receiveBuffer);
                        if (rc == 0)
                            break;
                        string str2 = System.Text.Encoding.UTF8.GetString(receiveBuffer, 0, rc);
                        _view.SetClientBox(str2);
                        string cs2 = System.Text.Encoding.UTF8.GetString(receiveBuffer, 0, 4);

                        switch (cs2)
                        {
                            case "PLAY":
                                Console.Write("in play for client: " + _clientModel.ElementAt(n).getId() + "\r\n");
                                //send response FIRST
                                string requestLine2 = "RTSP/1.0 200 OK\r\n";
                                string cseqLine2 = "CSeq: 1\r\n";
                                string sessionLine2 = "Session: " + _clientModel.ElementAt(n).getId() + "\r\n";
                                string message2 = requestLine2 + cseqLine2 + sessionLine2;
                                byte[] message2Bytes = System.Text.Encoding.ASCII.GetBytes(message2);
                                s.Send(message2Bytes);

                                //update the view server status box
                                updateInfoBox("The client " + _clientModel.ElementAt(n).getRtpModel().getIPEP().ToString() + " is playing " + video.getName() + "\r\n");

                                //start client model's timer
                                _clientModel.ElementAt(n).startTimer();
                                break;

                            case "PAUS":
                                Console.Write("in pause for client: " + _clientModel.ElementAt(n).getId() + "\r\n");
                                //send response FIRST
                                string requestLine3 = "RTSP/1.0 200 OK\r\n";
                                string cseqLine3 = "CSeq: 1\r\n";
                                string sessionLine3 = "Session: " + rand + "\r\n";
                                string message3 = requestLine3 + cseqLine3 + sessionLine3;
                                byte[] message3Bytes = System.Text.Encoding.ASCII.GetBytes(message3);
                                s.Send(message3Bytes);

                                //update the view server status box
                                updateInfoBox("The client " + _clientModel.ElementAt(n).getRtpModel().getIPEP().ToString() + " paused " + video.getName() + "\r\n");
                                //stop timer
                                _clientModel.ElementAt(n).stopTimer();
                                break;

                            case "TEAR":
                                Console.Write("in teardown for client: " + _clientModel.ElementAt(n).getId() + "\r\n");
                                //send response FIRST
                                string requestLine4 = "RTSP/1.0 200 OK\r\n";
                                string cseqLine4 = "CSeq: 1\r\n";
                                string sessionLine4 = "Session: " + rand + "\r\n";
                                string message4 = requestLine4 + cseqLine4 + sessionLine4;
                                byte[] message4Bytes = System.Text.Encoding.ASCII.GetBytes(message4);
                                s.Send(message4Bytes);

                                //update the view server status box
                                updateInfoBox("The client " + _clientModel.ElementAt(n).getRtpModel().getIPEP().ToString() + " is tearing down " + video.getName() + "\r\n");

                                //stop timer
                                _clientModel.ElementAt(n).stopTimer();

                                //delete mjpeg instance
                                _clientModel.ElementAt(n).getVideo().disposeVideo();
                                this.usedIds.Remove(_clientModel.ElementAt(n).getId());
                                //_clientModel.RemoveAt(n);
                       
                                needsSetup = true;
                                
                                break;

                            default://should never reach here
                                break;
                        }
                    }
                    if (getOut)
                        break;

                    //inside while loop
                    //receiveBuffer = new byte[128];
                }


            }
            catch (SocketException err)
            {
                //MessageBox.Show("Error occurred on accepted socket: {0}",err.Message);
                if(s != null)
                    s.Close();
            }
            finally
            {
                //if(rand == _clientModel.getId())
                _clientModel.ElementAt(n).stopTimer();
                s.Close();
            }
        }

        private void TimerEventProcessor(Object obj, ElapsedEventArgs args)
        {
            //Communications.
            RTSPTimer time = (RTSPTimer)obj;
            //check the ID of the timer Tick event
            //create new RTPpacket using getFrame of the MJPEGVideo
            for (int i = 0; i < _clientModel.Count; i++)
            {
                if(_clientModel.ElementAt(i).getId() == time.getId())
                //if (time.getId() == _clientModel.getId()) //this instance is the correct timer to perform operation on
                {
                    //Console.Write("Timer elapsed event for: " + _clientModel.getId().ToString() + "\r\n");
                    _clientModel.ElementAt(i).incrementSeqNo();
                    RTPmodel rtpModel = _clientModel.ElementAt(i).getRtpModel();
                    IPEndPoint EP = rtpModel.getIPEP();
                    //Console.Write("Sending packet to EndPoint: " + EP.ToString() + "\r\n");
                    MJPEGVideo video = _clientModel.ElementAt(i).getVideo();
                    //RTPpacket rtpPacket = new RTPpacket(video.GetNextFrame(), seqNo); //create a new RTPpacket
                    //byte[] someArray = new byte[15000];
                    byte[] arr = video.GetNextFrame();
                    RTPpacket rtppacket = new RTPpacket(arr, _clientModel.ElementAt(i).getSeqNo(), arr.Length);
                    
                    //check if the RTP header should be displayed
                    if (_view.displayRtpHeader())
                    {
                        updateInfoBox(rtppacket.getHeader() + "\r\n");
                    }

                    //update the frame number
                    setFrameBox(_clientModel.ElementAt(i).getSeqNo());

                    rtpModel.sendStuff(rtppacket.getBytes());
                    //_RTPModel.getSocket().SendTo(rtppacket.getBytes(), EP);
                }
            }
            //else
                //Console.Write("other one is here");

        }

        private int GetUniqueRandom()
        {
            bool cont = true;
            int toReturn = 0;
            do
            {
                toReturn = getrandom.Next();
                if (!usedIds.Contains(toReturn))
                    cont = false;
            }
            while (cont);

            usedIds.Add(toReturn);
            return toReturn;
        }

        private void updateInfoBox(string p)
        {
            _view.SetInfoBox(p);
        }

        private void UpdatedServerIP(string v)
        {
            _view.SetIPBox(v);
        }

        private void setFrameBox(int i)
        {
            _view.SetFrameBox(i.ToString());
        }

    }
}