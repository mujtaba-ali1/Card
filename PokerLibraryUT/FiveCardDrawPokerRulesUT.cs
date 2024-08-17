using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker_Library;
using System;

namespace PokerLibraryUT
{
    [TestClass]
    public class FiveCardDrawPokerRulesUT
    {
        [TestMethod]
        public void TestDeal()
        {
            Random random = new Random();
            Deck deck = new AceHighPokerDeck(random);
            PokerRules rules = new FiveCardDrawPokerRules();

            Hand hand = rules.DealHand(deck);

            Assert.AreEqual(5, hand.Count());
            Assert.AreEqual(47, deck.Count());
            Assert.IsInstanceOfType(hand, typeof(PokerHand));

        }

        [TestMethod]
        public void TestHand()
        {
            Hand hand = new PokerHand();
            FiveCardDrawPokerRules rules = new FiveCardDrawPokerRules();
        
            hand.Add(new PokerCard(CardSuit.Club, CardFace.Eight));
            hand.Add(new PokerCard(CardSuit.Spades, CardFace.Nine));

            int AmountofCards = hand.Count();
            Assert.AreEqual(AmountofCards, hand.Count());
        }

        [DataTestMethod]
        [DataRow("JC TC 9C 8C 7C", 2)]
        [DataRow("2D 3D 4D 5D 6D", 2)]
        [DataRow("QH TH AH JH KH", 2)] 

        [DataRow("5C 5D 5H 5S 3D", 3)]
        [DataRow("4C 4H 7H 4D 4S", 3)]

        [DataRow("6S 6H 6D KC KH", 4)]
        [DataRow("8H 3C 8D 3S 8S", 4)]

        [DataRow("2S JS 4S 5S TS", 5)]

        [DataRow("3D 6H 4S 7C 5H", 6)]

        [DataRow("2C 3H 2H 2D 4C", 7)]
        [DataRow("7S 6D 6S 5S 6C", 7)]
        [DataRow("TS 9H TD TC 8C", 7)]

        [DataRow("AS AH KD KC QH", 8)]
        [DataRow("TH JD 9H TS 9C", 8)]
        [DataRow("8H 8S 6C 7C 6D", 8)]

        [DataRow("5S 5C AH QS JD", 9)]
        [DataRow("4D 4H 3D TS 9C", 9)]
        [DataRow("7C 7H 6C 4C 3H", 9)]
        [DataRow("6H 6D 5S 4H 7C", 9)]

        [DataRow("2S AH 3S TC QD", 10)]
        public void TestRank(string cardNames, int expectedRank)
        {
            Hand h = CreateHand(cardNames);
            PokerRules rules = new FiveCardDrawPokerRules();
            int actualRank = rules.RankHand(h);
            Assert.AreEqual(expectedRank, actualRank);
        }
        [DataTestMethod]
        [DataRow("JC TC 9C 8C 7C", "Straight Flush, Jack High")]
        [DataRow("AC KC QC JC TC", "Royal Flush")]
        [DataRow("TC TS TD TH 4C", "Four of a Kind, Tens")]
        [DataRow("AC AD AH 6C 6S", "Full House, Aces over Sixes")]
        [DataRow("8C 7C 6C 5C 2C", "Flush, Eight High")]
        [DataRow("6C 5H 4D 3D 2C", "Straight, Six High")]
        [DataRow("AH KS QC JH TD", "Broadway Straight")]
        [DataRow("KC KH KS 8C 7C", "Three of a Kind, Kings")]
        [DataRow("5C 5D 2C 2S AD", "Two Pair, Fives and Twos")]
        [DataRow("4C 4D 8S 7D 2H", "One Pair, Fours")]
        [DataRow("7S 5H 4C 3D 2C", "High Card, Seven")]
        [DataRow("9C 8D 7C 6S 5D", "Straight, Nine High")]
        public void TestNameHand(string tc, string expectedName)
        {
            PokerRules rules = new FiveCardDrawPokerRules();

            Hand h = CreateHand(tc);
            string handName = rules.NameHand(h);
            Assert.AreEqual(expectedName, handName);
            
        }
        private Hand CreateHand(string cardNames)
        {
            Hand h = new PokerHand();
            foreach (string cardName in cardNames.Split(' '))
            {
                CardFace f;

                CardSuit s;

                switch (cardName[1])
                {
                    case 'C': s = CardSuit.Club; break;
                    case 'S': s = CardSuit.Spade; break;
                    case 'H': s = CardSuit.Heart; break;
                    case 'D': s = CardSuit.Diamond; break;
                    default: throw new Exception("Cannot find suit\n");
                }

                switch (cardName[0])
                {
                    case 'J': f = CardFace.Jack; break;
                    case 'K': f = CardFace.King; break;
                    /*case 'F': f = CardFace.AceHigh; break;*/ //repersenting high ace = 14
                    case 'Q': f = CardFace.Queen; break;
                    case 'T': f = CardFace.Ten; break;
                    case '9': f = CardFace.Nine; break;
                    case '8': f = CardFace.Eight; break;
                    case '7': f = CardFace.Seven; break;
                    case '6': f = CardFace.Six; break;
                    case '5': f = CardFace.Five; break;
                    case '4': f = CardFace.Four; break;
                    case '3': f = CardFace.Three; break;
                    case '2': f = CardFace.Two; break;
                    case 'A': f = CardFace.Ace; break;
                    default: throw new Exception("Cannot find Face\n");
                }

                h.Add(new PokerCard(s, f));

            }
            return h;
        }

