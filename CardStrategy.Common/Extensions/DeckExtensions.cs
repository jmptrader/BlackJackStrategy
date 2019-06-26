using CardStrategy.Models;
using System;
using System.Collections.Generic;

namespace CardStrategy.Common
{
    public static class DeckExtensions
    {
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
