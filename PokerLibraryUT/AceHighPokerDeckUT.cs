using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Poker_Library;

namespace PokerLibraryUT
{
    [TestClass]
    public class AceHighPokerDeckUT
    {
        [TestMethod]
        public void TestConstructor()
        {
            Random r = new Random();

            Deck d = new AceHighPokerDeck(r);

            Assert.AreEqual(52, d.Count());

        }

        [TestMethod]
        public void TestShuffle()
        {
            Random r = new Random();

            Deck oldDeck = new AceHighPokerDeck(r);
            Deck newDeck = new AceHighPokerDeck(r);

            Card[] oldOrder = oldDeck.Deal(52);

            Assert.AreEqual(52, oldOrder.Length);
            Assert.AreEqual(0, oldDeck.Count());

            newDeck.Shuffle();
            Card[] newOrder = newDeck.Deal(52);

            Assert.AreEqual(52, newOrder.Length);
            Assert.AreEqual(0, newDeck.Count());

            Assert.IsTrue((OrderIsDifferent(newOrder, oldOrder, newDeck)));

        }

        private bool OrderIsDifferent(Card[] newOrder, Card[] oldOrder, Deck newDeck)
        {  
            if (newOrder.Length != oldOrder.Length) throw new ArgumentException("Card Arrays must be same size\n");
            for (int i = 0; i < newDeck.Count(); i++)
            {
                if (newOrder[i].Face == oldOrder[i].Face && newOrder[i].CardSuit == newOrder[i].CardSuit)
                {
                    return false;

                }

            }

            return true;

        }

        [TestMethod]
        public void TestDeal()
        {
            Random r = new Random();

            Deck d = new AceHighPokerDeck(r);

            Card[] cards = d.Deal(5);
            Assert.AreEqual(5, cards.Length);
            Assert.AreEqual(47, d.Count());

        }
    }
}