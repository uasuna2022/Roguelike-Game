using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game
{
    public static class InputKeyConfiguration
    {
        public static ConsoleKey MoveUp { get; set; } = ConsoleKey.W;
        public static ConsoleKey MoveDown { get; set; } = ConsoleKey.S;
        public static ConsoleKey MoveLeft { get; set; } = ConsoleKey.A;
        public static ConsoleKey MoveRight { get; set; } = ConsoleKey.D;
        public static ConsoleKey PickItem { get; set; } = ConsoleKey.E;
        public static ConsoleKey DropItem { get; set; } = ConsoleKey.X;
        public static ConsoleKey DrinkPotion { get; set; } = ConsoleKey.P;
        public static ConsoleKey Equip { get; set; } = ConsoleKey.I;
        public static ConsoleKey Unequip { get; set; } = ConsoleKey.O;
        public static ConsoleKey Quit { get; set; } = ConsoleKey.Q;
    }
}
