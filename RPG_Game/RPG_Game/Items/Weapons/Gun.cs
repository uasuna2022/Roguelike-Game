using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.Items.Weapons
{
    public class Gun: WeaponBaseClass, ILightWeapon
    {
        public Gun() : base("Gun", 'G', 15, false) { }
    }
}
