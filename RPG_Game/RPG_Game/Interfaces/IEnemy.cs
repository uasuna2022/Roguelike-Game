using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.Interfaces
{
    public interface IEnemy
    {
        string Name { get; }
        char Symbol { get; }
        ConsoleColor Color { get; }
        int Damage { get; }
        int Health { get; set; }
        int Armor { get; }

        int X { get; set; }
        int Y { get; set; }
        IStrategy CurrentStrategy { get; set; }
    }
}
