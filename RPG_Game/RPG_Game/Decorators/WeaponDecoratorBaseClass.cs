using RPG_Game.Interfaces;
using RPG_Game.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.Decorators
{
    public abstract class WeaponDecoratorBaseClass: WeaponBaseClass, IWeapon
    {
        protected IWeapon wrappedWeapon;
        protected WeaponDecoratorBaseClass(IWeapon weapon): base(weapon.Name, weapon.Symbol, weapon.Damage, weapon.IsTwoHanded)
        {
            wrappedWeapon = weapon;
        }
        public override string GetDisplayName() => wrappedWeapon.GetDisplayName();
        public override void EquipPlayer(Player player) => wrappedWeapon.EquipPlayer(player);
        public override void UnequipPlayer(Player player) => wrappedWeapon.UnequipPlayer(player);
        public override int Damage => wrappedWeapon.Damage;

    }
}
