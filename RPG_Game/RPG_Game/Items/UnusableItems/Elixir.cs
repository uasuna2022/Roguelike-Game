using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.Items.UnusableItems
{
    public class Elixir: ItemBaseClass
    {
        public int Volume;
        public string Description;
        public Elixir(string description, int volume): base("Elixir", 'E')
        {
            Description = description;
            Volume = volume;
        }
    }
}
