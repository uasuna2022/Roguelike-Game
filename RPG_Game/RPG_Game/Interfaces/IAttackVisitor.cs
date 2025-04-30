using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.Interfaces
{
    public interface IAttackVisitor
    {
        int VisitHeavyWeapon(IHeavyWeapon heavyWeapon);
        int VisitLightWeapon(ILightWeapon lightWeapon);
        int VisitMagicWeapon(IMagicWeapon magicWeapon);
        int VisitNonWeapon(IItem nonWeapon); // in my current implementation you can't equip anything except IWeapon,
                                             // so in fact this method is unnecessary, but I'll add it, because in the future
                                             // it's possible, that a player would be able to equip anything else
    }
}
