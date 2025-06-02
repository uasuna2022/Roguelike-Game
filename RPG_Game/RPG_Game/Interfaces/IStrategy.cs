using RPG_Game.MVC_Pattern.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.Interfaces
{
    public interface IStrategy
    {
        void React(IEnemy reactingEnemy, GameState gameState);
    }
}
