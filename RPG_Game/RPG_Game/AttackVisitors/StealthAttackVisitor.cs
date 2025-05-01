using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.AttackVisitors
{
    public class StealthAttackVisitor : AttackVisitorBaseClass
    {
        public StealthAttackVisitor(IItem weapon, Player player): base(weapon, player) { }
        public override int VisitHeavyWeapon(IHeavyWeapon heavyWeapon)
        {
            return (((IWeapon)weapon).Damage + player.Strength + player.Aggression) / 2;
        }
        public override int VisitLightWeapon(ILightWeapon lightWeapon)
        {
            return (((IWeapon)weapon).Damage + player.Dexterity + player.Luck) * 2;
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
