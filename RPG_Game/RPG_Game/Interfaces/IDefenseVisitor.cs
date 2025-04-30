using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.Interfaces
{
    public interface IDefenseVisitor
    {
        int VisitHeavyWeapon(IHeavyWeapon heavyWeapon);
        int VisitLightWeapon(ILightWeapon lightWeapon);
        int VisitMagicWeapon(IMagicWeapon magicWeapon);
        int VisitNonWeapon(IItem nonWeapon); // the same story as with IAttackVisitor
    }
}
