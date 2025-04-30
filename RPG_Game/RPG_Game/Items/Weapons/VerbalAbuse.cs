using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.Items.Weapons
{
    public class VerbalAbuse: WeaponBaseClass, IMagicWeapon
    {
        public VerbalAbuse() : base("Verbal Abuse", 'V', 7, false) { }
        public override int Accept(IAttackVisitor attackVisitor)
        {
            return attackVisitor.VisitMagicWeapon(this);
        }
        public override int AcceptDefense(IDefenseVisitor defenseVisitor)
        {
            return defenseVisitor.VisitMagicWeapon(this);
        }
    }
}
