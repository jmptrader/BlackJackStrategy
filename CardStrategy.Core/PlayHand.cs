using CardStrategy.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CardStrategy.Core
{
    public class PlayHand
    {
        private readonly IList<Card> _cards;
        private readonly Table _table;

        public PlayHand(IList<Card> cards, Table table)
        {
            _cards = cards;
            _table = table;
        }

        public bool GameInProgress { get; set; }

        public void Deal()
        {
            if (GameInProgress)
            {
                throw new Exception("Cannot deal - a game is in progress");
            }            

            for (int i = 0; i <= 1; i++)
            {
                foreach (var player in _table.Players)
                {
                    DealSingleCard(player);
                }

                DealSingleCard(_table.Dealer, i == 1);
            }

            GameInProgress = true;
        }

        private void DealSingleCard(Player player, bool faceDown = false)
        {
            var card = _cards.First(); // Cannot run out of cards
            player.DealCard(card, faceDown);
            _cards.Remove(card);
        }

        public void Play()
        {
            if (!GameInProgress)
            {
                throw new Exception("A game must be in progress to play (call Deal first)");
            }

            while (GameInProgress)
            {
                foreach (var player in _table.Players)
                {
                    var action = player.GetAction(_table);
                    switch (action)
                    {
                        case PlayerAction.TakeCard:
                            DealSingleCard(player);
                            break;

                        case PlayerAction.Double:
                            DealSingleCard(player);
                            break;

                        case PlayerAction.Stand:
                            break;

                        case PlayerAction.Split:
                            break;
                    }

                    DealSingleCard(_table.Dealer);
                }
            }
        }
    }
}
