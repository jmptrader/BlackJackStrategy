using System;
using System.Linq;
using CardStrategy.Common.Extensions;

namespace CardStrategy.Models
{
    public class Player
    {
        public decimal Money { get; set; }

        public decimal Ante { get; set; }

        public BlackJackStrategy Strategy { get; set; }

        public Hand Hand { get; set; } = new Hand();        

        public void DealCard(Card card, bool faceDown = false)
        {
            if (faceDown) card.Visible = false;            
            Hand.AddTo(card);
        }

        public PlayerAction GetAction(Table table)
        {
            var strategyItems = Strategy.StrategyItems.Where(a => a.DealerCard.Equals(table.Dealer.Hand.Cards.Single(c => c.Visible)));
            var matchingStrategyItems = strategyItems?.Where(a => a.PlayerTotalCardValue == Hand.Cards.AddUp());

            if (!matchingStrategyItems.Any())
            {
                throw new Exception("Don't know what to do here.");
            }

            var strategyItem = matchingStrategyItems.Count() == 1
                ? matchingStrategyItems.Single()
                : matchingStrategyItems.Single(a => a is BlackJackOverrideStrategyItem);

            return strategyItem.Action;
        }
    }
}
