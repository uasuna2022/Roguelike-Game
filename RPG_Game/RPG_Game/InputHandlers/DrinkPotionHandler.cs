using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.InputHandlers
{
    public class DrinkPotionHandler: InputHandlerBaseClass
    {
        protected override bool Process(ConsoleKeyInfo consoleKey, Game game)
        {
            if (consoleKey.Key != InputKeyConfiguration.DrinkPotion)
                return false;

            GameDisplayer.Instance.AddNotification("tralalelo tralala");
            return true;
        }
    }
}
