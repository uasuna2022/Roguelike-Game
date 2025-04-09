using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.InputHandlers
{
    public class EquipHandler: InputHandlerBaseClass
    {
        protected override bool Process(ConsoleKeyInfo consoleKey, Game game)
        {
            if (consoleKey.Key != InputKeyConfiguration.Equip)
                return false;

            bool containsEquippableItems = false;
            foreach (IItem item in game.player.Inventory)
            {
                if (item.IsEquippable)
                {
                    containsEquippableItems = true;
                    break;
                }
            }
            if (game.player.Inventory.Count == 0)
            {
                GameDisplayer.Instance.AddNotification($"Your inventory is empty & you can't equip any item!");
                return true;
            }
            else if (!containsEquippableItems)
            {
                GameDisplayer.Instance.AddNotification($"All the items in your inventory are unequippable!");
                return true;
            }
            else if (game.player.LeftHand != null && game.player.RightHand != null)
            {
                GameDisplayer.Instance.AddNotification("Both of your hands are equipped! Unequip some of them to be able to reequip it!");
            }
            else
            {
                GameDisplayer.Instance.AddNotification($"Which item would you like to equip? Choose a number from 1 to 0 (10)");
                int index = (char)Console.ReadKey(true).KeyChar - 48;
                if (index < 0 || index > 9)
                {
                    GameDisplayer.Instance.AddNotification($"Invalid number! Choose a digit (0-9), not a letter or any other character");
                    return true;
                }
                if (index == 0) index += 10;
                if (index > game.player.Inventory.Count)
                {
                    GameDisplayer.Instance.AddNotification($"You don't have an item with this number in your inventory!");
                    return true;
                }
                if (!game.player.Inventory[index - 1].IsEquippable)
                {
                    GameDisplayer.Instance.AddNotification($"You can't equip {game.player.Inventory[index - 1].GetDisplayName()}! " +
                        $"It's an unequippable item!");
                    return true;
                }
                IWeapon chosenWeapon = (IWeapon)game.player.Inventory[index - 1];
                game.player.EquipWeapon(chosenWeapon);
                GameDisplayer.Instance.DrawPlayerStats(game.player);
            }
            return true;
        }
    }
}
