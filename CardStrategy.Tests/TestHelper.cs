using CardStrategy.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardStrategy.Tests
{
    public class TestHelper
    {
        public static List<Card> GetAllCardsForSuit(CardSuit cardSuit)
        {
            var cards = new List<Card>();
            for (int i = 2; i <= 10; i++)
            {
                cards.Add(new Card(cardSuit, i, CardType.Number));
            }
            cards.Add(new Card(cardSuit, 10, CardType.Jack));
            cards.Add(new Card(cardSuit, 10, CardType.Queen));
            cards.Add(new Card(cardSuit, 10, CardType.King));
            cards.Add(new Card(cardSuit, new[] { 1, 11 }, CardType.Ace));

            return cards;
        }

        public static List<AvailableAction> BuildAvailableActionsAlwaysStand()
        {
            var availableActions = new List<AvailableAction>();

            for (int d = 2; d <= 11; d++)
            {
                for (int p = 3; p <= 21; p++)
                {
                    var action = new AvailableAction()
                    {
                        DealerCard = d,
                        PlayerValue = p,
                        PlayerAction = PlayerAction.Stand,
                        Key = Guid.NewGuid()
                    };
                    availableActions.Add(action);
                }
            }

            return availableActions;
        }

    }
}
