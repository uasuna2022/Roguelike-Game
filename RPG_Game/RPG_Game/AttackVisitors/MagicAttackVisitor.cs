using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.AttackVisitors
{
    public class MagicAttackVisitor : AttackVisitorBaseClass
    {
        public MagicAttackVisitor(IItem weapon, Player player): base(weapon, player) { }
        public override int VisitHeavyWeapon(IHeavyWeapon heavyWeapon)
        {
            return 1;
        }
        public override int VisitLightWeapon(ILightWeapon lightWeapon)
        {
            return 1;
        }
        public override int VisitMagicWeapon(IMagicWeapon magicWeapon)
        {
            return magicWeapon.Damage + player.Wisdom;
        }
        public override int VisitNonWeapon(IItem nonWeapon)
        {
            return 0;
        }
    }
}
