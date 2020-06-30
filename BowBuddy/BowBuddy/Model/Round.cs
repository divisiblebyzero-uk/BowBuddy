using System;
using System.Collections.Generic;
using System.Text;

namespace BowBuddy.Model
{
    public class Round
    {
        public const string ScoringStyleImperial = "Imperial";
        public const string ScoringStyleMetric = "Metric";
        public string Name { get; set; }
        public string Venue { get; set; } = "Outdoor";
        public string Scoring { get; set; } = ScoringStyleImperial;
        public RoundDistance[] Distances { get; set; }
    }

    public class Measurement
    {
        public int Value { get; set; }
        public string Units { get; set; }
        public string DisplayString => $"{Value} {Units}";

        public double InMetres()
        {
            switch (Units)
            {
                case "cm":
                    return ((double)Value) / 100;
                case "m":
                    return ((double)Value);
                case "yds":
                    return ((double)Value) * 0.9144;
                default:
                    Console.WriteLine($"Unknown units {Units}");
                    return 0;
            }
        }

        public double InCentimetres()
        {
            return InMetres() * 100;
        }
    }

    public class RoundDistance
    {
        public Measurement Distance { get; set; }
        public int Arrows { get; set; }
        public Measurement FaceSize { get; set; }
        public string DisplayString => $"{Distance.DisplayString} x {Arrows} ({FaceSize.DisplayString})";
    }

    public sealed class RoundRegistry
    {
        private RoundRegistry()
        {
            PopulateRegistry();
        }

        private static readonly Lazy<RoundRegistry> instance = new Lazy<RoundRegistry>(() => new RoundRegistry());
        public static RoundRegistry Instance => instance.Value;

        public Dictionary<string, Round> Rounds = new Dictionary<string, Round>();

        private void PopulateRegistry()
        {
            Rounds["Junior National"] = new Round
            {
                Name = "Junior National",
                Distances = new RoundDistance[2]
                {
                    new RoundDistance {Distance = new Measurement {Value = 40, Units = "yds"}, Arrows = 48, FaceSize = new Measurement{Value = 122, Units = "cm"}},
                    new RoundDistance {Distance = new Measurement {Value = 30, Units = "yds"}, Arrows = 24, FaceSize = new Measurement{Value = 122, Units = "cm"}}
                }
            };

            Rounds["National"] = new Round
            {
                Name = "National",
                Distances = new RoundDistance[2]
                {
                    new RoundDistance {Distance = new Measurement {Value = 60, Units = "yds"}, Arrows = 48, FaceSize = new Measurement{Value = 122, Units = "cm"}},
                    new RoundDistance {Distance = new Measurement {Value = 50, Units = "yds"}, Arrows = 24, FaceSize = new Measurement{Value = 122, Units = "cm"}}
                }
            };
        }
    }
}
