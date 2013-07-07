namespace WindowsFormsApplication1
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.text_user = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.text_pass = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.text_server = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.text_chan = new System.Windows.Forms.TextBox();
            this.but_join = new System.Windows.Forms.Button();
            this.but_part = new System.Windows.Forms.Button();
            this.text_msg = new System.Windows.Forms.TextBox();
            this.but_say = new System.Windows.Forms.Button();
            this.chat_area = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.text_port = new System.Windows.Forms.TextBox();
            this.UpdateText = new System.Windows.Forms.Timer(this.components);
            this.VotingTime = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username:";
            // 
            // text_user
            // 
            this.text_user.Location = new System.Drawing.Point(76, 6);
            this.text_user.Name = "text_user";
            this.text_user.Size = new System.Drawing.Size(76, 20);
            this.text_user.TabIndex = 1;
            this.text_user.Text = "TestSabot";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(158, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password:";
            // 
            // text_pass
            // 
            this.text_pass.Location = new System.Drawing.Point(220, 6);
            this.text_pass.Name = "text_pass";
            this.text_pass.PasswordChar = '*';
            this.text_pass.Size = new System.Drawing.Size(79, 20);
            this.text_pass.TabIndex = 3;
            this.text_pass.Text = "testpass";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(305, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Server:";
            // 
            // text_server
            // 
            this.text_server.Location = new System.Drawing.Point(352, 6);
            this.text_server.Name = "text_server";
            this.text_server.Size = new System.Drawing.Size(116, 20);
            this.text_server.TabIndex = 5;
            this.text_server.Text = "irc.twitch.tv";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(474, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Channel:";
            // 
            // text_chan
            // 
            this.text_chan.Location = new System.Drawing.Point(529, 6);
            this.text_chan.Name = "text_chan";
            this.text_chan.Size = new System.Drawing.Size(71, 20);
            this.text_chan.TabIndex = 7;
            this.text_chan.Text = "#vifs";
            // 
            // but_join
            // 
            this.but_join.Location = new System.Drawing.Point(684, 6);
            this.but_join.Name = "but_join";
            this.but_join.Size = new System.Drawing.Size(48, 19);
            this.but_join.TabIndex = 8;
            this.but_join.Text = "Join";
            this.but_join.UseVisualStyleBackColor = true;
            this.but_join.Click += new System.EventHandler(this.but_join_Click);
            // 
            // but_part
            // 
            this.but_part.Location = new System.Drawing.Point(738, 5);
            this.but_part.Name = "but_part";
            this.but_part.Size = new System.Drawing.Size(46, 20);
            this.but_part.TabIndex = 9;
            this.but_part.Text = "Part";
            this.but_part.UseVisualStyleBackColor = true;
            this.but_part.Click += new System.EventHandler(this.but_part_Click);
            // 
            // text_msg
            // 
            this.text_msg.Location = new System.Drawing.Point(12, 385);
            this.text_msg.Name = "text_msg";
            this.text_msg.Size = new System.Drawing.Size(707, 20);
            this.text_msg.TabIndex = 11;
            // 
            // but_say
            // 
            this.but_say.Location = new System.Drawing.Point(725, 385);
            this.but_say.Name = "but_say";
            this.but_say.Size = new System.Drawing.Size(59, 21);
            this.but_say.TabIndex = 12;
            this.but_say.Text = "Say";
            this.but_say.UseVisualStyleBackColor = true;
            this.but_say.Click += new System.EventHandler(this.but_say_Click);
            // 
            // chat_area
            // 
            this.chat_area.BackColor = System.Drawing.Color.White;
            this.chat_area.DetectUrls = false;
            this.chat_area.Location = new System.Drawing.Point(11, 32);
            this.chat_area.Name = "chat_area";
            this.chat_area.ReadOnly = true;
            this.chat_area.Size = new System.Drawing.Size(773, 344);
            this.chat_area.TabIndex = 13;
            this.chat_area.Text = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(606, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Port:";
            // 
            // text_port
            // 
            this.text_port.Location = new System.Drawing.Point(641, 6);
            this.text_port.Name = "text_port";
            this.text_port.Size = new System.Drawing.Size(37, 20);
            this.text_port.TabIndex = 15;
            this.text_port.Text = "6667";
            // 
            // UpdateText
            // 
            this.UpdateText.Enabled = true;
            this.UpdateText.Tick += new System.EventHandler(this.UpdateText_Tick);
            // 
            // VotingTime
            // 
            this.VotingTime.Enabled = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 417);
            this.Controls.Add(this.text_port);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.chat_area);
            this.Controls.Add(this.but_say);
            this.Controls.Add(this.text_msg);
            this.Controls.Add(this.but_part);
            this.Controls.Add(this.but_join);
            this.Controls.Add(this.text_chan);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.text_server);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.text_pass);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.text_user);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Vifs Sabot Control Panel";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox text_user;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox text_pass;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox text_server;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox text_chan;
        private System.Windows.Forms.Button but_join;
        private System.Windows.Forms.Button but_part;
        private System.Windows.Forms.TextBox text_msg;
        private System.Windows.Forms.Button but_say;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox text_port;
        private System.Windows.Forms.RichTextBox chat_area;
        private System.Windows.Forms.Timer UpdateText;
        public System.Windows.Forms.Timer VotingTime;
    }
}

