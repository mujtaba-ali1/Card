using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poker_Library;
namespace PokerDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;

            Deck d = new AceHighPokerDeck(new Random());
            Deck d2 = new AceHighPokerDeck(new Random());

            d.Shuffle();
            PokerRules rules = new FiveCardDrawPokerRules();

            Console.WriteLine("\nPart A - Deal and Show Hand \n");

            Hand hand1 = rules.DealHand(d);
            Console.WriteLine($"Hand 1 Rank: {rules.RankHand(hand1)}");
            Console.WriteLine($"Hand 1 Name: {rules.NameHand(hand1)}");

            d.Shuffle();

            Console.WriteLine("\nPart B - Compare Two Hands\n");
            Hand hand2 = rules.DealHand(d);


            Hand hand3 = rules.DealHand(d2);
            Console.WriteLine($"Hand 2 Name: {rules.NameHand(hand2)}");
            Console.WriteLine($"Hand 2 Rank: {rules.RankHand(hand2)}");

            Console.WriteLine($"\nHand 3 Name: {rules.NameHand(hand3)}");
            Console.WriteLine($"Hand 3 rank: {rules.RankHand(hand3)}");

         
            if (rules.Compare(hand2, hand3) == 1)
            {
                Console.WriteLine("\nThe winner is hand 2");

            }
            else if(rules.Compare(hand2, hand3) == -1)
            {
                Console.WriteLine("The winner is hand 3");
            }
            else if (rules.Compare(hand2, hand3) == 0)
            {
                Console.WriteLine("It is a tie");

            }

        }
    }
}
           
   
