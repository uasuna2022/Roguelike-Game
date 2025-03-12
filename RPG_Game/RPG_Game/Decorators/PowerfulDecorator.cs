using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.Decorators
{
    public class PowerfulDecorator: WeaponDecoratorBaseClass, IWeapon
    {
        public PowerfulDecorator(IWeapon weapon): base(weapon) { }
        public override string GetDisplayName()
        {
            return base.GetDisplayName() + " (Powerful)";
        }
        public override int Damage => base.Damage + 5;

    }
}
