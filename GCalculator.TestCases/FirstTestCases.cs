using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GCalculator.Domain;
using Xunit;

namespace GCalculator.TestCases
{
    public class FirstTestCases
    {
        [Fact]
        public void SkirmisherPlayedFirstTurnCalculatesCorrectly()
        {
            var game = GameImporter.Import(File.OpenRead("skirmisherFirstTurn.json"));

            var result = Calculator.Calculate(game);

            Assert.Equal(6, result.FirstTurn.PlayerScore);
            Assert.Equal(0, result.SecondTurnResult.PlayerScore);
            Assert.Equal(0, result.ThirdTurn.PlayerScore);
        }
    }
}
