using RPG_Game.Decorators;
using RPG_Game.Interfaces;
using RPG_Game.Items.UnusableItems;
using RPG_Game.Items.Weapons;

namespace RPG_Game
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game new_game = new Game();
            new_game.StartGame();
        }
    }
}
