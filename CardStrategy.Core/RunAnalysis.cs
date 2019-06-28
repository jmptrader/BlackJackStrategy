using CardStrategy.Common.Extensions;
using CardStrategy.Core.Models;
using CardStrategy.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardStrategy.Core
{
    public class RunAnalysis : IRunAnalysis
    {
        public decimal Run(AnalysisConfiguration analysisConfiguration)
        {
            decimal currentBet = analysisConfiguration.StartingAnte;

            // Create the dealer
            var dealer = new Dealer()
            {
                Money = 9999999999 // House has infinite funds
            };

            // Create the table
            var table = new Table(dealer);

            // Create and shuffle the shoe
            var shoe = new Shoe(4);
            shoe.Cards.Shuffle();

            // Create the player and sit at the table
            var player = new Player();
            table.SitPlayer(player);

            // Cut the deck??

            // Play each hand
            var playHand = new PlayHand(shoe.Cards, table);

            while (player.Money > 0 && player.Money < analysisConfiguration.TargetFunds)
            {
                playHand.PlayerAnte(player, currentBet);
                playHand.Deal();

                while (playHand.GameInProgress)
                {
                    playHand.Play();
                }

                playHand.Payout();
            }

            return player.Money;
        }
    }
}
