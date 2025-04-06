using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.InputHandlers
{
    public class QuitHandler: InputHandlerBaseClass
    {
        protected override bool Process(ConsoleKeyInfo consoleKey, Game game)
        {
            if (char.ToUpper(consoleKey.KeyChar) != 'Q')
                return false;
            game.gameIsRunning = false;
            GameDisplayer.Instance.AddNotification("Exiting game...");
            Console.ReadKey();
            return true;
        }
    }
}
