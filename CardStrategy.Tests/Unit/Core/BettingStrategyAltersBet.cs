using CardStrategy.Core;
using CardStrategy.Core.Models;
using CardStrategy.Models;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CardStrategy.Tests.Unit.Core
{
    public class BettingStrategyAltersBet
    {
        [Theory]
        [InlineData(BettingStrategy.DoubleOnLose, false, 20)]
        [InlineData(BettingStrategy.DoubleOnLose, true, 10)]
        [InlineData(BettingStrategy.DoubleOnWin, true, 20)]
        [InlineData(BettingStrategy.DoubleOnWin, false, 10)]
        [InlineData(BettingStrategy.IncreaseOnLose, false, 11)]
        [InlineData(BettingStrategy.IncreaseOnLose, true, 10)]
        [InlineData(BettingStrategy.IncreaseOnWin, false, 10)]
        [InlineData(BettingStrategy.IncreaseOnWin, true, 11)]
        [InlineData(BettingStrategy.Steady, true, 10)]
        [InlineData(BettingStrategy.Steady, false, 10)]
        [InlineData(BettingStrategy.Martingale, true, 1000)]
        [InlineData(BettingStrategy.Martingale, false, 1000)]
        public void CurrentBet10_CallBettingStragyHelperForEachStrategy(
            BettingStrategy bettingStrategy, bool isWinner, decimal expectedNewBetAmount)
        {
            // Act
            decimal betAmount = BettingStrategyHelper.DetermineBet(10, bettingStrategy, isWinner, 1000, 2000);

            // Assert
            Assert.Equal(expectedNewBetAmount, betAmount);
        }

        [Fact]
        public async Task RunAnalysis_IncreaseOnWin_WinBet()
        {
            // Arrange
            var logger = Substitute.For<ILogger<RunAnalysis>>();
            var playHand = Substitute.For<IPlayHand>();

            var runAnalysis = new RunAnalysis(logger, playHand);

            var analysisConfiguration = new AnalysisConfiguration()
            {
                StartingAnte = 1,
                BettingStrategy = BettingStrategy.IncreaseOnWin,
                DeckCountPerShoe = 4,
                PlayerFunds = 10,
                TargetFunds = 11,
                AvailableActions = TestHelper.BuildAvailableActionsAlwaysStand()
            };

            var updateProgress = Substitute.For<IUpdateProgress>();

            // Act
            decimal result = await runAnalysis.Run(analysisConfiguration, updateProgress);

            // Assert
        }
    }
}
