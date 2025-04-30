using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.InputHandlers
{
    public class FightHandler : InputHandlerBaseClass
    {
        protected override bool Process(ConsoleKeyInfo consoleKey, Game game)
        {
            if (consoleKey.Key != InputKeyConfiguration.Fight)
                return false;
            else
            {
                GameDisplayer.Instance.AddNotification("lirililarila");
                return true;
            }
        }
    }
}
