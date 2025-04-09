using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.Items.UnusableItems
{
    public class Ring: ItemBaseClass
    {
        public Ring(string description) : base("Ring", 'R', false, false) 
        {
            Description = description;
        }

        public string Description;
    }
}
