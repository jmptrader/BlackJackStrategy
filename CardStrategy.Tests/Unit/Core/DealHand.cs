using CardStrategy.Core;
using CardStrategy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CardStrategy.Tests.Unit.Core
{
    public class DealHand
    {
        [Fact]
        public void PlayHand_Deal_GameInProgress()
        {
            // Arrange
            var cards = new List<Card>()
            {
                new Card(CardSuit.Clubs, 4, CardType.Number),
                new Card(CardSuit.Clubs, 5, CardType.Number)
            };
            var dealer = new Dealer();
            var table = new Table(dealer)
            {
                Dealer = dealer
            };
            var playHand = new PlayHand();

            playHand.Init(cards, table);

            // Act
            playHand.Deal();

            // Assert
            Assert.True(playHand.GameInProgress);
        }

        [Fact]
        public void PlayHand_Deal_DealerHasOneVisibleCard()
        {
            // Arrange
            var cards = new List<Card>()
            {
                new Card(CardSuit.Clubs, 4, CardType.Number),
                new Card(CardSuit.Clubs, 5, CardType.Number),
                new Card(CardSuit.Clubs, 6, CardType.Number)
            };
            var dealer = new Dealer();
            var table = new Table(dealer)
            {
                Dealer = dealer
            };
            var playHand = new PlayHand();

            playHand.Init(cards, table);


            // Act
            playHand.Deal();

            // Assert
            Assert.Single(dealer.Hand.Cards.Where(a => a.Visible));
        }

    }
}
