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

            PlaceWalls();
            PlaceItems();
        }
        public void PlaceWalls() // hardcoded at this stage
        {
            for (int i = 1; i < Height; i += 3)
            {
                for (int j = 0; j < Width - 5; j++)
                {
                    Grid[i, j].isWall = true;
                }
            }
        }
        public Cell GetCell(int x, int y)
        {
            return Grid[x, y];
        }
        public void PlaceItems() // hardcoded at this stage
        {
            Grid[0, 10].AddItem(new Coin(15));
            Grid[6, 2].AddItem(new _2HandedSword());
            Grid[11, 18].AddItem(new AggresiveDecorator(new Gun()));
            Grid[17, 4].AddItem(new Book("This is a very cool book! Read it immediately :)", "Frankenstein"));
            Grid[9, 5].AddItem(new Gold(25));
            Grid[3, 25].AddItem(new Coin(10));
            Grid[3, 30].AddItem(new UnluckyDecorator(new PowerfulDecorator(new VerbalAbuse())));
            Grid[0, 1].AddItem(new Gun());
            Grid[0, 2].AddItem(new Gun());
            Grid[0, 3].AddItem(new Gun());
            Grid[0, 4].AddItem(new Gun());
            Grid[0, 5].AddItem(new Gun());
            Grid[0, 6].AddItem(new Gun());
            Grid[0, 7].AddItem(new Gun());
            Grid[0, 8].AddItem(new Gun());
            Grid[0, 9].AddItem(new Gun());
            Grid[0, 10].AddItem(new Gun());
            Grid[0, 11].AddItem(new Gun());
        }
        public void DrawRoom(Player player)
        {                   
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (i == player.X && j == player.Y)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.OutputEncoding = System.Text.Encoding.UTF8;
                        Console.Write("\u00B6");
                        Console.ResetColor();
                    }

                    else if (Grid[i, j].isWall == true)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("█");
                        Console.ResetColor();
                    }

                    else
                    {
                        IItem? topItem = Grid[i, j].GetTopItem();
                        if (topItem != null)
                        {
                            Console.Write(topItem.Symbol);
                        }
                        else Console.Write(" ");
                    }
                }

                Console.WriteLine();
            }
                
            
        }
    }
}
