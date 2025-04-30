using RPG_Game.Interfaces;
using RPG_Game.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.Decorators
{
    public abstract class WeaponDecoratorBaseClass: IWeapon, IHeavyWeapon, ILightWeapon, IMagicWeapon
    {
        protected IWeapon wrappedWeapon;
        protected WeaponDecoratorBaseClass(IWeapon weapon)
        {
            wrappedWeapon = weapon;
        }
        public virtual string Name => wrappedWeapon.Name;
        public virtual char Symbol => wrappedWeapon.Symbol;
        public virtual ConsoleColor ConsoleColor => wrappedWeapon.ConsoleColor;
        public virtual int Damage => wrappedWeapon.Damage;
        public virtual bool IsTwoHanded => wrappedWeapon.IsTwoHanded;
        public virtual bool IsEquippable => wrappedWeapon.IsEquippable;
        public virtual bool IsDrinkable => wrappedWeapon.IsDrinkable;

        public virtual string GetDisplayName() => wrappedWeapon.GetDisplayName();
        public virtual void EquipPlayer(Player player) => wrappedWeapon.EquipPlayer(player);
        public virtual void UnequipPlayer(Player player) => wrappedWeapon.UnequipPlayer(player);
        public virtual void PickUp(Player player, Room room)
        {
            player.AddItemToInventory(this, room);
        }
        public virtual int Accept(IAttackVisitor attackVisitor)
        {
            return ((dynamic)wrappedWeapon).Accept(attackVisitor);
        }
        public virtual int AcceptDefense(IDefenseVisitor defenseVisitor)
        {
            return ((dynamic)wrappedWeapon).AcceptDefense(defenseVisitor);
        }
    }
}
