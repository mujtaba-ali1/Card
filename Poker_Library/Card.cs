using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//card interface, with CardSuit, CardFace enums

namespace Poker_Library
{
    public enum CardSuit
    {
        Club = 0,
        Heart = 1,
        Diamond = 2, Diamonds = 2,
        Spade = 3, Spades = 3

    }
    public enum CardFace
    {
        AceLow = 1, Two = 2, Three = 3, Four = 4, Five = 5, Six = 6, Seven = 7, Eight = 8, Nine = 9, Ten = 10, Jack = 11, Queen = 12,
        King = 13, Ace = 14
    }

    public interface Card
    {
        CardSuit CardSuit
        {
            get;
        }
        CardFace Face
        {
            get;
        }

    }
}