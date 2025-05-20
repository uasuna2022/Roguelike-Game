using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_Game.MVC_Pattern.Controller;

namespace RPG_Game.InputHandlers
{
    public class DefaultHandler: InputHandlerBaseClass
    {
        protected override bool Process(ConsoleKeyInfo consoleKey, GameController controller)
        {
            Player p = controller.GameState.Players[controller.LocalPlayerIdx];

            //GameDisplayer.Instance.AddNotification("Unknown command. Press a valid key from a list of commands");
            p.Notify("Unknown command. Press a valid key from a list of commands");
            return true;
        }
    }
}
