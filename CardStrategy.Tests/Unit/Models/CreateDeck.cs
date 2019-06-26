using CardStrategy.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CardStrategy.Tests.Models
{
    public class CreateDeck
    {
        [Fact]
        public void CreateDeck_Creates52Cards()
        {
            // Arrange

            // Act
            var deck = Deck.CreateDeck();

            // Assert
            Assert.Equal(52, deck.Cards.Count);
        }
    }
}
