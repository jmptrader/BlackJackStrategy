using CardStrategy.Core;
using CardStrategy.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using CardStrategy.Common.Extensions;

namespace CardStrategy.Tests.Integration
{
    public class PlaySingleHand_LoseToDealer
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
                        PlayerTotalCardValue = 6,                        
                        Action = PlayerAction.Stand
                    }
                }
            };

            var player = new Player()
            {
                Money = 100,
                Strategy = blackJackStrategy
            };

            // Act
            table.SitPlayer(player);
            playHand.PlayerAnte(player, 10);
            playHand.Deal();

            while (playHand.GameInProgress)
            {
                playHand.Play();
            }

            playHand.Payout();

            // Assert
            Assert.Equal(90, player.Money);
        }

        [Fact]
        public void PlaySingleHand_1Player_PlayerDraws_Bust()
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
                        PlayerTotalCardValue = 6,
                        Action = PlayerAction.TakeCard
                    },
                    new BlackJackStrategyItem()
                    {
                        DealerCard = new Card(CardSuit.Spades, 3, CardType.Number),
                        PlayerTotalCardValue = 12,
                        Action = PlayerAction.TakeCard
                    },
                    new BlackJackStrategyItem()
                    {
                        DealerCard = new Card(CardSuit.Spades, 3, CardType.Number),
                        PlayerTotalCardValue = 19,
                        Action = PlayerAction.TakeCard
                    }
                }
            };

            var player = new Player()
            {
                Money = 100,
                Strategy = blackJackStrategy
            };

            // Act
            table.SitPlayer(player);
            playHand.PlayerAnte(player, 10);
            playHand.Deal();

            while (playHand.GameInProgress)
            {
                playHand.Play();
            }

            playHand.Payout();

            // Assert
            Assert.Equal(90, player.Money);
            Assert.True(player.Hand.Cards.AddUp() > 21);
        }
    }
}
