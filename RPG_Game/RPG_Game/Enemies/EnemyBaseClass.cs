using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_Game.Interfaces;

namespace RPG_Game.Enemies
{
    public abstract class EnemyBaseClass: IEnemy
    {
        public abstract string Name { get; }
        public abstract char Symbol { get; }
        public virtual ConsoleColor Color => ConsoleColor.Red;
        public abstract int Health { get; set; }
        public abstract int Damage { get; }
        public abstract int Armor { get; }

        public int X { get; set; }
        public int Y { get; set; }
        public IStrategy CurrentStrategy { get; set; }
        protected EnemyBaseClass(int x, int y, IStrategy strategy)
        {
            X = x;
            Y = y;
            CurrentStrategy = strategy;
        } 
    }
}
