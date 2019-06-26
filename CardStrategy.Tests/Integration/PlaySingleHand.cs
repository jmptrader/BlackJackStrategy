using CardStrategy.Core;
using CardStrategy.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CardStrategy.Tests.Integration
{
    public class PlaySingleHand
    {
        [Fact]
        public void PlaySingleHand_1Player_PlayerStands()
        {
            // Arrange
            var dealer = new Dealer();
            var table = new Table(dealer);
            var shoe = new Shoe(4);
            var playHand = new PlayHand(shoe.Cards, table);

            var blackJackStrategy = new BlackJackStrategy()
            {
                StrategyItems = new List<BlackJackStrategyItem>()
                {
                    new BlackJackStrategyItem()
                    {
                        DealerCard = new Card(CardSuit.Spades, 3, CardType.Number),
                        PlayerCards = new List<Card>()
                        {
                            new Card(CardSuit.Spades, 2, CardType.Number),
                            new Card(CardSuit.Spades, 4, CardType.Number),
                        },
                        Action = PlayerAction.Stand
                    }
                }
            };

            var player = new Player()
            {
                Money = 100,
                Strategy = blackJackStrategy
            };

            table.SitPlayer(player);

            // Act
            playHand.Deal();

            while (playHand.GameInProgress)
            {
                playHand.Play();
            }

            // Assert
            Assert.Equal(10, player.Money);
        }
    }
}
