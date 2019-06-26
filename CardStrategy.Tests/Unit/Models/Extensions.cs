using CardStrategy.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using CardStrategy.Common;

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
    }
}
