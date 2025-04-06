using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_Game.EnumClasses;
using RPG_Game.Interfaces;

namespace RPG_Game.InputHandlers
{
    public class DropHandler: InputHandlerBaseClass
    {
        protected override bool Process(ConsoleKeyInfo consoleKey, Game game)
        {
            if (char.ToUpper(consoleKey.KeyChar) != 'X')
                return false;
            if (game.player.Inventory.Count == 0 && game.player.LeftHand == null && game.player.RightHand == null)
            {
                GameDisplayer.Instance.AddNotification("There is nothing to drop at the moment. Pick up some item to be able to equip or drop it!");
                return true;
            }
            GameDisplayer.Instance.AddNotification("Enter 'A' if you want to drop all the items you have;\n" +
                "Enter 'L' to drop an item from your left hand, 'R' - from your right hand;\n" +
                "Enter a digit (0-9) to drop the concrete item from the inventory");
            ConsoleKeyInfo additionalConsoleKey = Console.ReadKey(true);
            char additionalChar = char.ToUpper(additionalConsoleKey.KeyChar);
            int index = additionalChar - 48;
            if (index == 0) index += 10;
            if (index > 0 && index <= 10)
            {
                if (index > game.player.Inventory.Count)
                {
                    GameDisplayer.Instance.AddNotification($"You don't have an item with this number in your inventory! Try again!");
                    return true;
                }
                IItem chosenItem = game.player.Inventory[index - 1];
                game.player.DropItemFromInventory(chosenItem, game.room);
                return true;
            }

            switch (additionalChar)
            {
                case 'A':
                    GameDisplayer.Instance.AddNotification("Are you sure you want to empty both of your hands and your inventory? (Y - yes, N - no)");
                    ConsoleKeyInfo agreementConsoleKey = Console.ReadKey(true);
                    if (char.ToUpper(agreementConsoleKey.KeyChar) == 'N')
                    {
                        GameDisplayer.Instance.AddNotification("Operation cancelled.");
                        return true;
                    }
                    else if (char.ToUpper(agreementConsoleKey.KeyChar) == 'Y')
                    {
                        game.player.newDropItemFromHand(Hand.Left, game.room);
                        game.player.newDropItemFromHand(Hand.Right, game.room);
                        while (game.player.Inventory.Count > 0)
                        {
                            IItem item = game.player.Inventory[0];
                            game.player.DropItemFromInventory(item, game.room);
                        }
                        GameDisplayer.Instance.ClearNotifications();
                        GameDisplayer.Instance.AddNotification($"Everything dropped on the tile ({game.player.X}, {game.player.Y})");
                        return true;
                    }
                    else
                    {
                        GameDisplayer.Instance.AddNotification("Invalid input. Enter 'X' to see again all the options!");
                        return true;
                    }
                case 'L':
                    game.player.newDropItemFromHand(Hand.Left, game.room);
                    break;
                case 'R':
                    game.player.newDropItemFromHand(Hand.Right, game.room);
                    break;
            }
            return true;
        }
    }
}
