using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BowBuddy.Model;

namespace BowBuddy.Service
{
    public class ScoreCalculationService
    {
        private readonly Dictionary<(string ageGroup, string gender, string bowType),
            List<(string classification, int minimumHandicap)>> ClassificationBoundaries = new Dictionary<(string ageGroup, string gender, string bowType), List<(string classification, int minimumHandicap)>>();

        public ScoreCalculationService()
        {
            PopulateClassificationBoundaries();
        }
        private void PopulateClassificationBoundaries()
        { 

            ClassificationBoundaries[(ScoreSheet.AgeGroupAdult, ScoreSheet.GenderMale, ScoreSheet.BowTypeRecurve)] = new List<(string classification, int minimumHandicap)>
            {
                (ScoreSheet.Classification3rd, 58),
                (ScoreSheet.Classification2nd, 50),
                (ScoreSheet.Classification1st, 44),
                (ScoreSheet.ClassificationBow, 36),
                (ScoreSheet.ClassificationMB, 28),
                (ScoreSheet.ClassificationGMB, 22)
            };
        }

        
        public void CalculateScores(ScoreSheet scoreSheet)
        {
            
            if (!RoundRegistry.Instance.Rounds.ContainsKey(scoreSheet.RoundName))
            {
                Console.WriteLine($"Error finding round: {scoreSheet.RoundName}");
                return;
            }

            Round round = RoundRegistry.Instance.Rounds[scoreSheet.RoundName];
            string goldScore = GetGoldScoreForRound(round);


            scoreSheet.Total = new Total();
            scoreSheet.Dozens.ForEach(dozen =>
            {
                dozen.Ends.ForEach(end => end.EndTotal = end.Scores.Sum(ConvertScoreToInt));
                dozen.Total = new Total
                {
                    Score = dozen.Ends.Sum(end => end.Scores.Sum(ConvertScoreToInt)),
                    Hits = dozen.Ends.Sum(end =>
                        end.Scores.Count(score => (!String.IsNullOrEmpty(score) && score != "M" && score != "0"))),
                    Golds = dozen.Ends.Sum(end => end.Scores.Count(score => score == goldScore))
                };

                scoreSheet.Total.Score += dozen.Total.Score;
                scoreSheet.Total.Hits += dozen.Total.Hits;
                scoreSheet.Total.Golds += dozen.Total.Golds;
                scoreSheet.Total.RunningTotal += dozen.Total.Score;

                
                dozen.Total.RunningTotal = scoreSheet.Total.RunningTotal;
            });

            scoreSheet.Handicap = HandicapCalculationService.Instance.CalculateHandicap(round, scoreSheet.Total.Score);

            var lookupKey = (scoreSheet.AgeGroup, scoreSheet.Gender, scoreSheet.BowType);

            if (ClassificationBoundaries.ContainsKey(lookupKey))
            {
                scoreSheet.Classification = ClassificationBoundaries[lookupKey]
                    .OrderBy(v => v.minimumHandicap)
                    .FirstOrDefault(v => v.minimumHandicap >= scoreSheet.Handicap).classification;
            }

        }

        private int ConvertScoreToInt(string score)
        {
            if (String.IsNullOrEmpty(score) || score == "M")
            {
                return 0;
            }
            if (score == "X")
            {
                return 10;
            }
            return int.Parse(score);
        }

        private string GetGoldScoreForRound(Round round)
        {
            switch (round.Scoring)
            {
                case Round.ScoringStyleImperial:
                    return "9";
                case Round.ScoringStyleMetric:
                    return "X";
                case Round.ScoringStyleWorcester:
                    return "5";
                default:
                    return "9";
            }
        }


    }


}
