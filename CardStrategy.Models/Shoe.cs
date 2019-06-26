using System;
using System.Collections.Generic;
using System.Text;

namespace CardStrategy.Models
{
    public class Shoe : CardCollection
    {
        public Shoe(int deckCount)
        {
            DeckCount = deckCount;

            for (int i = 1; i <= deckCount; i++)
            {
                var deck = Deck.CreateDeck();
                Cards.AddRange(deck.Cards);
            }
        }

        public int DeckCount { get; set; }        
    }
}