        [DataTestMethod]
        [DataRow("JC TC 9C 8C 7C", "5C 5D 5H 5S 3D", -1)]
        [DataRow("2S JS 4S 5S TS", "8H 3C 8D 3S 8S", 1)]
        [DataRow("JC TC 9C 8C 7C", "TD 9D 8D 7D 6D", -1)]
        [DataRow("AC AH AD AS KC", "QH QD QS QC JD", -1)]
        [DataRow("AC AH AD AS JC", "AC AH AD AS KD", 1)]
        [DataRow("AC AH AD AS KC", "AC AH AD AS KD", 0)]
        [DataRow("JC JH JD 8C 8S", "TS TH TD 9C 9S", -1)]
        [DataRow("JC JH JD 8C 8S", "JS JH JD 9C 9S", 1)]
        [DataRow("JH JD JC 8H 8C", "JD JH JC 8S 8D", 0)]
        [DataRow("AC JC TC 9C 8C", "KD JD TD 9D 8D", -1)]
        [DataRow("AC KC TC 9C 8C", "AD JD TD 9D 8D", -1)]
        [DataRow("AC KC QC 9C 8C", "AD KD TD 9D 8D", -1)]
        [DataRow("AC KC QC TC 8C", "AD KD QD 9D 8D", -1)]
        [DataRow("AC KC QC TC 9C", "AD KD QD TD 8D", -1)]
        [DataRow("AC KC QC TC 8C", "AD KD QD TD 8D", 0)]
        [DataRow("6D 5C 4S 3H 2D", "7D 6H 5S 4C 3D", 1)]
        [DataRow("9D 8C 7S 6H 5D", "9H 8S 7C 6D 5H", 0)]
        [DataRow("QD QC QH 9H 2D", "JD JH JS TC 3D", -1)]
        [DataRow("8D 8C 8H 9H 2D", "8D 8H 8S TC 3D", 1)]
        [DataRow("6D 6C 6H 9H 2D", "6D 6H 6S 9C 3D", 1)]
        [DataRow("QD QC QH 9H 2D", "QD QH QS 9C 2D", 0)]
        [DataRow("QD QC 9H 9S 5D", "JD JH TS TC 3D", -1)]
        [DataRow("QD QC 9H 9S 5D", "QS QH TS TC 3D", 1)]
        [DataRow("QD QC 9H 9S 5D", "JD JH 9S 9C 3D", -1)]
        [DataRow("QD QC 9H 9S 5D", "QD QH 9S 9C 5D", 0)]
        [DataRow("7D 7C TH 6S 5D", "8S 8H 9S 7C 4D", 1)] 
        [DataRow("8D 8C TH 6S 5D", "8S 8H 9S 7C 4D", -1)]
        [DataRow("8D 8C TH 6S 5D", "8S 8H TS 7C 4D", 1)]
        [DataRow("8D 8C TH 7S 5D", "8S 8H TS 7C 4D", -1)]
        [DataRow("8D 8C TH 7S 4C", "8S 8H TS 7C 4D", 0)]
        [DataRow("AD TC 9H 5S 4D", "KS JH 7S 6C 3D", -1)]
        [DataRow("AD TC 9H 5S 4D", "AS JH 7S 6C 3D", 1)]
        [DataRow("AD JC 9H 5S 4D", "AS JH 7S 6C 3D", -1)]
        [DataRow("AD JC 9H 5S 4D", "AS JH 9S 6C 3D", 1)]
        [DataRow("AD JC 9H 5S 4D", "AS JH 9S 5C 3D", -1)]
        [DataRow("AD JC 7H 6S 3S", "AS JH 7S 6C 3D", 0)]
        public void testCompare(string tc1, string tc2, int expectedResult)
        {
            Hand h1 = CreateTestHand(tc1);
            Hand h2 = CreateTestHand(tc2);

            PokerRules rules = new FiveCardDrawPokerRules();
            int result = rules.Compare(h1, h2);

            Assert.AreEqual (expectedResult, result);

        }

        Hand CreateTestHand(string tc)
        {
            PokerHand h = new PokerHand();
            string[] parts = tc.Split(' ');
            foreach (string part in parts)
            {
                if (part.Length < 2) throw new Exception("Error in spacing");
                CardFace face;
                switch (part[0])
                {
                    case 'J': face = CardFace.Jack; break;
                    case 'K': face = CardFace.King; break;
                    case 'F': face = CardFace.Ace; break; //repersenting high ace = 14
                    case 'Q': face = CardFace.Queen; break;
                    case 'T': face = CardFace.Ten; break;
                    case '9': face = CardFace.Nine; break;
                    case '8': face = CardFace.Eight; break;
                    case '7': face = CardFace.Seven; break;
                    case '6': face = CardFace.Six; break;
                    case '5': face = CardFace.Five; break;
                    case '4': face = CardFace.Four; break;
                    case '3': face = CardFace.Three; break;
                    case '2': face = CardFace.Two; break;
                    case 'A': face = CardFace.Ace; break;
                    default: throw new Exception("Invalid card face"); 

                }

                CardSuit suit;
                switch (part[1])
                {
                    case 'C': suit = CardSuit.Club; break;
                    case 'D': suit = CardSuit.Diamond; break;
                    case 'H': suit = CardSuit.Heart; break;
                    case 'S': suit = CardSuit.Spades; break;
                    default: suit = CardSuit.Spades; break;

                }
                h.Add(new PokerCard(suit, face));
            }

            return h;



        }
    }
}


