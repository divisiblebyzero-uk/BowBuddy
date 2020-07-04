﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BowBuddy.Model
{
    public class ScoreSheet
    {
        public const string BowTypeRecurve = "Recurve";
        public const string BowTypeCompound = "Compound";
        public const string BowTypeLongbow = "Longbow";
        public const string BowTypeBarebow = "Barebow";
        public const string AgeGroupAdult = "Adult";
        public const string AgeGroupU18 = "U18";
        public const string AgeGroupU16 = "U16";
        public const string AgeGroupU14 = "U14";
        public const string AgeGroupU12 = "U12";
        public const string GenderMale = "Male";
        public const string GenderFemale = "Female";
        public const string Classification3rd = "3rd";
        public const string Classification2nd = "2nd";
        public const string Classification1st = "1st";
        public const string ClassificationBow = "Bow";
        public const string ClassificationMB = "MB";
        public const string ClassificationGMB = "GMB";

        public DateTime Date { get; set; }
        public string RoundName { get; set; }
        public List<Dozen> Dozens { get; set; }
        public Total Total { get; set; } 
        public int Handicap { get; set; }
        public string Gender { get; set; }
        public string AgeGroup { get; set; } = AgeGroupAdult;
        public string BowType { get; set; }
        public string Classification { get; set; }
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
