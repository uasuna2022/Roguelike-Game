using RPG_Game.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.Enemies
{
    public class Dragon: EnemyBaseClass
    {
        public override string Name => "Dragon";
        public override char Symbol => 'D';
        public override int Damage => 50;
        public override int Health { get; set; } = 100;
        public override int Armor => 30;

        public Dragon(int x, int y) : base(x, y, new CalmStrategy()) { }
    }
}
