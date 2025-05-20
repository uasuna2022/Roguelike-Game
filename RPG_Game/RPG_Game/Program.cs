using RPG_Game.Decorators;
using RPG_Game.Interfaces;
using RPG_Game.Items.UnusableItems;
using RPG_Game.Items.Weapons;
using RPG_Game.MVC_Pattern.Controller;
using System;
using System.Runtime.InteropServices;

namespace RPG_Game
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            int version = 0;
            bool validInput = false;
            while (!validInput)
            {
                Console.Write("Enter dungeon version (1, 2, or 3): ");
                string? input = Console.ReadLine();

                if (int.TryParse(input, out version) && version >= 1 && version <= 3)
                {
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 1, 2, or 3.");
                }
            }

            /*
            Game game = new Game(version);
            game.BuildGame(version);
            game.StartGame();
            */
            GameController gameController = new GameController(version);
            gameController.Run();
        }
    }
}
