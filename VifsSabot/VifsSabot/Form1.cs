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
using System.Threading;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public string server, channel, nick, pass;
        public string newMsg, botMsg;
        string replyingUser, formatedMessage;
        char[] chatSeperator={' '};
        public int port;
        TcpClient IRCconnection = null;   
        NetworkStream ns = null;
        StreamReader reader = null;
        StreamWriter writer = null;
        bool botOnline = false;
        public string[] words;
        private BackgroundWorker bw = new BackgroundWorker();
        string LineFromReader = "";
        bool IsLineRead = true;
        Thread ReadStreamThread;
        CrazyChat Replies;
        string votingOptions;
        int voteSize, voteTickCount=0;
        bool activeVote = false;
        bool finishVote = false;
        //int[] votecount;
        VotingSystem Votes;
        List<string> modList;

        public Form1()
        {
            InitializeComponent();
            modList = new List<string>();
            Replies = new CrazyChat();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            //can set in the Design for the timer in properties
            //VotingTime.Interval = 10000;
        }

        private void but_join_Click(object sender, EventArgs e)
        {

            server = text_server.Text.ToString();
            port = Convert.ToInt32(text_port.Text.ToString());
            nick = text_user.Text.ToString();
            pass = text_pass.Text.ToString();
            channel = text_chan.Text.ToString();
            Votes = new VotingSystem();
            VifsbotInit();
        }

        //ran on a different thread, reads in from the stream and waits till the line is posted
        private void ReadIn()
        {
            while (true)
            {
                    if (IsLineRead && reader != null)
                    {
                        LineFromReader = reader.ReadLine();
                        if (LineFromReader != null)
                        {
                            IsLineRead = false;
                        }
                    }
                Thread.Sleep(UpdateText.Interval);
            }
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
                botOnline = true;
                DataSend("PASS", pass);
                DataSend("NICK", nick);
                DataSend("USER", nick);
                DataSend("JOIN", channel);
                bw.RunWorkerAsync();
                ReadStreamThread = new Thread(new ThreadStart(this.ReadIn));
                ReadStreamThread.Start();
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

        //private void bw_DoWork(object sender, DoWorkEventArgs e) //fucks up here
        //{                                                        //trying to have it run it the background so it wouldn't fuck the controls up
        //    char sperator = ' ';                                 //This infinte loop thing was dumb what was I thinking, anyway every iteration it
        //    while (botOnline)                                    //should exit out to report progress and update the chat log. Maybe replace this 
        //    {                                                    //this with a thing that every X seconds reads all the lines in the buffer processes 
        //        //newMsg = reader.ReadLine();                      //them and prints them? Idk fuck around with something. This is a terrible method holy shit.
        //        //bw.ReportProgress(1, newMsg);
        //        words = newMsg.Split(sperator);
        //        if (words[0] == "PING")
        //        {
        //            DataSend("PONG", words[1]); //responding to pings to twitch doesn't fuck you out. Needs to happen somewhere
        //        }
        //    }
        //}

        private void but_say_Click(object sender, EventArgs e)
        {
            botMsg = text_msg.Text.ToString();
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


        //timer that loops to post read in lines from the stream
        private void UpdateText_Tick(object sender, EventArgs e)
        {
            if (botOnline)
            {
                    if (!IsLineRead)
                    {

                        if (LineFromReader.Contains("PRIVMSG"))
                        {
                            WordSplitter();
                        }
                        else if (LineFromReader.Contains("PING"))
                        {
                            PingHandler();
                        }
                        else if ((LineFromReader.ToLower().Contains("mode "))&&(LineFromReader.ToLower().Contains(" +o")))
                        {
                            string[] tempMods;
                            tempMods = LineFromReader.Split(' ');
                            modList.Add(tempMods[tempMods.Length-1]);
                        }
                        else
                        {
                            chat_area.AppendText(LineFromReader + "\r\n");
                        }
                        IsLineRead = true;
                    }
                    if (activeVote)
                    {
                        VotingTime_Tick();
                    }
            }
        }

        private void WordSplitter()
        {    
            words = LineFromReader.Split(chatSeperator, 4);
            replyingUser = words[0].Remove(words[0].IndexOf('!')).TrimStart(':');
            formatedMessage = words[3].TrimStart(':');
            chat_area.AppendText("<" + replyingUser + "> " + formatedMessage + "\r\n");
            KeywordDetector();
        }

        private void KeywordDetector()
        {
            if (formatedMessage.ToLower().Contains(nick.ToLower())&&!activeVote)
            {
                var reply = Replies.giveReply(replyingUser);
                DataSend("PRIVMSG ", channel + ' ' + ":" + reply);
                writer.Flush();
                chat_area.AppendText("<TestSabot> " + reply + "\r\n");
            }
            if (!activeVote)
            {
                if ((modList.Contains(replyingUser.ToLower())) && formatedMessage.ToLower().StartsWith("vs"))
                {
                    Voting();
                }

            }
            if (activeVote)
            {
                if ((modList.Contains(replyingUser.ToLower())) && formatedMessage.ToLower().StartsWith("end vote"))
                {
                    finishVote = true;
                }
                for (int c=1; c<=voteSize; c++)
                {
                    if ((formatedMessage.Contains(c.ToString())&&(formatedMessage.Length==c.ToString().Length)))//something goes after this to filter messages with vote and text from false postiving
                    {
                        //votecount[c-1]++;
                        Votes.AddVote(c, replyingUser);
                    }
                }
            }
        }

        private void PingHandler()
        {
            words = LineFromReader.Split(chatSeperator);
            if (words[0] == "PING")
            {
                DataSend("PONG", words[1]); 
            }
        }

        private void Voting()
        {
            Votes.CreateNew();
            string[] tempVoteSetup = formatedMessage.Split(',');
            if (tempVoteSetup.Length == 1)
            {
                DataSend("PRIVMSG ", channel + ' ' + ":VOTE: 1, 2, 3, or 4");
                voteSize = 4;
                chat_area.AppendText("VOTING START");
            }
            else
            {
                votingOptions = "VOTE:";
                for (int c = 1; c <= tempVoteSetup.Length - 1; c++)
                {
                    votingOptions += (" " + c + " for " + tempVoteSetup[c] + ",");
                    Votes.AddVoteItem(c, tempVoteSetup[c]);
                }
                voteSize = tempVoteSetup.Length-1;
                votingOptions = votingOptions.TrimEnd(',');
                DataSend("PRIVMSG ", channel + " :" + votingOptions);
                chat_area.AppendText("VOTING START");
            }

            activeVote = true;

        }

        private void VotingTime_Tick(object sender, EventArgs e)
        {
                voteTickCount++;
                if (voteTickCount == 200)
                {
                    //DataSend("PRIVMSG ", channel + " :10 SECONDS LEFT TO VOTE");
                    SendMessage(" :10 SECONDS LEFT TO VOTE");
                }
                else if (voteTickCount == 300 || activeVote == false)
                {
                    SendMessage(Votes.ReturnVotes());
                    activeVote = false;
                    voteTickCount = 0;
                }
        }

        private void VotingTime_Tick()
        {
            if(finishVote){
            //voteTickCount++;
            //if (voteTickCount == 200)
            //{
            //    //DataSend("PRIVMSG ", channel + " :10 SECONDS LEFT TO VOTE");
            //    SendMessage(" :10 SECONDS LEFT TO VOTE");
            //}
            //else if (voteTickCount == 300 || finishVote)
            //{
                SendMessage(Votes.ReturnVotes());
                activeVote = false;
                finishVote = false;
                voteTickCount = 0;
            }
        }

        private void SendMessage(string message)
        {
            DataSend("PRIVMSG ", channel + " :" + message);
        }
    }
}
    


