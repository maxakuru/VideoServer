using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SE3314Assignment1
{
    public partial class View : Form
    {
        Controller _controller;
        public View()
        {
            InitializeComponent();
            _controller = new Controller();
            this.textBox_clientRequests.ScrollBars = ScrollBars.Vertical;
            this.textBox_clientRequests.ReadOnly = true;
            this.textBox_serverStatus.ScrollBars = ScrollBars.Vertical;
            this.textBox_serverStatus.ReadOnly = true;
        }

        public bool displayRtpHeader()
        {
            return checkBox_print.Checked;
        }

        private void getState()
        {
            //_controller.getState();
        }

        public void SetIPBox(String _msg)
        {
            string text = _msg;
            SetInfoCallback d = new SetInfoCallback(set_IP);
            this.Invoke(d, new Object[] { text });

        }
        public void set_IP(String _msg)
        {
            this.textBox_IP.Text = _msg;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _controller.listenButton_btn_Click(sender, e);
        }

        internal void Disable_listenButton()
        {
            this.button_listen.Enabled = false;
        }

        internal string GetPortNumber()
        {
            return this.textBox_port.Text;
        }

        delegate void SetInfoCallback(string info);

        public void SetInfoBox(String _msg)
        {
            string text = _msg;
            SetInfoCallback d = new SetInfoCallback(add_text);
            this.Invoke(d, new Object[] { text });

        }
        public void add_text(String _msg)
        {
            this.textBox_serverStatus.Text += _msg;
        }

        public void SetClientBox(String _msg)
        {
            string text = _msg;
            SetInfoCallback d = new SetInfoCallback(add_client_text);
            this.Invoke(d, new Object[] { text });

        }

        public void SetFrameBox(String _msg)
        {
            string text = _msg;
            SetInfoCallback d = new SetInfoCallback(setFrameNumber);
            this.Invoke(d, new Object[] { text });

        }
        public void setFrameNumber(String num)
        {
            this.textBox_frame.Text = num;
        }

        public void add_client_text(String _msg)
        {
            this.textBox_clientRequests.Text += _msg;
        }
    }
}
