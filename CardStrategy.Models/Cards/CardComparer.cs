using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardStrategy.Models
{
    // https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.contains?view=netframework-4.8
    public class CardComparer : IEqualityComparer<Card>
    {
        public bool Equals(Card x, Card y)
        {
            //Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            //Check whether the cards are equal.
            return x.Type == y.Type && x.Values.SequenceEqual(y.Values) && x.Suit == y.Suit;

        }

        public int GetHashCode(Card card)
        {            
            if (Object.ReferenceEquals(card, null)) return 0;
            
            int hashValues = card.Values.GetHashCode();
            int hashSuit = card.Suit.GetHashCode();
            int hashType = card.Type.GetHashCode();

            //Calculate the hash code for the product.
            return hashValues ^ hashSuit ^ hashType;

        }
    }

}
