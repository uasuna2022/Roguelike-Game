using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.Enemies
{
    public class Goblin: EnemyBaseClass
    {
        public override string Name => "Goblin";
        public override char Symbol => 'G';
        public override int Damage => 10;
        public override int Health { get; set; } = 20;
        public override int Armor => 15;
    }
}
