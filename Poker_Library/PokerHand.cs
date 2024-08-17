using System.Collections;
using System.Collections.Generic;

namespace Poker_Library
{
    public class PokerHand : Hand
    {
        private List<Card> cards;
        public PokerHand()
        {
            cards = new List<Card>();
        }
        public PokerHand(Card[] initalCards)
        {
            cards = new List<Card>(initalCards);

        }
        public void Add(Card c)
        {
            cards.Add(c);

        }

        public int Count()
        {
            return cards.Count;

        }

        public IEnumerator<Card> GetEnumerator()
        {
            return cards.GetEnumerator();
        }

        public void Remove(Card c)
        {
            cards.Remove(c);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return cards.GetEnumerator();
        }

    }
}