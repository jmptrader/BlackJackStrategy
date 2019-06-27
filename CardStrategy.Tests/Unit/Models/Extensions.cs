using CardStrategy.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using CardStrategy.Common.Extensions;

namespace CardStrategy.Tests.Models
{
    public class Extensions
    {
        [Fact]
        public void ShuffleDeck_UntilCardsMoved()
        {
            // Arrange
            var deck = Deck.CreateDeck();

            // Act
            int count = 0; // It is theoretically feasible that no cards will be shuffled - hence the loop
            do
            {
                count = deck.Cards.Shuffle();
            }
            while (count == 0);

            // Assert
            Assert.True(count != 0); // Asserting that we got here
        }

        [Fact]
        public void SwapCards_Swap()
        {
            // Arrange
            var deck = Deck.CreateDeck();
            var card1 = deck.Cards[1].Clone();

            // Act
            deck.Cards.Swap(1, 2);

            // Assert
            Assert.Equal(card1.Values[0], deck.Cards[2].Values[0]);
        }

        [Fact]
        public void AddUpHand_NoAces_12()
        {
            // Arrange
            var hand = new Hand()
            {
                Cards = new List<Card>()
                {
                    new Card(CardSuit.Clubs, 3, CardType.Number),
                    new Card(CardSuit.Diamonds, 3, CardType.Number),
                    new Card(CardSuit.Clubs, 4, CardType.Number),
                    new Card(CardSuit.Spades, 2, CardType.Number),
                }
            };

            // Act
            int total = hand.Cards.AddUp();

            // Assert
            Assert.Equal(12, total);
        }

        [Fact]
        public void AddUpHand_Aces_12Plus_SoftAce()
        {
            // Arrange
            var hand = new Hand()
            {
                Cards = new List<Card>()
                {
                    new Card(CardSuit.Clubs, 3, CardType.Number),
                    new Card(CardSuit.Diamonds, 3, CardType.Number),
                    new Card(CardSuit.Clubs, 4, CardType.Number),
                    new Card(CardSuit.Spades, 2, CardType.Number),
                    new Card(CardSuit.Spades, new [] {1, 11 }, CardType.Ace),
                }
            };

            // Act
            int total = hand.Cards.AddUp();

            // Assert
            Assert.Equal(13, total);
        }

        [Fact]
        public void AddUpHand_Aces_3Ace_13()
        {
            // Arrange
            var hand = new Hand()
            {
                Cards = new List<Card>()
                {                    
                    new Card(CardSuit.Spades, new [] { 1, 11 }, CardType.Ace),
                    new Card(CardSuit.Clubs, new [] { 1, 11 }, CardType.Ace),
                    new Card(CardSuit.Diamonds, new [] { 1, 11 }, CardType.Ace),
                }
            };

            // Act
            int total = hand.Cards.AddUp();

            // Assert
            Assert.Equal(13, total);
        }
    }
}
