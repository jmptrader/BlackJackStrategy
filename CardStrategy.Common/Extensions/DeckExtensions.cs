using CardStrategy.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardStrategy.Common.Extensions
{
    public static class DeckExtensions
    {
        public static bool IsBlackJack(this IList<Card> cards)
        {
            return (cards.Count() == 2 && cards.Any(a => a.Type == CardType.Ace)
                && cards.Any(a => a.Values[0] == 10));
        }

        /// <summary>
        /// Return a total numeric value of all the card values totalled
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static int AddUp(this IList<Card> cards)
        {
            // Don't add up cards that are face down
            var validCards = cards.Where(a => a.Visible);

            // Calculate the totals that can't be changed
            int hardTotal = validCards.Where(a => a.Type != CardType.Ace).Sum(a => a.Values[0]);

            // Now include aces
            var aces = validCards.Where(a => a.Type == CardType.Ace);
            if (aces.Count() == 0) return hardTotal;

            if (21 - hardTotal >= 11 + (aces.Count() - 1))
            {
                hardTotal += aces.Take(1).Sum(a => a.Values.Max()) + aces.Skip(1).Sum(a => a.Values.Min());                
            }
            else
            {
                hardTotal += aces.Sum(a => a.Values.Min());
            }

            return hardTotal;
        }

        /// <summary>
        /// Returns a count of the cards moved
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static int Shuffle(this IList<Card> cards)
        {
            Random rnd = new Random();
            int countMoved = 0;

            int totalCards = cards.Count - 1;
            for (int i = 1; i <= totalCards; i++)
            {
                int targetPosition = rnd.Next(totalCards - 1) + 1;
                if (targetPosition != i)
                {
                    cards.Swap(targetPosition, i);
                    countMoved++;
                }
            }

            return countMoved;
        }

        public static void Swap(this IList<Card> cards, int target, int source)
        {
            var tmpCard = cards[target].Clone();
            cards[source] = cards[target].Clone();
            cards[target] = tmpCard;
        }

    }
}
