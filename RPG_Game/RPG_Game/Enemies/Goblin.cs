using RPG_Game.Interfaces;
using RPG_Game.MVC_Pattern.Model;
using RPG_Game.Strategies;
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
        public override int MaxHealth => 20;
        public override int Armor => 15;

        public Goblin(int x, int y) : base(x, y, new OffensiveStrategy()) { }
    }
}
