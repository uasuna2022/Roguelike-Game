using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_Game.MVC_Pattern.Controller;

namespace RPG_Game.InputHandlers
{
    public class QuitHandler: InputHandlerBaseClass
    {
        protected override bool Process(ConsoleKeyInfo consoleKey, GameController controller)
        {
            if (consoleKey.Key != InputKeyConfiguration.Quit)
                return false;

            controller.RequestQuit(); // I need to handle RequestQuit inside controller class
            GameDisplayer.Instance.AddNotification("Exiting game...");
            Console.ReadKey();
            return true;
        }
    }
}
