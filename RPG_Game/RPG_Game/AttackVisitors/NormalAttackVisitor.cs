using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.AttackVisitors
{
    public class NormalAttackVisitor : AttackVisitorBaseClass
    {
        public NormalAttackVisitor(IItem weapon, Player player) : base(weapon, player) { }
        public override int VisitHeavyWeapon(IHeavyWeapon heavyWeapon)
        {
            return heavyWeapon.Damage + player.Strength + player.Aggression;
        }
        public override int VisitLightWeapon(ILightWeapon lightWeapon)
        {
            return lightWeapon.Damage + player.Dexterity + player.Luck;
        }
        public override int VisitMagicWeapon(IMagicWeapon magicWeapon)
        {
            return 1;
        }
        public override int VisitNonWeapon(IItem nonWeapon)
        {
            return 0;
        }
    }
}
