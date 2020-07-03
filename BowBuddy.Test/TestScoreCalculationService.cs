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
                Date = DateTime.Now,
                Dozens = new Dozen[]
                {
                    new Dozen
                    {
                        Ends = new End[]
                        {
                            new End
                            {
                                Scores = new string[] {"X","9","8","7","M","M"}
                            },
                            new End
                            {
                                Scores = new string[] {"X","9","8","7","M","M"}
                            }
                        }.ToList()
                    },
                    new Dozen
                    {
                        Ends = new End[]
                        {
                            new End
                            {
                                Scores = new string[] {"X","9","8","7","M","M"}
                            },
                            new End
                            {
                                Scores = new string[] {"X","9","8","7","M","M"}
                            }
                        }.ToList()
                    },
                    new Dozen
                    {
                        Ends = new End[]
                        {
                            new End
                            {
                                Scores = new string[] {"X","9","8","7","M","M"}
                            },
                            new End
                            {
                                Scores = new string[] {"X","9","8","7","M","M"}
                            }
                        }.ToList()
                    },
                    new Dozen
                    {
                        Ends = new End[]
                        {
                            new End
                            {
                                Scores = new string[] {"X","9","8","7","M","M"}
                            },
                            new End
                            {
                                Scores = new string[] {"X","9","8","7","M","M"}
                            }
                        }.ToList()
                    },
                    new Dozen
                    {
                        Ends = new End[]
                        {
                            new End
                            {
                                Scores = new string[] {"X","9","8","7","M","M"}
                            },
                            new End
                            {
                                Scores = new string[] {"X","9","8","7","M","M"}
                            }
                        }.ToList()
                    }
                }.ToList(),
                RoundName = "Portsmouth"
            };

            _scoreCalculationService.CalculateScores(scoreSheet);

            Assert.Equal(340, scoreSheet.Total.Score);
            Assert.Equal(340, scoreSheet.Total.RunningTotal);
            Assert.Equal(10, scoreSheet.Total.Golds);
            Assert.Equal(40, scoreSheet.Total.Hits);
            scoreSheet.Dozens.ForEach(dozen =>
            {
                Assert.Equal(68, dozen.Total.Score);
                Assert.Equal(2, dozen.Total.Golds);
                Assert.Equal(8, dozen.Total.Hits);
                dozen.Ends.ForEach(end => Assert.Equal(34, end.EndTotal));
            });
        }
    }
}
