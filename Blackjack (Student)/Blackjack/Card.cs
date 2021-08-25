using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    class Card
    {
        // unicode values for the suit glyphs
        public enum Suit{
            SPADE,
            CLUB,
            HEART,
            DIAMOND
        };

        public Suit suit;
        public int value;

        public Card(Suit suit, int value)
        {
            this.suit = suit;
            this.value = value;
        }

        public string GetSuitName ()
        {
            switch (suit)
            {
                case Suit.SPADE:
                    return " Spade";
                case Suit.CLUB:
                    return " Club";
                case Suit.HEART:
                    return " Heart";
                case Suit.DIAMOND:
                    return " Diamond";
            }
            return string.Empty;
        }

        // Print a card to the console
        // Use the unicode glyphs to output the card suit, 
        // along with the value of the card.
        // Face cards appear as A,J,K or Q
        public string Print()
        {
            string output = string.Empty;
            //output the card and suite
            switch (value)
            {
                default:
                    output = String.Format("{0} {1} ", value, GetSuitName());
                    break;
                case 1:
                    output = String.Format("A {0} ", GetSuitName());
                    break;
                case 11:
                    output = String.Format("J {0} ", GetSuitName());
                    break;
                case 12:
                    output = String.Format("K {0} ", GetSuitName());
                    break;
                case 13:
                    output = String.Format("Q {0} ", GetSuitName());
                    break;
            }
            return output;
        }
    }
}
