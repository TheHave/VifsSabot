using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;               
using System.Net.Sockets;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public string server, channel, nick, pass;
        public string newMsg, botMsg;
        public int port;
        TcpClient IRCconnection = null;   
        NetworkStream ns = null;
        StreamReader reader = null;
        StreamWriter writer = null;
        bool botOnline = true;
        public string[] words;
        private BackgroundWorker bw = new BackgroundWorker();
        Regex nameExtractor =new Regex("!*:");

        public Form1()
        {
            InitializeComponent();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
        }

        private void but_join_Click(object sender, EventArgs e)
        {

            server = text_server.Text.ToString();
            port = Convert.ToInt32(text_port.Text.ToString());
            nick = text_user.Text.ToString();
            pass = text_pass.Text.ToString();
            channel = text_chan.Text.ToString();
            VifsbotInit();
        }

        public void VifsbotInit() 
        {
            try
            {
                IRCconnection = new TcpClient(server, port);
            }
            catch
            {
                chat_area.AppendText("Connection Failed");     
            }
            try
            {
                ns = IRCconnection.GetStream();
                reader = new StreamReader(ns);
                writer = new StreamWriter(ns);
                DataSend("PASS", pass);
                DataSend("NICK", nick);
                DataSend("USER", nick);
                DataSend("JOIN", channel);
                bw.RunWorkerAsync();
            }
            catch
            {
                chat_area.AppendText("Communication Error");
            }
            finally
            {
                if (reader == null)
                {
                    reader.Close();
                }
                if (writer == null)
                {
                    writer.Close();
                } 
                if (ns == null)
                {
                    ns.Close();
                }
            }
        }

        public void DataSend(string cmd, string param)
        {
            if (param == null)
            {
                writer.WriteLine(cmd);
                writer.Flush();
            }
            else
            {
                writer.WriteLine(cmd + ' ' + param);
                writer.Flush();
            }
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            char sperator = ' ';
            while (botOnline)
            {
                newMsg = reader.ReadLine();
                bw.ReportProgress(1, newMsg);
                words = newMsg.Split(sperator);
                if (words[0] == "PING")
                {
                    DataSend("PONG", words[1]);
                }
            }
        }

        private void but_say_Click(object sender, EventArgs e)
        {
            botMsg=text_msg.Text.ToString();
            DataSend("PRIVMSG ", channel + ' ' +":"+ botMsg);
            writer.Flush();
            chat_area.AppendText("<TestSabot> "+botMsg + "\r\n");
            text_msg.Clear();
        }

        private void but_part_Click(object sender, EventArgs e)
        {
            DataSend("PART", channel);
            botOnline = false;
            bw.CancelAsync();
        }

        private void bw_ProgressChanged(Object sender, ProgressChangedEventArgs e)
        {
            chat_area.AppendText(e.UserState.ToString());
        }
        
    }
}
    


