using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.AccessControl;
using static Poker_Library.FiveCardDrawPokerRules;

namespace Poker_Library
{
    public class FiveCardDrawPokerRules : PokerRules
    {
        public class HandInfo
        {
            public HandInfo()
            {
                highCards = new List<CardFace>();
            }
            public int rank
            {
                get;
                set;
            }
            public string name
            {
                get;
                set;
            }

            public List<CardFace> highCards
            {
                get;
            }
        }

        public int Compare(Hand h1, Hand h2)
        {
            HandInfo hi1 = GetHandInfo(h1);
            HandInfo hi2 = GetHandInfo(h2);

            // 1 wins
            if (hi1.rank < hi2.rank) return -1;

            //2 wins
            else if (hi1.rank > hi2.rank) return 1;

            else if (hi1.rank == hi2.rank)
            {

                //rank tie
                for (int i = 0; i < hi1.highCards.Count() && i < hi2.highCards.Count(); i++)
                {
                    {
                        if (hi1.highCards[i] > hi2.highCards[i]) return -1; // should be 1
                        else if (hi1.highCards[i] < hi2.highCards[i]) return 1; // should be -1
                    }
                }

            }

            return 0; // tie
        }

        private HandInfo GetHandInfo(Hand h)
        {
            HandInfo handInfo = new HandInfo();
            PokerRules rules = new FiveCardDrawPokerRules();
            handInfo.rank = rules.RankHand(h);

            List<Card> handList = new List<Card>(
              h.OrderByDescending<Card, CardFace>((c) =>
              {
                  return c.Face;
              }));

            switch (handInfo.rank)
            {
                case 2:
                    //straight flush
                    handInfo.highCards.Add(handList[0].Face);
                    break;

                //4 of a kind
                case 3:
                    handInfo.highCards.Add(handList[2].Face);
                    if (handList[2].Face == handList[0].Face) handInfo.highCards.Add(handList[4].Face);
                    else if (handList[2].Face != handList[0].Face) handInfo.highCards.Add(handList[0].Face);
                    break;

                //full house
                case 4:
                    if (handList[2].Face == handList[1].Face)
                    {
                        handInfo.highCards.Add(handList[0].Face);  // 3-kind
                        handInfo.highCards.Add(handList[3].Face);  // pair
                    }
                    else handInfo.highCards.Add(handList[1].Face);
                    {
                        handInfo.highCards.Add(handList[2].Face);  // 3-kind
                        handInfo.highCards.Add(handList[0].Face);   // pair
                    }
                    break;

                //Flush
                case 5:
                    for (int i = 0; i < handList.Count(); i++) handInfo.highCards.Add(handList[i].Face);

                    break;

                //Straight
                case 6:
                    for (int i = 0; i < handList.Count(); i++) handInfo.highCards.Add(handList[i].Face);
                    break;

                case 7:
                    handInfo.highCards.Add(handList[2].Face);
                    for (int i = 0; i < handList.Count(); i++)
                    {
                        if (handList[i].Face != handList[2].Face)
                            handInfo.highCards.Add(handList[i].Face);
                    }
                    break;
                case 8:
                    if (handList[0].Face == handList[1].Face)
                        handInfo.highCards.Add(handList[0].Face);
                    if (handList[2].Face == handList[3].Face)
                        handInfo.highCards.Add(handList[2].Face);
                    handInfo.highCards.Add(handList[4].Face);
                    break;
                case 9:
                    for (int i = 0; i < handList.Count() - 1; i++)
                    {
                        if (handList[i].Face == handList[i + 1].Face)
                        {
                            handInfo.highCards.Add(handList[i].Face);
                            break;
                        }
                    }
                    for (int i = 0; i < handList.Count(); i++)
                    {
                        if (!handInfo.highCards.Contains(handList[i].Face))
                            handInfo.highCards.Add(handList[i].Face);
                    }
                    break;

                case 10:
                    for (int i = 0; i < handList.Count(); i++) handInfo.highCards.Add(handList[i].Face);
                    break;

            }
            return handInfo;
        }

        public Hand DealHand(Deck d)
        {
            return new PokerHand(d.Deal(5));

        }

        public string NameHand(Hand h)
        {
            HandInfo handInfo = GetHandInfo(h);
            handInfo.name = NameRankHand(h, handInfo);

            //Royal Flush & Straight Flush
            if (handInfo.rank == 2)
            {
                if (handInfo.highCards[0] == CardFace.Ace) return "Royal Flush";
                else return $"Straight Flush, {handInfo.highCards[0]} High";
            }
            //4ofAKind
            else if (handInfo.rank == 3) return $"Four of a Kind, {Plural(handInfo.highCards[0])}";

            return handInfo.name;
        }

