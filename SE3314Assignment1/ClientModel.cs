using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE3314Assignment1
{
    class ClientModel
    {
        private int ID;
        private RTSPTimer _RTSPTimer;
        MJPEGVideo video;
        private RTPmodel _RTPModel;
        int interv = 80;
        private int seqNo;
        private int rand;
        private int port;

        public ClientModel(int rand, MJPEGVideo video, RTPmodel _RTPModel)
        {
            //passed an RTPmodel object
            seqNo = 1;
            ID = rand;
            this.video = video;
            _RTSPTimer = new RTSPTimer(rand);
            _RTSPTimer.Interval = interv;
            this._RTPModel = _RTPModel;
        }

        public ClientModel(int rand, MJPEGVideo video, int port)
        {
            // passed the port number, make the RTPmodel here
            seqNo = 1;
            ID = rand;
            this.video = video;
            _RTSPTimer = new RTSPTimer(rand);
            _RTSPTimer.Interval = interv;
            this._RTPModel = new RTPmodel(port);
        }

        public int getSeqNo()
        {
            return seqNo;
        }

        public void incrementSeqNo()
        {
            seqNo++;
        }

        public void resetSeqNo()
        {
            seqNo = 1;
        }

        public RTPmodel getRtpModel()
        {
            return _RTPModel;
        }

        public MJPEGVideo getVideo()
        {
            return this.video;
        }

        public void setVideo(MJPEGVideo v)
        {
            this.video = v;
        }

        internal void setId(int rand)
        {
            ID = rand;
        }

        public int getId()
        {
            return ID;
        }

        public void startTimer()
        {
            //this._RTSPTimer.Enabled = true;
            this._RTSPTimer.Starttime();
            
        }

        public void stopTimer()
        {
            this._RTSPTimer.Stoptime();
        }

        public RTSPTimer getTimer()
        {
            return _RTSPTimer;
        }

    }
}
