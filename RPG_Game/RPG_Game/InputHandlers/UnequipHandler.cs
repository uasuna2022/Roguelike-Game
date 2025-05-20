using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using RPG_Game.MVC_Pattern.Controller;

namespace RPG_Game.InputHandlers
{
    public class UnequipHandler: InputHandlerBaseClass
    {
        protected override bool Process(ConsoleKeyInfo consoleKey, GameController controller)
        {
            if (consoleKey.Key != InputKeyConfiguration.Unequip)
                return false;

            Player player = controller.GameState.Players[controller.LocalPlayerIdx];
            Room room = controller.GameState.Room;

            if (player.LeftHand == null && player.RightHand == null)
            {
                //GameDisplayer.Instance.AddNotification("Currently both of your hands are empty! Equip some weapon (I) to be able to unequip it later!");
                player.Notify("Currently both of your hands are empty! Equip some weapon (I) to be able to unequip it later!");
                return true;
            }
            //GameDisplayer.Instance.AddNotification("Which hand would you like to unequip?");
            player.Notify("Which hand would you like to unequip?");
            char handChar = Console.ReadKey(true).KeyChar;
            switch (char.ToUpper(handChar))
            {
                case 'L':
                    player.UnequipWeapon(true, room);
                    //GameDisplayer.Instance.DrawPlayerStats(game.player);
                    player.Refresh();
                    break;
                case 'R':
                    player.UnequipWeapon(false, room);
                    //GameDisplayer.Instance.DrawPlayerStats(game.player);
                    player.Refresh();
                    break;
                default:
                    //GameDisplayer.Instance.AddNotification("Invalid choice. Press 'L' or 'R'.");
                    player.Notify("Invalid choice. Press 'L' or 'R'.");
                    break;
            }

            return true;
        }
    }
}
