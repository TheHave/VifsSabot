using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class VotingSystem
    {
        public Dictionary<int, VoteInfo> Votes;
        List<string> voters;

        public void CreateNew()
        {
            Votes = new Dictionary<int, VoteInfo>();
            voters = new List<string>();
        }

        public void AddVoteItem(int position, string vote)
        {
            VoteInfo temp = new VoteInfo();
            temp.Counts = 0;
            temp.Vote = vote;
            Votes.Add(position, temp);
        }

        public void AddVote(int position, string user)
        {
            if (!voters.Contains(user))
            {
                var tempCount = Votes[position].Counts;
                tempCount++;
                var tempVoteInfo = Votes[position];
                tempVoteInfo.Counts = tempCount;
                Votes[position] = tempVoteInfo;
                voters.Add(user);
            }
        }

        private void OrderVotes()
        {
            Votes.OrderBy(s => s.Value.Counts);
        }

        public string ReturnVotes()
        {
            OrderVotes();
            string returnValue = "";
            foreach (var item in Votes)
            {
                returnValue += item.Value.Vote + " with " + item.Value.Counts + " votes,";
            }
            returnValue = returnValue.TrimEnd(',');
            return returnValue;
        }
    }

    struct VoteInfo
    {
        public int Counts;
        public string Vote;
    }
}
