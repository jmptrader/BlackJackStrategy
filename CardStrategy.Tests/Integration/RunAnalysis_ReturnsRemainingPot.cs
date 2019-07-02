using CardStrategy.Core;
using CardStrategy.Core.Models;
using CardStrategy.Models;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CardStrategy.Tests.Integration
{
    public class RunAnalysis_ReturnsRemainingPot
    {
        [Fact]
        public void RunAnalysis_Steady_ReturnsRemainingPot()
        {
            // Arrange
            var logger = Substitute.For<ILogger<RunAnalysis>>();
            var runAnalysis = new RunAnalysis(logger);
            var analysisConfiguration = new AnalysisConfiguration()
            {
                StartingAnte = 1,
                BettingStrategy = BettingStrategy.Steady,
                DeckCountPerShoe = 4,
                PlayerFunds = 10,
                TargetFunds = 11,
                AvailableActions = BuildAvailableActionsAlwaysStand()
            };

            // Act
            decimal result = runAnalysis.Run(analysisConfiguration);

            // Assert
            Assert.True(result <= analysisConfiguration.TargetFunds + 1);
        }

        private List<AvailableAction> BuildAvailableActionsAlwaysStand()
        {
            var availableActions = new List<AvailableAction>();

            for (int d = 2; d <= 11; d++)
            {
                for (int p = 3; p <= 21; p++)
                {
                    var action = new AvailableAction()
                    {
                        DealerCard = d,
                        PlayerValue = p,
                        PlayerAction = PlayerAction.Stand,
                        Key = Guid.NewGuid()
                    };
                    availableActions.Add(action);
                }
            }

            return availableActions;
        }
    }
}
