using CardStrategy.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardStrategy.Core
{
    public interface IPlayHand
    {
        Table Table { get; set; }
        bool GameInProgress { get; set; }

        void Init(List<Card> cards, Table table);
        void Deal();
        void PlayerAnte(Player player, decimal anti);
        void Play();
        bool Payout();
    }
}
