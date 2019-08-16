using System;
using System.Collections.Generic;
using System.Text;

namespace CardStrategy.Models
{
    public abstract class CardCollection
    {
        public List<Card> Cards { get; set; } = new List<Card>();

        public void AddTo(Card card)
        {
            Cards.Add(card);
        }
    }
}
