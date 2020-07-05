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
            List<(string classification, int minimumHandicap)>> _classificationBoundaries = new Dictionary<(string ageGroup, string gender, string bowType), List<(string classification, int minimumHandicap)>>();

        public ScoreCalculationService()
        {
            PopulateClassificationBoundaries();
        }
        private void PopulateClassificationBoundaries()
        {

            _classificationBoundaries[(ScoreSheet.AgeGroupAdult, ScoreSheet.GenderMale, ScoreSheet.BowTypeRecurve)] = new List<(string classification, int minimumHandicap)>
            {
                (ScoreSheet.Classification3rd, 58),
                (ScoreSheet.Classification2nd, 50),
                (ScoreSheet.Classification1st, 44),
                (ScoreSheet.ClassificationBow, 36),
                (ScoreSheet.ClassificationMB, 28),
                (ScoreSheet.ClassificationGMB, 22)
            };
            _classificationBoundaries[(ScoreSheet.AgeGroupAdult, ScoreSheet.GenderMale, ScoreSheet.BowTypeCompound)] = new List<(string classification, int minimumHandicap)>
            {
                (ScoreSheet.Classification3rd, 48),
                (ScoreSheet.Classification2nd, 38),
                (ScoreSheet.Classification1st, 32),
                (ScoreSheet.ClassificationBow, 23),
                (ScoreSheet.ClassificationMB, 16),
                (ScoreSheet.ClassificationGMB, 10)
            };
            _classificationBoundaries[(ScoreSheet.AgeGroupAdult, ScoreSheet.GenderMale, ScoreSheet.BowTypeLongbow)] = new List<(string classification, int minimumHandicap)>
            {
                (ScoreSheet.Classification3rd, 74),
                (ScoreSheet.Classification2nd, 69),
                (ScoreSheet.Classification1st, 65),
                (ScoreSheet.ClassificationBow, 60),
                (ScoreSheet.ClassificationMB, 55),
                (ScoreSheet.ClassificationGMB, 52)
            };
            _classificationBoundaries[(ScoreSheet.AgeGroupAdult, ScoreSheet.GenderMale, ScoreSheet.BowTypeBarebow)] = new List<(string classification, int minimumHandicap)>
            {
                (ScoreSheet.Classification3rd, 71),
                (ScoreSheet.Classification2nd, 64),
                (ScoreSheet.Classification1st, 56),
                (ScoreSheet.ClassificationBow, 49),
                (ScoreSheet.ClassificationMB, 45),
                (ScoreSheet.ClassificationGMB, 40)
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

            if (_classificationBoundaries.ContainsKey(lookupKey))
            {
                scoreSheet.Classification = _classificationBoundaries[lookupKey]
                    .OrderBy(v => v.minimumHandicap)
                    .FirstOrDefault(v => v.minimumHandicap >= scoreSheet.Handicap).classification;
            }

            if (String.IsNullOrEmpty(scoreSheet.Classification))
            {
                scoreSheet.Classification = ScoreSheet.ClassificationUnclassified;
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
