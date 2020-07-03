using System;
using BowBuddy.Model;
using BowBuddy.Service;
using Xunit;

namespace BowBuddy.Test
{
    public class TestHandicapCalculationService
    {
        private readonly HandicapCalculationService _service = new HandicapCalculationService();
        [Theory]
        [InlineData("Junior National", 397, 66)]
        [InlineData("Junior National", 410, 65)]
        [InlineData("Junior National", 396, 66)]
        public void Test1(string roundName, int score, int expectedHandicap)
        {
            Round round = RoundRegistry.Instance.Rounds[roundName];
            Assert.Equal(expectedHandicap, _service.CalculateHandicap(round, score));

        }
    }
}
