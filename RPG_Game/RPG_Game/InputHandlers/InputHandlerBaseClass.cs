using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        protected abstract bool Process(ConsoleKeyInfo consoleKey, Game game);
        public void HandleInput(ConsoleKeyInfo consoleKey, Game game)
        {
            if (!Process(consoleKey, game))
            {
                _nextHandler?.HandleInput(consoleKey, game);
            }
        }


    }
}
