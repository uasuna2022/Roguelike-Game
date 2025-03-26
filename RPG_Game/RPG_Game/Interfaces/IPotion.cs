using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.Interfaces
{
    public interface IPotion: IItem
    {
        int Volume { get; }
        double HealingEffect { get; }
        void ConsumePotion(Player player);
    }
}
