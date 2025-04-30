using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.Items.Weapons
{
    public class _2HandedSword: WeaponBaseClass, IHeavyWeapon
    {
        public _2HandedSword() : base("2HandedSword", 'S', 25, true) { }
        public override int Accept(IAttackVisitor attackVisitor)
        {
            return attackVisitor.VisitHeavyWeapon(this);
        }
        public override int AcceptDefense(IDefenseVisitor defenseVisitor)
        {
            return defenseVisitor.VisitHeavyWeapon(this);
        }
    }
}
