using RPG_Game.Decorators;
using RPG_Game.Interfaces;
using RPG_Game.Items.Currency;
using RPG_Game.Items.UnusableItems;
using RPG_Game.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game
{
    public class Room
    {
        public int Width = 40;
        public int Height = 20;
        public Cell[,] Grid;

        public Room()
        {
            Grid = new Cell[Height, Width];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Grid[i, j] = new Cell(i, j);
                }
            }
        }
        public Cell GetCell(int x, int y)
        {
            return Grid[x, y];
        }
    }
}
