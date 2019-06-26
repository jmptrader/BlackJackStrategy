using System;

namespace CardStrategy.Models
{
    public class Card
    {
        public Card(CardSuit suit, int[] values, CardType cardType)
        {
            Suit = suit;
            Values = values;
            Type = cardType;

            AssignDescription();
        }

        public Card(CardSuit suit, int value, CardType cardType)
        {
            Suit = suit;
            Values = new[] { value };
            Type = cardType;

            AssignDescription();
        }

        private void AssignDescription()
        {
            if (Type == CardType.Number)
            {
                Description = $"{Values[0]} of {Suit}";
            }
            else
            {
                Description = $"{Type.ToString()} of {Suit}";
            }
        }

        public CardSuit Suit { get; set; }
        public int[] Values { get; set; }
        public CardType Type { get; set; }
        public string Description { get; set; }
        public bool Visible { get; internal set; } = true;

        public Card Clone()
        {
            return (Card)this.MemberwiseClone();
        }

        public override bool Equals(object obj)
        {
            var cardComparer = new CardComparer();
            return cardComparer.Equals(this, (Card)obj);            
        }
    }
}
