using CardStrategy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using CardStrategy.Common.Extensions;

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
            // Turn cards first
            if (player.Hand.Cards.Any(a => !a.Visible))
            {
                player.Hand.Cards.First(a => !a.Visible).TurnCard();
                return;
            }

            var card = _cards.First(); // Cannot run out of cards
            player.DealCard(card, faceDown);
            _cards.Remove(card);
        }

        public void PlayerAnte(Player player, decimal anti)
        {
            player.Money -= anti;
            player.Ante = anti;
        }

        public void Play()
        {
            if (!GameInProgress)
            {
                throw new Exception("A game must be in progress to play (call Deal first)");
            }

            foreach (var player in _table.Players)
            {
                while (true)
                {
                    if (!GetPlayerAction(player)) break;
                    if (IsPlayerBust(player)) break;
                }
            }

            while (true)
            {
                DealSingleCard(_table.Dealer);
                if (IsPlayerBust(_table.Dealer) ||
                    HasDealerReachedLimit(_table.Dealer))
                {
                    break;
                }
            }

            GameInProgress = false;
        }

        public void Payout()
        {
            bool dealerBust = IsPlayerBust(_table.Dealer);
            int dealerTotal = dealerBust ? -1 : _table.Dealer.Hand.Cards.AddUp();

            foreach (var player in _table.Players)
            {
                if (IsPlayerBust(player))
                {
                    LoseStake(player, _table.Dealer);
                    continue;
                }

                if (dealerBust)
                {
                    PayWinnings(player, _table.Dealer);
                }
            }
        }

        private void PayWinnings(Player player, Dealer dealer)
        {
            decimal odds = 2;
            if (player.Hand.Cards.IsBlackJack())
            {
                odds = 2.5m;
            }

            decimal winnings = (player.Ante * odds);

            player.Money += winnings;
            player.Ante = 0;

            dealer.Money -= winnings;
        }

        private void LoseStake(Player player, Dealer dealer)
        {
            dealer.Money += player.Ante;
            player.Ante = 0;
        }

        private bool HasDealerReachedLimit(Dealer dealer)
        {
            // Dealer must draw to 16 and stand on 17
            int total = dealer.Hand.Cards.AddUp();
            return (total >= 17);
        }

        private bool IsPlayerBust(Player player)
        {
            int total = player.Hand.Cards.AddUp();
            return (total > 21);
            
        }

        private bool GetPlayerAction(Player player)
        {
            var action = player.GetAction(_table);
            switch (action)
            {
                case PlayerAction.TakeCard:
                    DealSingleCard(player);
                    break;

                case PlayerAction.Double:
                    DealSingleCard(player);
                    return false;

                case PlayerAction.Stand:
                    return false;                    

                case PlayerAction.Split:
                    break;
            }

            return true;
        }
    }
}
