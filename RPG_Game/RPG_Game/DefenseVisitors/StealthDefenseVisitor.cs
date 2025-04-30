using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.DefenseVisitors
{
    public class StealthDefenseVisitor : DefenseVisitorBaseClass
    {
        public StealthDefenseVisitor(IItem weapon, Player player) : base(weapon, player) { }

        public override int VisitHeavyWeapon(IHeavyWeapon heavyWeapon)
        {
            return player.Strength;
        }
        public override int VisitLightWeapon(ILightWeapon lightWeapon)
        {
            return player.Dexterity;
        }
        public override int VisitMagicWeapon(IMagicWeapon magicWeapon)
        {
            return 0;
        }
        public override int VisitNonWeapon(IItem nonWeapon)
        {
            return 0;
        }
    }
}
