using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BowBuddy.Model;

namespace BowBuddy.Service
{
    public class ScoreCalculationService
    {
        
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

            scoreSheet.Handicap = new HandicapCalculationService().CalculateHandicap(round, scoreSheet.Total.Score);


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

    /*public string MinimumGoldScore { get; set; }

    public int Score => Ends.Sum(e => e.Scores.Select(score =>
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

    }).Sum());

    public int Golds()
    {
        if (MinimumGoldScore == "X")
        {
            return Ends.Sum(e => e.Scores.Count(s => s == "X"));
        }
        else
        {
            return Ends.Sum(e => e.Scores.Count(s => int.Parse(s) >= int.Parse(MinimumGoldScore)));
        }
    }*/
}


}
