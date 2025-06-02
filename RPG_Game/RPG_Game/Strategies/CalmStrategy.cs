using RPG_Game.Interfaces;
using RPG_Game.MVC_Pattern.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.Strategies
{
    public class CalmStrategy : IStrategy
    {
        public void React(IEnemy reactingEnemy, GameState gameState) { }  // does absolutely nothing 
    }
}
