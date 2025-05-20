using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_Game.MVC_Pattern.Controller;

namespace RPG_Game.Interfaces
{
    public interface IInputHandler
    {
        IInputHandler SetNext(IInputHandler next);
        void HandleInput(ConsoleKeyInfo consoleKey, GameController controller);
    }
}
