using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE3314Assignment1
{
    class RTSPTimer : System.Timers.Timer
    {
        private int clientId;
        private int val = 80;

        public RTSPTimer(int id)
        {
            this.clientId = id;
            this.Interval = val;
            //this.Enabled = true;
        }

        public int getId()
        {
            return this.clientId;
        }

        public void setId(int id)
        {
            this.clientId = id;
        }


        internal void Starttime()
        {
            this.Enabled = true;
        }

        internal void Stoptime()
        {
            this.Enabled = false;
        }
    }
}
