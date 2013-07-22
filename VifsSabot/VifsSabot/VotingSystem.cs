using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class VotingSystem
    {
        public Dictionary<int, VoteInfo> Votes;
        public Dictionary<string, int> voters;
        //List<string> voters;

        public void CreateNew()
        {
            Votes = new Dictionary<int, VoteInfo>();
            voters = new Dictionary<string, int>();
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
            if (!voters.ContainsKey(user))
            {
                //var tempCount = Votes[position].Counts;
                //tempCount++;
                //var tempVoteInfo = Votes[position];
                //tempVoteInfo.Counts = tempCount;
                //Votes[position] = tempVoteInfo;
                CountVote(position, 1);
                voters.Add(user, position);
            }
            else
            {
                changeVote(position, user);
            }
        }

        private void changeVote(int position, string user)
        {
            int oldVote = voters[user];
            CountVote(oldVote, -1);
            CountVote(position, 1);
            voters[user] = position;
        }

        private void CountVote(int position, int addedVote)
        {
            var tempCount = Votes[position].Counts;
            tempCount += addedVote;
            var tempVoteInfo = Votes[position];
            tempVoteInfo.Counts = tempCount;
            Votes[position] = tempVoteInfo;
        }

        private List<VoteInfo> OrderVotes()
        {
            return Votes.OrderByDescending(s => s.Value.Counts).Select(o => o.Value).ToList();
        }

        public string ReturnVotes()
        {
            var tempList = OrderVotes();
            string returnValue = "";
            foreach (var item in tempList)
            {
                returnValue += item.Vote + " with " + item.Counts + " votes,";
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
