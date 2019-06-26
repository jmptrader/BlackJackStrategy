using CardStrategy.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CardStrategy.Tests.Unit.Models
{
    public class TableSitPlayer
    {
        [Fact]
        public void SitSinglePlayerAtTable()
        {
            // Arrange
            var dealer = new Dealer();
            var table = new Table(dealer);
            var player = new Player();

            // Act
            table.SitPlayer(player);

            // Assert
            Assert.Equal(1, table.Players.Count);
        }
    }
}
