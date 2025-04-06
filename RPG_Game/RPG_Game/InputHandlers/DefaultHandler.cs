using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.InputHandlers
{
    public class DefaultHandler: InputHandlerBaseClass
    {
        protected override bool Process(ConsoleKeyInfo consoleKey, Game game)
        {
            GameDisplayer.Instance.AddNotification("Unknown command. Press a valid key from a list of commands");
            return true;
        }
    }
}
