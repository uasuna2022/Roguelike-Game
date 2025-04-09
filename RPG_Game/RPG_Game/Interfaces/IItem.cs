using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.Interfaces
{
    public interface IItem
    {
        string Name { get; }
        char Symbol { get; }
        ConsoleColor ConsoleColor { get; }
        string GetDisplayName();
        void EquipPlayer (Player player);
        void UnequipPlayer (Player player);
        void PickUp (Player player, Room room);  
        bool IsEquippable { get; }
        bool IsDrinkable { get; }
    }
}
