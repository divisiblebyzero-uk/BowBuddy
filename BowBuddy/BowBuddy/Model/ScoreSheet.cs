using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BowBuddy.Model
{
    public class ScoreSheet
    {
        public DateTime Date { get; set; }
        public string RoundName { get; set; }
        public List<Dozen> Dozens { get; set; }
        public Total Total { get; set; } 
        public int Handicap { get; set; }
    }

    public class End
    {
        public string[] Scores { get; set; }
        public int EndTotal { get; set; }
    }

    public class Dozen
    {
        public List<End> Ends { get; set; }
        public Total Total { get; set; }
    }

    public class Total
    {
        public int Score { get; set; }
        public int Hits { get; set; }
        public int Golds { get; set; }
        public int RunningTotal { get; set; }
    }
    
}
