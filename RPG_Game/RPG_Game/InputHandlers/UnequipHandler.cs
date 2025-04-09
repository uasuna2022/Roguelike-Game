using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.InputHandlers
{
    public class UnequipHandler: InputHandlerBaseClass
    {
        protected override bool Process(ConsoleKeyInfo consoleKey, Game game)
        {
            if (consoleKey.Key != InputKeyConfiguration.Unequip)
                return false;

            if (game.player.LeftHand == null && game.player.RightHand == null)
            {
                GameDisplayer.Instance.AddNotification("Currently both of your hands are empty! Equip some weapon (I) to be able to unequip it later!");
                return true;
            }
            GameDisplayer.Instance.AddNotification("Which hand would you like to unequip?");
            char handChar = Console.ReadKey(true).KeyChar;
            switch (char.ToUpper(handChar))
            {
                case 'L':
                    game.player.UnequipWeapon(true, game.room);
                    GameDisplayer.Instance.DrawPlayerStats(game.player);
                    break;
                case 'R':
                    game.player.UnequipWeapon(false, game.room);
                    GameDisplayer.Instance.DrawPlayerStats(game.player);
                    break;
                default:
                    GameDisplayer.Instance.AddNotification("Invalid choice. Press 'L' or 'R'.");
                    break;
            }

            return true;
        }
    }
}
