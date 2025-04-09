using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.InputHandlers
{
    public class PickingUpHandler: InputHandlerBaseClass
    {
        protected override bool Process (ConsoleKeyInfo consoleKey, Game game)
        {
            if (consoleKey.Key != InputKeyConfiguration.PickItem)
                return false;
            game.player.PickUpItem(game.room);
            GameDisplayer.Instance.DrawCellStats(game.room.GetCell(game.player.X, game.player.Y));
            return true;
        }
    }
}
