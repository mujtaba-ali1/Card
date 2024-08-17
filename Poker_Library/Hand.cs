using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker_Library
{
    public interface Hand : IEnumerable<Card>
    {
        int Count();
        void Add(Card c);
        void Remove(Card c);
    }
}