using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BowBuddy.Model;
using BowBuddy.Service;
using Xunit;

namespace BowBuddy.Test
{
    public class TestScoreCalculationService
    {
        private readonly ScoreCalculationService _scoreCalculationService = new ScoreCalculationService();

        [Fact]
        public void TestScs()
        {
            ScoreSheet scoreSheet = new ScoreSheet
            {
                Gender = ScoreSheet.GenderMale,
                BowType = ScoreSheet.BowTypeRecurve,
                AgeGroup = ScoreSheet.AgeGroupAdult,
                Date = DateTime.Now,
                Dozens = new Dozen[]
                {
                    new Dozen
                    {
                        Ends = new End[]
                        {
                            new End
                            {
                                Scores = new string[] {"X","X","X","X","7","M"}
                            },
                            new End
                            {
                                Scores = new string[] {"X","X","X","X","7","M"}
                            }
                        }.ToList()
                    },
                    new Dozen
                    {
                        Ends = new End[]
                        {
                            new End
                            {
                                Scores = new string[] {"X","X","X","X","7","M"}
                            },
                            new End
                            {
                                Scores = new string[] {"X","X","X","X","7","M"}
                            }
                        }.ToList()
                    },
                    new Dozen
                    {
                        Ends = new End[]
                        {
                            new End
                            {
                                Scores = new string[] {"X","X","X","X","7","M"}
                            },
                            new End
                            {
                                Scores = new string[] {"X","X","X","X","7","M"}
                            }
                        }.ToList()
                    },
                    new Dozen
                    {
                        Ends = new End[]
                        {
                            new End
                            {
                                Scores = new string[] {"X","X","X","X","7","M"}
                            },
                            new End
                            {
                                Scores = new string[] {"X","X","X","X","7","M"}
                            }
                        }.ToList()
                    },
                    new Dozen
                    {
                        Ends = new End[]
                        {
                            new End
                            {
                                Scores = new string[] {"X","X","X","X","7","M"}
                            },
                            new End
                            {
                                Scores = new string[] {"X","X","X","X","7","M"}
                            }
                        }.ToList()
                    }
                }.ToList(),
                RoundName = "Portsmouth"
            };

            _scoreCalculationService.CalculateScores(scoreSheet);

            Assert.Equal(470, scoreSheet.Total.Score);
            Assert.Equal(470, scoreSheet.Total.RunningTotal);
            Assert.Equal(40, scoreSheet.Total.Golds);
            Assert.Equal(50, scoreSheet.Total.Hits);
            scoreSheet.Dozens.ForEach(dozen =>
            {
                Assert.Equal(94, dozen.Total.Score);
                Assert.Equal(8, dozen.Total.Golds);
                Assert.Equal(10, dozen.Total.Hits);
                dozen.Ends.ForEach(end => Assert.Equal(47, end.EndTotal));
            });

            Assert.Equal(53, scoreSheet.Handicap);
            Assert.Equal("3rd", scoreSheet.Classification);
        }
    }
}
