namespace SE3314Assignment1
{
    partial class View
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_listen = new System.Windows.Forms.Button();
            this.checkBox_print = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.textBox_IP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_frame = new System.Windows.Forms.TextBox();
            this.textBox_serverStatus = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_clientRequests = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button_listen
            // 
            this.button_listen.Location = new System.Drawing.Point(257, 60);
            this.button_listen.Name = "button_listen";
            this.button_listen.Size = new System.Drawing.Size(95, 40);
            this.button_listen.TabIndex = 0;
            this.button_listen.Text = "Listen";
            this.button_listen.UseVisualStyleBackColor = true;
            this.button_listen.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox_print
            // 
            this.checkBox_print.AutoSize = true;
            this.checkBox_print.Location = new System.Drawing.Point(460, 70);
            this.checkBox_print.Name = "checkBox_print";
            this.checkBox_print.Size = new System.Drawing.Size(159, 24);
            this.checkBox_print.TabIndex = 1;
            this.checkBox_print.Text = "Print RTP Header";
            this.checkBox_print.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Listen on Port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Server IP address";
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(151, 67);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(100, 26);
            this.textBox_port.TabIndex = 4;
            // 
            // textBox_IP
            // 
            this.textBox_IP.Enabled = false;
            this.textBox_IP.Location = new System.Drawing.Point(167, 147);
            this.textBox_IP.Name = "textBox_IP";
            this.textBox_IP.Size = new System.Drawing.Size(100, 26);
            this.textBox_IP.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(433, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Frame #";
            // 
            // textBox_frame
            // 
            this.textBox_frame.Enabled = false;
            this.textBox_frame.Location = new System.Drawing.Point(507, 144);
            this.textBox_frame.Name = "textBox_frame";
            this.textBox_frame.Size = new System.Drawing.Size(100, 26);
            this.textBox_frame.TabIndex = 7;
            // 
            // textBox_serverStatus
            // 
            this.textBox_serverStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.textBox_serverStatus.Location = new System.Drawing.Point(57, 291);
            this.textBox_serverStatus.Multiline = true;
            this.textBox_serverStatus.Name = "textBox_serverStatus";
            this.textBox_serverStatus.Size = new System.Drawing.Size(562, 212);
            this.textBox_serverStatus.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(53, 257);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Server Status";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(53, 558);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "Client Requests";
            // 
            // textBox_clientRequests
            // 
            this.textBox_clientRequests.Location = new System.Drawing.Point(57, 591);
            this.textBox_clientRequests.Multiline = true;
            this.textBox_clientRequests.Name = "textBox_clientRequests";
            this.textBox_clientRequests.Size = new System.Drawing.Size(562, 230);
            this.textBox_clientRequests.TabIndex = 11;
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 921);
            this.Controls.Add(this.textBox_clientRequests);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_serverStatus);
            this.Controls.Add(this.textBox_frame);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_IP);
            this.Controls.Add(this.textBox_port);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox_print);
            this.Controls.Add(this.button_listen);
            this.Name = "View";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_listen;
        private System.Windows.Forms.CheckBox checkBox_print;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.TextBox textBox_IP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_frame;
        private System.Windows.Forms.TextBox textBox_serverStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_clientRequests;
    }
}

