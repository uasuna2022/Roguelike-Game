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
        public abstract void AttackPlayer(Player player); 
    }
}
