using System;
using System.Collections.Generic;
using System.Text;

namespace BowBuddy.Model
{
    public class Round
    {
        public string Name { get; set; }
        public Measurement FaceSize { get; set; } = new Measurement{Value = 122, Units = "cm"};
        public bool Indoor { get; set; } = false;
        public bool Imperial { get; set; } = true;
        public RoundDistance[] Distances { get; set; }

        public static Round JuniorNational = new Round
        {
            Name = "JuniorNational",
            Distances = new RoundDistance[2]
            {
                new RoundDistance {Distance = new Measurement {Value = 40, Units = "yds"}, Arrows = 48},
                new RoundDistance {Distance = new Measurement {Value = 30, Units = "yds"}, Arrows = 24}
            }
        };

        public static Round National = new Round
        {
            Name = "National",
            Distances = new RoundDistance[2]
            {
                new RoundDistance {Distance = new Measurement {Value = 60, Units = "yds"}, Arrows = 48},
                new RoundDistance {Distance = new Measurement {Value = 50, Units = "yds"}, Arrows = 24}
            }
        };
    }

    public class Measurement
    {
        public int Value { get; set; }
        public string Units { get; set; }
    }

    public class RoundDistance
    {
        public Measurement Distance { get; set; }
        public int Arrows { get; set; }
    }
}
