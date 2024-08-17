using Poker_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveCardDraw
{
    internal class Game
    {
        Player player1, player2;
        decimal pot;
        Deck deck;
        Player currPlayer;
        PokerRules rules;

        public Game(Player p1, Player p2)
        {
            player1 = p1;
            player2 = p2;
            player1.Join(this);
            player2.Join(this);

            deck = new AceHighPokerDeck(new Random());
            rules = new FiveCardDrawPokerRules();


        }

        public void StartGame()
        {
            deck.Shuffle();
            

            player1.GiveInitalHand(rules.DealHand(deck));
            player2.GiveInitalHand(rules.DealHand(deck));

            for (int i = 1; i <= 2; i++) //change round later to include n rounds flexibility
            {
                if (i > 1)
                {
                    if (player1.TurnedInReturn() > -1) Console.WriteLine($"Your opponent chose to replace {player2.TurnedInReturn()}");
                }

                Console.WriteLine($"Round Number: {i}");

                //player 1 
                currPlayer = player1;
                Console.WriteLine($"\nPot ${pot}");
                player1.PromptStartTurn();
                player1.DisplayBalance();
                Console.WriteLine("\nYour Hand is: ");
                player1.DisplayHand();



                if (player1.FoldCheck())
                {
                    Console.WriteLine($"{player2.Name} wins the hand");
                    player2.addPot(pot);
                    return;
                }
                PlayTurn(currPlayer, i);
                player1 = currPlayer;


                Console.Clear(); Console.WriteLine($"{player1.Name}'s cards have been hidden");
                if (player1.TurnedInReturn() > -1) Console.WriteLine($"{player1.Name} chose to replace {player1.TurnedInReturn()} cards");


                //player 2
                currPlayer = player2;
                player2.PromptStartTurn();
                player2.DisplayBalance();
                Console.WriteLine($"Your Hand is: ");
                player2.DisplayHand();

                if (player2.FoldCheck())
                {
                    Console.WriteLine($"{player1.Name} wins the hand");
                    player1.addPot(pot);
                    return;
                }

                PlayTurn(currPlayer, i);
                player2 = currPlayer;
                Console.Clear(); Console.WriteLine($"{player2.Name}'s cards have been hidden");




            }

            showDown(player1, player2);


        }

        public void PlayTurn(Player player, int round)
        {
            decimal bet = player.PlaceBet();
            pot += bet;


            Console.WriteLine($"\nBet ${bet}");
            Console.WriteLine($"Pot ${pot}");

            player.DisplayBalance();
            Console.WriteLine();

            if (round == 1)
            {
                Card[] selected = player.turnInCards();
                if (selected.Length > 0)
                {
                    Card[] newCards = deck.Deal(selected.Length);
                    player.replaceCards(newCards);
                }
            }

        }

        public void showDown(Player player1, Player player2)
        {
            Console.WriteLine("\nHere is the showdown: \n");

            Console.WriteLine($"{player1.Name}'s hand: {rules.NameHand(player1.hand)}");
            player1.DisplayHand();

            Console.WriteLine($"{player2.Name}'s hand: {rules.NameHand(player2.hand)}\n");
            player2.DisplayHand();


            switch (rules.Compare(player1.hand, player2.hand))
            {
                case 0: Console.WriteLine("Tie!"); break;
                case -1:
                    {
                        player2.addPot(pot);
                        Console.WriteLine($"{player2.Name}'s wins"); break;
                    }
                case 1:
                    {
                        player1.addPot(pot);
                        Console.WriteLine($"{player1.Name}'s wins"); break;
                    }

                default: throw new Exception("Error in computing");
            }

            Console.WriteLine($"{player1.Name}'s Balance: {player1.Balance}");
            Console.WriteLine($"{player1.Name}'s Balance: {player2.Balance}");

            pot = 0;

        }





    }
}
