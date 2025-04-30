using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_Game.Interfaces;

namespace RPG_Game.Enemies
{
    public class Rat: EnemyBaseClass
    {
        public override string Name => "Rat";
        public override char Symbol => 'R';
        public override int Damage => 0;
        public override int Health { get; set; } = 5;
        public override void AttackPlayer(Player player)
        {
            player.Luck -= 10;
            GameDisplayer.Instance.AddNotification($"I'm a rat! I don't deal damage, but I can make you so unlucky XD. Your luck reduced by 10 points");
        }
        public override int Armor => 3;
    }
}
