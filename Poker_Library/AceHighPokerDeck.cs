using System;
using System.Collections.Generic;
using System.Linq;
//Acehighdeck concrete class repersenting entire card deck w/out jokers

namespace Poker_Library
{
    public class AceHighPokerDeck : Deck
    {
        private List<Card> cards;
        private Random rand = new Random();
        public AceHighPokerDeck(Random random)
        {

            cards = new List<Card>();
            for (CardSuit s = CardSuit.Club; s <= CardSuit.Spade; s++)
            {
                for (CardFace f = CardFace.Two; f <= CardFace.Ace; f++)
                {
                    cards.Add(new PokerCard(s, f));
                }
            }
        }

        public int Count()
        {
            return cards.Count();
        }

        public Card[] Deal(int count)
        {
            if (cards.Count < count) throw new Exception("Not enough cards\n");

            Card[] dealtCards = new Card[count];

            for (int i = 0; i < count; i++) dealtCards[i] = cards[i];
            cards.RemoveRange(0, count);
            return dealtCards;
        }

        public void Shuffle()
        {
            List<Card> newCards = new List<Card>();
            while (cards.Count > 0)
            {
                int i = rand.Next(0, cards.Count);

                newCards.Add(cards[i]);
                cards.RemoveAt(i);
            }
            cards = newCards;
        }
    }
}
