using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Deck interface
namespace Poker_Library
{
    public interface Deck
    {
        int Count();
        void Shuffle();
        Card[] Deal(int count);

    }
}