using System;
using System.Collections.Generic;
using System.Text;

namespace CardStrategy.Models
{
    public enum CardSuit
    {
        Spades,
        Diamonds,
        Clubs,
        Hearts
    }

    public enum CardType
    {
        Number,
        Ace,
        Jack,
        Queen,
        King
    }

    public enum PlayerAction
    {
        TakeCard,
        Stand,
        Double,
        Split
    }
}
