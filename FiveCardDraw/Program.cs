using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveCardDraw
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Five Card Poker Game!");
            string player1Name = getName(1);
            string player2Name = getName(2);

            Player player1 = new Player(player1Name, 100.00M);
           Player player2 = new Player(player2Name, 100.00M);
            Game game = new Game(player1, player2);
            game.StartGame();     
            
        }

        public static string getName(int num)
        {
            Console.WriteLine($"Enter your name, player {num}");

            while (true)
            {
                string name = Console.ReadLine();
                if (string.IsNullOrEmpty(name.Trim()))
                {
                    Console.WriteLine("Name cannot be empty!");
                    continue;
                }

                return name;
            }

        }

    }
}
