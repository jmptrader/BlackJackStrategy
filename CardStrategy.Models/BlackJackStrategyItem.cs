using System;
using System.Collections.Generic;
using System.Text;

namespace CardStrategy.Models
{
    public class BlackJackStrategyItem
    {
        public Card DealerCard { get; set; }
        public List<Card> PlayerCards { get; set; }        
        public PlayerAction Action { get; set; }
    }
}
