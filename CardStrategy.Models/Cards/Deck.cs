using System;
using System.Collections.Generic;
using System.Text;

namespace CardStrategy.Models
{
    public class Deck : CardCollection
    {
        private Deck() { }        

        public static Deck CreateDeck()
        {
            var deck = new Deck();
            deck.Cards = new List<Card>();

            for (int cardSuit = 0; cardSuit <= 3; cardSuit++)
            {
                for (int cardIdx = 2; cardIdx <= 10; cardIdx++)
                {
                    var newCard = new Card((CardSuit)cardSuit, cardIdx, CardType.Number);
                    deck.Cards.Add(newCard);
                }
                    
                deck.Cards.Add(new Card((CardSuit)cardSuit, 10, CardType.Jack));
                deck.Cards.Add(new Card((CardSuit)cardSuit, 10, CardType.Queen));
                deck.Cards.Add(new Card((CardSuit)cardSuit, 10, CardType.King));
                deck.Cards.Add(new Card((CardSuit)cardSuit, new[] { 1, 11 }, CardType.Ace));
            }

            return deck;
        }
    }
}
