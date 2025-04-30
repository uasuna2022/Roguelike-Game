using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.DefenseVisitors
{
    public class NormalDefenseVisitor : DefenseVisitorBaseClass
    {
        public NormalDefenseVisitor(IItem weapon, Player player) : base(weapon, player) { }

        public override int VisitHeavyWeapon(IHeavyWeapon heavyWeapon)
        {
            return player.Luck + player.Strength;
        }
        public override int VisitLightWeapon(ILightWeapon lightWeapon)
        {
            return player.Luck + player.Dexterity;
        }
        public override int VisitMagicWeapon(IMagicWeapon magicWeapon)
        {
            return player.Luck + player.Dexterity;
        }
        public override int VisitNonWeapon(IItem nonWeapon)
        {
            return player.Dexterity;
        }
    }
}
