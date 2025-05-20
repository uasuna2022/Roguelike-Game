using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_Game.MVC_Pattern.Controller;

namespace RPG_Game.InputHandlers
{
    public abstract class InputHandlerBaseClass: IInputHandler
    {
        private IInputHandler? _nextHandler;
        public IInputHandler SetNext(IInputHandler nextHandler)
        {
            _nextHandler = nextHandler;
            return _nextHandler;
        }
        protected abstract bool Process(ConsoleKeyInfo consoleKey, GameController controller);
        public void HandleInput(ConsoleKeyInfo consoleKey, GameController controller)
        {
            if (!Process(consoleKey, controller))
            {
                _nextHandler?.HandleInput(consoleKey, controller);
            }
        }


    }
}
