using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BowBuddy.Model;

namespace BowBuddy.Service
{
    /**
     * Interpreted from: http://www.roystonarchery.org/new/wp-content/uploads/2013/09/Graduated-Handicap-Tables.pdf?e632e9
     */
    public class HandicapCalculationService
    {

        private static readonly Lazy<HandicapCalculationService> instance = new Lazy<HandicapCalculationService>(() => new HandicapCalculationService());
        public static HandicapCalculationService Instance => instance.Value;

        private HandicapCalculationService()
        {

        }

        private Dictionary<string, List<(int, int)>> HandicapTables = new Dictionary<string, List<(int, int)>>();

        public int CalculateHandicap(Round round, int score)
        {
            List<(int handicap, int score)> handicapTable = GetHandicapTable(round);
            var value = handicapTable.FirstOrDefault(h => h.score <= score);
            if (value.handicap == 0 && value.score == 0)
            {
                return 100;
            }
            else
            {
                return value.handicap;
            }

        }

        public List<(int handicap, int score)> GetHandicapTable(Round round)
        {
            if (HandicapTables.ContainsKey(round.Name))
            {
                return HandicapTables[round.Name];
            }

            List<(int handicap, int score)> handicapTable = new List<(int, int)>();
            for (int i = 0; i < 101; i++)
            {
                handicapTable.Add((i, CalculateScoreForHandicap(i, round)));
            }

            HandicapTables[round.Name] = handicapTable;
            return handicapTable;
        }

        private int CalculateScoreForHandicap(int handicap, Round round)
        {
            double runningTotal = 0d;
            foreach (RoundDistance distance in round.Distances)
            {
                runningTotal += distance.Arrows * CalculateScoreForHandicap(handicap, distance, round.Scoring);
            }
            return Convert.ToInt32(runningTotal);
        }

        private double CalculateScoreForHandicap(int handicap, RoundDistance distance, string scoring)
        {
            double d = 0.0D;
            double rootMeanSquare = GetRootMeanSquare(handicap, distance);
            double averageScore = d;

            if (scoring == Round.ScoringStyleImperial)
            {
                for (int i = 1; i <= 4; i++)
                {
                    
                    averageScore += Math.Exp(-Math.Pow(i * distance.FaceSize.InCentimetres() / 10.0D + 0.357D, 2.0D) / Math.Pow(rootMeanSquare, 2.0D));
                }
                return 9.0D - 2.0D * averageScore - Math.Exp(-Math.Pow(distance.FaceSize.InCentimetres() / 2.0D + 0.357D, 2.0D) / Math.Pow(rootMeanSquare, 2.0D));
            }
            else if (scoring == Round.ScoringStyleMetric)
            {
                for (int i = 1; i <= 10; i++)
                {
                    averageScore += Math.Exp(-Math.Pow(i * distance.FaceSize.InCentimetres() / 20.0D + 0.357D, 2.0D) / Math.Pow(rootMeanSquare, 2.0D));
                }
                return 10.0D - averageScore;
            }
            else if (scoring == Round.ScoringStyleWorcester)
            {
                for (int i = 1; i <= 5; i++)
                {
                    averageScore += Math.Exp(-Math.Pow(i * distance.FaceSize.InCentimetres() / 10.0D + 0.357D, 2.0D) / Math.Pow(rootMeanSquare, 2.0D));
                }
                return 5.0D - averageScore;
            }
            else
            {
                
                return 0d;
            }
        }

        private double GetRootMeanSquare(int handicap, RoundDistance distance)
        {
            double rangeInMetres = distance.Distance.InMetres();
            return 100.0D * rangeInMetres * Math.Pow(1.036D, 12.9D + handicap) * 5.0D * Math.Pow(10.0D, -4.0D) * (1.0D + 1.429D * Math.Pow(10.0D, -6.0D) * Math.Pow(1.07D, 4.3D + handicap) * Math.Pow(rangeInMetres, 2.0D));
        }
    }
}