        public int RankHand(Hand h)
        {

            List<Card> handList = new List<Card>(
              h.OrderByDescending<Card, CardFace>((c) =>
              {
                  return c.Face;
              }));

            if (isStraight(handList) && isFlush(handList)) return 2;

            if (isFourOfAKind(handList)) return 3;

            if (isFullhosue(handList))
            {
                return 4;
            }

            if (isFlush(handList)) return 5;

            if (isStraight(handList)) return 6;

            if (isThreeOfAKind(handList)) return 7;

            if (isTwoPair(handList)) return 8;

            if (isOnePair(handList)) return 9;

            return 10;
        }

        private string NameRankHand(Hand hand, HandInfo handInfo)
        {
            List<Card> handList = new List<Card>(
              hand.OrderByDescending<Card, CardFace>((c) =>
              {
                  return c.Face;
              }));

            switch (handInfo.rank)
            {
                case 2:
                    {
                        if (handInfo.highCards[0] == CardFace.Ace)
                        {
                            handInfo.name = "Royal Flush";
                            break;

                        }
                        else
                        {
                            handInfo.name =
                              "Straight Flush, " + handInfo.highCards[0] + " High";
                        }
                        break;

                    }

                case 3:
                    {

                        handInfo.name = "Four of a Kind, " + Plural(handInfo.highCards[0]);
                        break;

                    }

                case 4: //FULL HOUSE
                    {

                        handInfo.name = $"Full House, {Plural(handInfo.highCards[0])} over {Plural(handInfo.highCards[1])}";
                        break;

                    }

                case 5:
                    handInfo.name = "Flush, " + (handInfo.highCards[0]) + " High";
                    break;
                case 6:
                    {
                        if (handList[0].Face == CardFace.Ace)
                        {
                            handInfo.name = "Broadway Straight";
                            break;
                        }
                        else
                        {
                            handInfo.name =
                              "Straight, " + handList[0].Face.ToString() + " High";
                            break;
                        }
                    }
                case 7:
                    handInfo.name = "Three of a Kind, " + Plural(handList[2].Face);
                    break;
                case 8:

                    handInfo.name = "Two Pair, " + Plural(handList[1].Face) + " and " +
                      Plural(handList[3].Face);
                    break;

                case 9:
                    {
                        for (int i = 0; i < handList.Count() - 1; i++)
                        {
                            if (handList[i].Face == handList[i + 1].Face)
                            {
                                handInfo.name = "One Pair, " + Plural(handList[i].Face);
                                break;
                            }
                        }
               
                    }
                    break;

                case 10:
                    handInfo.name = "High Card, " + (handList[0].Face);
                    break;
                default:
                    handInfo.name = "unknown ";
                    break;
            }
            return handInfo.name;
        }

        private string Plural(CardFace f)
        {
            return f.ToString() + (f == CardFace.Six ? "es" : "s");
        }

        //helper methods for ranking
        public bool isStraight(List<Card> handList)
        {
            for (int i = 0; i < 4; i++)
            {
                if (handList[i].Face - handList[i + 1].Face != 1) return false;
            }
            return true;
        }

        public bool isFlush(List<Card> handList)
        {
            for (int i = 0; i < 4; i++)
            {
                if (handList[i].CardSuit != handList[i + 1].CardSuit) return false;
            }
            return true;
        }

        private bool isThreeOfAKind(List<Card> handList)
        {
            if (handList[0].Face == handList[2].Face ||
              handList[1].Face == handList[3].Face ||
              handList[2].Face == handList[4].Face)
                return true;

            else return false;
        }

        private bool isFourOfAKind(List<Card> handList)
        {
            if (handList[0].Face == handList[3].Face ||
              handList[1].Face == handList[4].Face)
                return true;

            else return false;

        }

        private bool isFullhosue(List<Card> handList)
        {
            if (handList[0].Face == handList[2].Face && handList[3].Face == handList[4].Face)
            {
                return true;
            }

            if (handList[0].Face == handList[1].Face && handList[2].Face == handList[4].Face)
            {
                return true;
            }
            return false;
        }

        private bool isTwoPair(List<Card> handList)
        {

            int pair = 0;
            for (int i = 0; i < 4; i++)
            {
                if (handList[i].Face == handList[i + 1].Face)
                {
                    pair++;
                    i++;
                }
            }
            if (pair == 2) return true;
            else return false;

        }

        private bool isOnePair(List<Card> handList)
        {
            //same logic from two pair but changed to find if only one pair exists
            int pair = 0;
            for (int i = 0; i < 4; i++)
            {
                if (handList[i].Face == handList[i + 1].Face)
                {
                    pair++;
                    i++;
                }
            }
            if (pair == 1) return true; // only difference
            else return false;

        }

    }
}