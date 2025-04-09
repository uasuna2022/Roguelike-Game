using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.Items.Weapons
{
    public abstract class WeaponBaseClass : ItemBaseClass, IWeapon
    {
        public virtual int Damage { get; }
        public bool IsTwoHanded { get; }
        protected WeaponBaseClass(string name, char symbol, int damage, bool isTwoHanded = false) : 
            base(name, symbol, true, false, ConsoleColor.Cyan)
        {
            Damage = damage;
            IsTwoHanded = isTwoHanded;
        }
        // dodać PickUpWeapon() {}
    }
}
