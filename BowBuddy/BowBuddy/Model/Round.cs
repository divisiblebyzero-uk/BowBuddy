﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BowBuddy.Model
{
    public class Round
    {
        public const string ScoringStyleImperial = "Imperial";
        public const string ScoringStyleMetric = "Metric";
        public const string ScoringStyleWorcester = "Worcester";
        public const string VenueIndoor = "Indoor";
        public const string VenueOutdoor = "Outdoor";
        public const string UnitsCentimetres = "cm";
        public const string UnitsMetres = "m";
        public const string UnitsYards = "yd";
        public const string UnitsInches = "in";

        public string Name { get; set; }
        public string Venue { get; set; } = VenueOutdoor;
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
                case Round.UnitsCentimetres:
                    return ((double)Value) / 100;
                case Round.UnitsMetres:
                    return ((double)Value);
                case Round.UnitsYards:
                    return ((double)Value) * 0.9144;
                case Round.UnitsInches:
                    return ((double) Value) * 0.0254;
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
        public List<string> RoundNames => RoundRegistry.Instance.Rounds.Keys.ToList();

        private string[] ScoreOptionsImperial = new[] { "9", "7", "5", "3", "1", "M" };
        private string[] ScoreOptionsMetric = new[] { "X", "9", "8", "7", "6", "5", "4", "3", "2", "1", "M" };
        private string[] ScoreOptionsWorcester = new[] { "5", "4", "3", "2", "1", "M" };

        public string[] ScoreOptions(Round round)
        {
            return ScoreOptions(round.Scoring);
        }

        public string[] ScoreOptions(string scoringStyle)
        {
            switch (scoringStyle)
            {
                case Round.ScoringStyleImperial:
                    return ScoreOptionsImperial;
                case Round.ScoringStyleMetric:
                    return ScoreOptionsMetric;
                case Round.ScoringStyleWorcester:
                    return ScoreOptionsWorcester;
                default:
                    return null;
            }
            
        }

        private void PopulateRegistry()
        {
            Rounds["Junior National"] = new Round
            {
                Name = "Junior National",
                Distances = new RoundDistance[2]
                {
                    new RoundDistance {Distance = new Measurement {Value = 40, Units = Round.UnitsYards}, Arrows = 48, FaceSize = new Measurement{Value = 122, Units = Round.UnitsCentimetres}},
                    new RoundDistance {Distance = new Measurement {Value = 30, Units = Round.UnitsYards}, Arrows = 24, FaceSize = new Measurement{Value = 122, Units = Round.UnitsCentimetres}}
                }
            };

            Rounds["Short National"] = new Round
            {
                Name = "Short National",
                Distances = new RoundDistance[2]
                {
                    new RoundDistance {Distance = new Measurement {Value = 50, Units = Round.UnitsYards}, Arrows = 48, FaceSize = new Measurement{Value = 122, Units = Round.UnitsCentimetres}},
                    new RoundDistance {Distance = new Measurement {Value = 40, Units = Round.UnitsYards}, Arrows = 24, FaceSize = new Measurement{Value = 122, Units = Round.UnitsCentimetres}}
                }
            };

            Rounds["National"] = new Round
            {
                Name = "National",
                Distances = new RoundDistance[2]
                {
                    new RoundDistance {Distance = new Measurement {Value = 60, Units = Round.UnitsYards}, Arrows = 48, FaceSize = new Measurement{Value = 122, Units = Round.UnitsCentimetres}},
                    new RoundDistance {Distance = new Measurement {Value = 50, Units = Round.UnitsYards}, Arrows = 24, FaceSize = new Measurement{Value = 122, Units = Round.UnitsCentimetres}}
                }
            };

            Rounds["Portsmouth"] = new Round
            {
                Name = "Portsmouth",
                Distances = new RoundDistance[1]
                {
                    new RoundDistance {Distance = new Measurement {Value = 20, Units = Round.UnitsYards}, Arrows = 60, FaceSize = new Measurement{Value = 60, Units = Round.UnitsCentimetres}}
                },
                Scoring = Round.ScoringStyleMetric,
                Venue = Round.VenueIndoor
            };

            Rounds["Worcester"] = new Round
            {
                Name = "Worcester",
                Distances = new RoundDistance[1]
                {
                    new RoundDistance {Distance = new Measurement {Value = 20, Units = Round.UnitsYards}, Arrows = 60, FaceSize = new Measurement{Value = 16, Units = Round.UnitsInches}}
                },
                Scoring = Round.ScoringStyleWorcester,
                Venue = Round.VenueIndoor
            };

            Rounds["Frostbite"] = new Round
            {
                Name = "Frostbite",
                Distances = new RoundDistance[1]
                {
                    new RoundDistance {Distance = new Measurement {Value = 30, Units = Round.UnitsMetres}, Arrows = 36, FaceSize = new Measurement{Value = 80, Units = Round.UnitsCentimetres}}
                },
                Scoring = Round.ScoringStyleMetric
            };

            Rounds["Shop Range"] = new Round
            {
                Name = "Shop Range",
                Distances = new RoundDistance[1]
                {
                    new RoundDistance
                    {
                        Distance = new Measurement {Value = 12, Units = Round.UnitsYards}, Arrows = 6,
                        FaceSize = new Measurement {Value = 60, Units = Round.UnitsCentimetres}
                    }
                },
                Venue = Round.VenueOutdoor
            };

        }
    }
}
