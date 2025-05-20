using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_Game.MVC_Pattern.Controller;

namespace RPG_Game.InputHandlers
{
    public class PickingUpHandler: InputHandlerBaseClass
    {
        protected override bool Process (ConsoleKeyInfo consoleKey, GameController controller)
        {
            if (consoleKey.Key != InputKeyConfiguration.PickItem)
                return false;

            Room room = controller.GameState.Room;
            Player player = controller.GameState.Players[controller.LocalPlayerIdx];

            player.PickUpItem(room);
            GameDisplayer.Instance.DrawCellStats(room.GetCell(player.X, player.Y));
            return true;
        }
    }
}
