using System;
using System.Linq;

namespace CardStrategy.Models
{
    public class Player
    {
        public decimal Money { get; set; }

        public BlackJackStrategy Strategy { get; set; }

        public Hand Hand { get; set; } = new Hand();

        public void DealCard(Card card, bool faceDown = false)
        {
            if (faceDown) card.Visible = false;            
            Hand.AddTo(card);
        }

        public PlayerAction GetAction(Table table)
        {
            var strategyItem = Strategy.StrategyItems
                .FirstOrDefault(a => a.PlayerCards.All(b => Hand.Cards.Contains(b, new CardComparer())) &&
                    a.DealerCard.Equals(table.Dealer.Hand.Cards.Single(c => c.Visible)));

            if (strategyItem == null)
            {
                throw new Exception("Don't know what to do here.");
            }

            return strategyItem.Action;
        }
    }
}
