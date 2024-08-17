using Poker_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace FiveCardDraw
{
    internal class Player
    {
        string name;
        decimal balance;
        Hand playingHand;
        Game playingGame;
        PokerRules rules;
        int turnInCardCount;
        public Player(string name, decimal initalBalance)
        {
            this.name = name;
            this.balance = initalBalance;
            this.turnInCardCount = -1;

        }

        public decimal Balance { get { return balance; } }
        public decimal Cards { get { return turnInCardCount; } }

        public string Name { get { return name; } }


        public decimal PlaceBet()
        {
            decimal bet = 0;


            bool betValid = false;




            while (!betValid)
            {
                try
                {
                    Console.WriteLine("\nHow much would you like to bet? ");
                    string line = Console.ReadLine();

                    bet = decimal.Parse(line);
                    if (bet <= 0 || bet > Balance)
                    {
                        Console.WriteLine("Bad bet input");
                        throw new Exception();
                    }
                    betValid = true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Try Again");
                    Console.WriteLine();
                }
            }
            balance -= bet;

            return bet;

        }

     
        public bool FoldCheck()
        {


            while (true)
            {
                Console.Write("Would you like to Fold? (F): ");
                string line = Console.ReadLine();

                //check if folded
                if (line.Trim() == "F" || line.Trim() == "f")
                {

                    return true; //break out if they fold
                }
                else if (string.IsNullOrEmpty(line) || line.Trim() != "N" || line.Trim() != "n")
                {
                    return false;
                }
                else
                {
                    Console.Write("Bad Input. Try Again: F for Fold, Enter for continue");
                }



            }

        }
        public Hand hand { get { return playingHand; } }

        public void Join(Game game)
        {
            playingGame = game;
            playingHand = null;
            rules = new FiveCardDrawPokerRules(); // get what type of game user chooses
        }

        public void PromptStartTurn()
        {
            Console.Write($"\n{name}, hit Enter to start your turn: ");
            Console.ReadLine();


        }
        public void GiveInitalHand(Hand h)
        {
            playingHand = h;
        }


        public void addPot(decimal pot)
        {
            balance += pot;
            Console.WriteLine($"{name} wins the pot");
        }
        public void DisplayHand()

        {

            Console.WriteLine($"\n{rules.NameHand(playingHand)} ");
            Console.WriteLine();
            foreach (Card c in playingHand)
            {
                Console.WriteLine($"{c.Face} of {c.CardSuit}");
            }
        }

        public void DisplayBalance()
        {
            Console.WriteLine($"Your Balance is: ${balance} ");
        }

        public int TurnedInReturn()
        {

            return turnInCardCount;
        }


        public Card[] turnInCards()
        {
            List<Card> selectedCards = new List<Card>();


            bool valid = false;

            while (!valid)
            {

                try
                {
                    Console.WriteLine("Here are your cards");

                    for (int i = 0; i < playingHand.Count(); i++)
                    {
                        Card c = playingHand.ElementAt(i);
                        Console.WriteLine($"\t[{i + 1}] {c.Face} of {c.CardSuit}");

                    }


                    Console.Write("Enter card indexes, seperated by spaces to trade in, or enter for none: ");
                    string line = Console.ReadLine();
                    List<int> index = new List<int>();
                    if (!string.IsNullOrEmpty(line))
                    {
                        foreach (string selectedIndex in line.Split(' '))
                        {
                            int i = int.Parse(selectedIndex) - 1;
                            if (i < 0 || i > playingHand.Count()) throw new Exception("Invalid index");
                            selectedCards.Add(playingHand.ElementAt(i));
                        }
                        selectedCards = new List<Card>(selectedCards.Distinct());
                    }
                    foreach (Card c in selectedCards.Distinct())
                    {
                        playingHand.Remove(c);
                    }

                    turnInCardCount = selectedCards.Count();
                    valid = true;

                }
                catch (Exception e) { Console.WriteLine("Try Again"); }

            }



            return selectedCards.ToArray();
        }
        public void replaceCards(Card[] cards)
        {
            foreach (Card card in cards) playingHand.Add(card);
        }




    }


}
