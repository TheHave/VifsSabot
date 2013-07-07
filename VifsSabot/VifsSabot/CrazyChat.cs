using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WindowsFormsApplication1
{
    class CrazyChat
    {
        private List<string> Replies;
        private static Random rnd;

        public CrazyChat()
        {
            Replies = new List<string>();
            rnd = new Random();
            readInReplies();
        }


        private void readInReplies(){
            var reader = new StreamReader("../../Reply.txt");
            while(reader.Peek() >= 0){
                Replies.Add(reader.ReadLine());
            }
        }

        public string giveReply()
        {
            int r = rnd.Next(Replies.Count);
            return Replies[r];

        }
    }

    
}
