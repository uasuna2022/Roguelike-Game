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
            if (char.ToUpper(consoleKey.KeyChar) != 'E')
                return false;
            game.player.PickUpItem(game.room);
            return true;
        }
    }
}
