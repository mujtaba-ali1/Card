using System;

//Pokercard single card concrete class
namespace Poker_Library
{
    public class PokerCard : Card
    {
        private CardSuit suit;
        private CardFace face;

        public PokerCard(CardSuit s, CardFace f)
        {
            suit = s;
            face = f;
        }
        public CardSuit CardSuit
        {
            get
            {
                return suit;
            }
        }
        public CardFace Face
        {
            get
            {
                return face;
            }
        }

        public override string ToString()
        {
            string cardValue = "";
            switch (face)
            {
                case CardFace.Two:
                    cardValue += "2";
                    break;
                case CardFace.Three:
                    cardValue += "3";
                    break;
                case CardFace.Four:
                    cardValue += "4";
                    break;
                case CardFace.Five:
                    cardValue += "5";
                    break;
                case CardFace.Six:
                    cardValue += "6";
                    break;
                case CardFace.Seven:
                    cardValue += "7";
                    break;
                case CardFace.Eight:
                    cardValue += "8";
                    break;
                case CardFace.Nine:
                    cardValue += "9";
                    break;
                case CardFace.Ten:
                    cardValue += "10";
                    break;
                case CardFace.Queen:
                    cardValue += "Q";
                    break;
                case CardFace.King:
                    cardValue += "K";
                    break;
                case CardFace.Jack:
                    cardValue += "J";
                    break;
                case CardFace.Ace:
                    cardValue += "A";
                    break;

                default:
                    throw new InvalidOperationException("Invalid Value entered\n");
            }

            switch (suit)
            {
                case CardSuit.Diamonds:
                    cardValue += "D";
                    break;
                case CardSuit.Spades:
                    cardValue += "S";
                    break;
                case CardSuit.Heart:
                    cardValue += "H";
                    break;
                case CardSuit.Club:
                    cardValue += "C";
                    break;
                default:
                    throw new InvalidOperationException("Invalid Value entered\n");

            }
            return cardValue;
        }

    }

}