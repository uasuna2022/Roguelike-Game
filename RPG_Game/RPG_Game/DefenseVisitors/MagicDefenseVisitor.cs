using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.DefenseVisitors
{
    public class MagicDefenseVisitor : DefenseVisitorBaseClass
    {
        public MagicDefenseVisitor(IItem weapon, Player player) : base(weapon, player) { }

        public override int VisitHeavyWeapon(IHeavyWeapon heavyWeapon)
        {
            return player.Luck;
        }
        public override int VisitLightWeapon(ILightWeapon lightWeapon)
        {
            return player.Luck;
        }
        public override int VisitMagicWeapon(IMagicWeapon magicWeapon)
        {
            return 2 * player.Wisdom;
        }
        public override int VisitNonWeapon(IItem nonWeapon)
        {
            return player.Luck;
        }
    }
}
