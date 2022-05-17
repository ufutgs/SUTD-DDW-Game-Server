using System;
using System.Collections.Generic;
using System.Text;
namespace SUTD_UROP
{
    class Room
    {
        private int size;
        private List<int> sequence = new List<int>();
        private List<int> random_sequence;
        public Dictionary<int,bool> Participant = new Dictionary<int, bool>();
        public Dictionary<int, Nullable<float>> result = new Dictionary<int, Nullable<float>>();
        public List<float> sorting_result = new List<float>();
        public string sequenceString="";
        private long time;
        public Room(int size=20)
        {
            this.size = size;
            rng();
        }
        private void rng()
        {
            for (int i = 0; i < size; i++)
            {
                sequence.Add(i + 1);
            }
            var rand = new Random();
            random_sequence = new List<int>(sequence);
            for (int j = 0; j < size; j++)
            {
                int chosen = rand.Next(j, sequence.Count);
                int change = random_sequence[j];
                random_sequence[j] = random_sequence[chosen];
                random_sequence[chosen] = change;
                if (j != random_sequence.Count - 1)
                { sequenceString += (random_sequence[j].ToString() + ","); }
                else
                {
                    sequenceString += random_sequence[j].ToString();
                }
            }
        }

        public void sendResult()
        {
            if (!result.ContainsValue(null))
            {
                sorting_result.Sort();
            }
        }
        public bool checkStatus(int _fromClient)
        {
            Participant[_fromClient] = true;
            if (Participant.ContainsValue(false))
            {
                return false;
            }
            return true;
        }

        public void recordTime()
        {
            time = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }
        public int getSize()
        { return size; }
    }
}
