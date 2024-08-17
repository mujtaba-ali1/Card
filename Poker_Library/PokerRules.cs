using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Rules interface
//interface for providing rules 
namespace Poker_Library
{
    public interface PokerRules
    {
        Hand DealHand(Deck d);
        int Compare(Hand h1, Hand h2);
        int RankHand(Hand h);
        string NameHand(Hand h);
    }
}