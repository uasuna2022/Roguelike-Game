using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.InputHandlers
{
    public class DrinkPotionHandler: InputHandlerBaseClass
    {
        protected override bool Process(ConsoleKeyInfo consoleKey, Game game)
        {
            if (consoleKey.Key != InputKeyConfiguration.DrinkPotion)
                return false;

            bool containsDrinkableItems = false;
            foreach (IItem item in game.player.Inventory)
            {
                if (item.IsDrinkable)
                {
                    containsDrinkableItems = true;
                    break;
                }
            }
            if (game.player.Inventory.Count == 0)
            {
                GameDisplayer.Instance.AddNotification($"Your inventory is empty & you can't drink any potion!");
                return true;
            }
            else if (!containsDrinkableItems)
            {
                GameDisplayer.Instance.AddNotification($"There are no potions in your inventory!");
                return true;
            }
            else
            {
                GameDisplayer.Instance.AddNotification($"Which potion would you like to drink? Choose a number from 1 to 0 (10)");
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
                if (!game.player.Inventory[index - 1].IsDrinkable)
                {
                    GameDisplayer.Instance.AddNotification($"You can't drink {game.player.Inventory[index - 1].GetDisplayName()}! " +
                        $"It's an undrinkable item!");
                    return true;
                }
                IPotion chosenPotion = (IPotion)game.player.Inventory[index - 1];
                chosenPotion.ConsumePotion(game.player);
            }
            return true;
        }
    }
}
