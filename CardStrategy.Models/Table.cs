using System;
using System.Collections.Generic;
using System.Text;

namespace CardStrategy.Models
{
    public class Table
    {
        public Table(Dealer dealer)
        {
            Dealer = dealer;
        }

        public IList<Player> Players { get; set; } = new List<Player>();
        public Dealer Dealer { get; set; }

        public int Spaces = 6;

        public void SitPlayer(Player player)
        {
            Players.Add(player);
        }
    }
}
