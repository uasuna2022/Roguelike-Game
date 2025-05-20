using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_Game.EnumClasses;
using RPG_Game.Interfaces;
using RPG_Game.MVC_Pattern.Controller;

namespace RPG_Game.InputHandlers
{
    public class DropHandler: InputHandlerBaseClass
    {
        protected override bool Process(ConsoleKeyInfo consoleKey, GameController controller)
        {
            if (consoleKey.Key != InputKeyConfiguration.DropItem)
                return false;

            Player player = controller.GameState.Players[controller.LocalPlayerIdx];
            Room room = controller.GameState.Room;

            if (player.Inventory.Count == 0 && player.LeftHand == null && player.RightHand == null)
            {
                //GameDisplayer.Instance.AddNotification("There is nothing to drop at the moment. Pick up some item to be able to equip or drop it!");
                player.Notify("There is nothing to drop at the moment. Pick up some item to be able to equip or drop it!");
                return true;
            }
            player.Notify("Enter 'A' if you want to drop all the items you have;");
            player.Notify("Enter 'L' to drop an item from your left hand, 'R' - from your right hand;");
            player.Notify("Enter a digit (0-9) to drop the concrete item from the inventory");
            ConsoleKeyInfo additionalConsoleKey = Console.ReadKey(true);
            char additionalChar = char.ToUpper(additionalConsoleKey.KeyChar);
            int index = additionalChar - 48;
            if (index == 0) index += 10;
            if (index > 0 && index <= 10)
            {
                if (index > player.Inventory.Count)
                {
                    //GameDisplayer.Instance.AddNotification($"You don't have an item with this number in your inventory! Try again!");
                    player.Notify($"You don't have an item with this number in your inventory! Try again!");
                    return true;
                }
                IItem chosenItem = player.Inventory[index - 1];
                player.DropItemFromInventory(chosenItem, room);
                //GameDisplayer.Instance.DrawCellStats(room.GetCell(player.X, player.Y));
                player.Refresh();
                return true;
            }

            switch (additionalChar)
            {
                case 'A':
                    //GameDisplayer.Instance.AddNotification("Are you sure you want to empty both of your hands and your inventory? (Y - yes, N - no)");
                    player.Notify("Are you sure you want to empty both of your hands and your inventory? (Y - yes, N - no)");
                    ConsoleKeyInfo agreementConsoleKey = Console.ReadKey(true);
                    if (char.ToUpper(agreementConsoleKey.KeyChar) == 'N')
                    {
                        //GameDisplayer.Instance.AddNotification("Operation cancelled.");
                        player.Notify("Operation cancelled.");
                        return true;
                    }
                    else if (char.ToUpper(agreementConsoleKey.KeyChar) == 'Y')
                    {
                        player.newDropItemFromHand(Hand.Left, room);
                        player.newDropItemFromHand(Hand.Right, room);
                        while (player.Inventory.Count > 0)
                        {
                            IItem item = player.Inventory[0];
                            player.DropItemFromInventory(item, room);
                        }
                        GameDisplayer.Instance.ClearNotifications();
                        GameDisplayer.Instance.AddNotification($"Everything dropped on the tile ({player.X}, {player.Y})");
                        GameDisplayer.Instance.DrawCellStats(room.GetCell(player.X, player.Y));
                        player.Notify($"Everything dropped on the tile ({player.X}, {player.Y})");
                        player.Refresh();
                        return true;
                    }
                    else
                    {
                        //GameDisplayer.Instance.AddNotification("Invalid input. Enter 'X' to see again all the options!");
                        player.Notify("Invalid input. Enter 'X' to see again all the options!");
                        return true;
                    }
                case 'L':
                    player.newDropItemFromHand(Hand.Left, room);
                    //GameDisplayer.Instance.DrawCellStats(room.GetCell(player.X, player.Y));
                    player.Refresh();
                    break;
                case 'R':
                    player.newDropItemFromHand(Hand.Right, room);
                    //GameDisplayer.Instance.DrawCellStats(room.GetCell(player.X, player.Y));
                    player.Refresh();
                    break;
                default:
                    //GameDisplayer.Instance.AddNotification("Invalid input. Enter 'X' to see again all the options!");
                    player.Notify("Invalid input. Enter 'X' to see again all the options!");
                    break;
            }
            return true;
        }
    }
}
