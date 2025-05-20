using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using RPG_Game.MVC_Pattern.Controller;

namespace RPG_Game.InputHandlers
{
    public class EquipHandler: InputHandlerBaseClass
    {
        protected override bool Process(ConsoleKeyInfo consoleKey, GameController controller)
        {
            if (consoleKey.Key != InputKeyConfiguration.Equip)
                return false;

            Player player = controller.GameState.Players[controller.LocalPlayerIdx];
            Room room = controller.GameState.Room;

            bool containsEquippableItems = false;
            foreach (IItem item in player.Inventory)
            {
                if (item.IsEquippable)
                {
                    containsEquippableItems = true;
                    break;
                }
            }
            if (player.Inventory.Count == 0)
            {
                //GameDisplayer.Instance.AddNotification($"Your inventory is empty & you can't equip any item!");
                player.Notify($"Your inventory is empty & you can't equip any item!");
                return true;
            }
            else if (!containsEquippableItems)
            {
                //GameDisplayer.Instance.AddNotification($"All the items in your inventory are unequippable!");
                player.Notify($"All the items in your inventory are unequippable!");
                return true;
            }
            else if (player.LeftHand != null && player.RightHand != null)
            {
                //GameDisplayer.Instance.AddNotification("Both of your hands are equipped! Unequip some of them to be able to reequip it!");
                player.Notify("Both of your hands are equipped! Unequip some of them to be able to reequip it!");
            }
            else
            {
                //GameDisplayer.Instance.AddNotification($"Which item would you like to equip? Choose a number from 1 to 0 (10)");
                player.Notify($"Which item would you like to equip? Choose a number from 1 to 0 (10)");
                int index = (char)Console.ReadKey(true).KeyChar - 48;
                if (index < 0 || index > 9)
                {
                    //GameDisplayer.Instance.AddNotification($"Invalid number! Choose a digit (0-9), not a letter or any other character");
                    player.Notify($"Invalid number! Choose a digit (0-9), not a letter or any other character");
                    return true;
                }
                if (index == 0) index += 10;
                if (index > player.Inventory.Count)
                {
                    //GameDisplayer.Instance.AddNotification($"You don't have an item with this number in your inventory!");
                    player.Notify($"You don't have an item with this number in your inventory!");
                    return true;
                }
                if (!player.Inventory[index - 1].IsEquippable)
                {
                    //GameDisplayer.Instance.AddNotification($"You can't equip {player.Inventory[index - 1].GetDisplayName()}! " +
                    //$"It's an unequippable item!");
                    player.Notify($"You can't equip {player.Inventory[index - 1].GetDisplayName()}! It's an unequippable item!");
                    return true;
                }
                IWeapon chosenWeapon = (IWeapon)player.Inventory[index - 1];
                player.EquipWeapon(chosenWeapon);
                //GameDisplayer.Instance.DrawPlayerStats(player);
                player.Refresh();
            }
            return true;
        }
    }
}
