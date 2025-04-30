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
    }
}
