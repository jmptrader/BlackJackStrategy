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

namespace CardStrategy.Tests.Integration
{
    public class RunAnalysis_ReturnsRemainingPot
    {
        [Theory]
        [InlineData(BettingStrategy.Steady)]
        [InlineData(BettingStrategy.Martingale)]
        [InlineData(BettingStrategy.DoubleOnWin)]
        [InlineData(BettingStrategy.IncreaseOnWin)]
        [InlineData(BettingStrategy.IncreaseOnLose)]
        [InlineData(BettingStrategy.DoubleOnLose)]        
        public async Task RunAnalysis_Steady_ReturnsRemainingPot(BettingStrategy bettingStrategy)
        {
            // Arrange
            var logger = Substitute.For<ILogger<RunAnalysis>>();
            var playHand = new PlayHand();
            var runAnalysis = new RunAnalysis(logger, playHand);
            var analysisConfiguration = new AnalysisConfiguration()
            {
                StartingAnte = 1,
                BettingStrategy = bettingStrategy,
                DeckCountPerShoe = 4,
                PlayerFunds = 10,
                TargetFunds = 11,
                AvailableActions = TestHelper.BuildAvailableActionsAlwaysStand()
            };

            var updateProgress = Substitute.For<IUpdateProgress>();

            // Act
            decimal result = await runAnalysis.Run(analysisConfiguration, updateProgress);

            // Assert
            Assert.True(result <= analysisConfiguration.TargetFunds + 1);
        }

    }
}
