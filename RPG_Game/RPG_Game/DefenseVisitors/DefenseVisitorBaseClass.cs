using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.DefenseVisitors
{
    public abstract class DefenseVisitorBaseClass : IDefenseVisitor
    {
        protected IItem weapon;
        protected Player player;

        protected DefenseVisitorBaseClass(IItem weapon, Player player)
        {
            this.weapon = weapon;
            this.player = player;
        }

        public abstract int VisitHeavyWeapon(IHeavyWeapon heavyWeapon);
        public abstract int VisitLightWeapon(ILightWeapon lightWeapon);
        public abstract int VisitMagicWeapon(IMagicWeapon magicWeapon);
        public abstract int VisitNonWeapon(IItem nonWeapon);
    }
}
