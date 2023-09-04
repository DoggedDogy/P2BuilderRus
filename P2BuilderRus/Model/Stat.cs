using System;

namespace P2BuiderRus.Model
{
    public class Stat
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public int Modifier { get; set; }
        public Stat(string Name, int Score)
        {
            this.Name = Name;
            this.Score = Score;
            this.Modifier = (int)Math.Floor((double)(Score - 10) / 2);
        }
        public void Update(int plus)
        {
            Score += plus;
            Modifier = (int)Math.Floor((double)(Score - 10) / 2);
        }
    }
}