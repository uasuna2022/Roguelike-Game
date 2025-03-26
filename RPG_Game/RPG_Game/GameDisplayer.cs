using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game
{
    public sealed class GameDisplayer
    {
        private static GameDisplayer _instance;
        public static GameDisplayer Instance => (_instance == null) ? (new GameDisplayer()) : _instance;

        private List<string> _notifications = new List<string>();
        private GameDisplayer() { }

        private const int _roomTop = 0;
        private const int _roomLeft = 0;
        private const int _playerStatsTop = 0;
        private const int _playerStatsLeft = 59;
        private const int _notificationsTop = 27;
        private const int _notificationsLeft = 0;
        private const int _instructionsTop = 21;
        private const int _instructionsLeft = 0;

        public void Initialize(Room room, Player player, string instructions)
        {
            Console.Clear();
            DrawRoom(room, player);
            DrawStats(player);
            DrawInstructions(instructions);
            DrawNotifications();
        }

        public void DrawRoom(Room room, Player player)
        {
            for (int i = 0; i < room.Height; i++)
            {
                Console.SetCursorPosition(_roomLeft, _roomTop + i);
                for (int j = 0; j < room.Width; j++)
                {
                    if (i == player.X && j == player.Y)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.OutputEncoding = System.Text.Encoding.UTF8;
                        Console.Write("\u00B6");
                        Console.ResetColor();
                    }
                    else if (room.Grid[i, j].Enemy != null)
                    {
                        Console.ForegroundColor = room.Grid[i, j].Enemy.Color;
                        Console.Write(room.Grid[i, j].Enemy.Symbol);
                        Console.ResetColor();
                    }

                    else if (room.Grid[i, j].isWall == true)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("█");
                        Console.ResetColor();
                    }

                    else
                    {
                        IItem? topItem = room.Grid[i, j].GetTopItem();
                        if (topItem != null)
                        {
                            Console.Write(topItem.Symbol);
                        }
                        else Console.Write(" ");
                    }
                }
                Console.ResetColor();
            }
        }
        public void UpdateMapCells(int oldX, int oldY, int newX, int newY, Room room, Player player)
        {
            RedrawRoomCell(oldX, oldY, room, player);
            RedrawRoomCell(newX, newY, room, player);
        }
        private void RedrawRoomCell(int row, int col, Room room, Player player)
        {
            Console.SetCursorPosition(_roomLeft + col, _roomTop + row);
            if (row == player.X && col == player.Y)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Console.Write("¶");
                Console.ResetColor();
            }

            else if (room.Grid[row, col].Enemy != null)
            {
                Console.ForegroundColor = room.Grid[row, col].Enemy.Color;
                Console.Write(room.Grid[row, col].Enemy.Symbol);
                Console.ResetColor();
            }

            else if (room.Grid[row, col].isWall == true)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("█");
                Console.ResetColor();
            }

            else
            {
                IItem? topItem = room.Grid[row, col].GetTopItem();
                if (topItem != null)
                {
                    Console.Write(topItem.Symbol);
                }
                else Console.Write(" ");
            }
        }
        public void DrawStats(Player player)
        {
            int row = _playerStatsTop;
            Console.SetCursorPosition(_playerStatsLeft, row++);
            Console.WriteLine("=== Player Stats ===");
            Console.SetCursorPosition(_playerStatsLeft, row++);
            Console.WriteLine($"Health: {player.Health} / {player.GetMaxHealth}".PadRight(30));
            Console.SetCursorPosition(_playerStatsLeft, row++);
            Console.WriteLine($"Strength: {player.Strength}".PadRight(30));
            Console.SetCursorPosition(_playerStatsLeft, row++);
            Console.WriteLine($"Dexterity: {player.Dexterity}".PadRight(30));
            Console.SetCursorPosition(_playerStatsLeft, row++);
            Console.WriteLine($"Luck: {player.Luck}".PadRight(30));
            Console.SetCursorPosition(_playerStatsLeft, row++);
            Console.WriteLine($"Aggression: {player.Aggression}".PadRight(30));
            Console.SetCursorPosition(_playerStatsLeft, row++);
            Console.WriteLine($"Wisdom: {player.Wisdom}".PadRight(30));
            Console.SetCursorPosition(_playerStatsLeft, row++);
            Console.WriteLine($"Coins: {player.Coins}  Gold: {player.Gold}".PadRight(30));
            row++;


            Console.SetCursorPosition(_playerStatsLeft, row++);
            Console.WriteLine("=== Equipped Items ===".PadRight(30));

            Console.SetCursorPosition(_playerStatsLeft, row++);
            string leftHandText = "empty";
            if (player.LeftHand != null)
            {
                leftHandText = player.LeftHand.GetDisplayName();
            }
            Console.WriteLine($"Left Hand: {leftHandText}".PadRight(100));

            Console.SetCursorPosition(_playerStatsLeft, row++);
            string rightHandText = "empty";
            if (player.RightHand != null)
            {
                rightHandText = player.RightHand.GetDisplayName();
            }
            Console.WriteLine($"Right Hand: {rightHandText}".PadRight(100));

            int startRow = row;
            for (int i = 0; i < 11; i++)
            {
                Console.SetCursorPosition(_playerStatsLeft, startRow + i);
                Console.WriteLine("".PadRight(50));
            }
            Console.SetCursorPosition(_playerStatsLeft, row++);
            if (player.Inventory.Count == 0)
            {
                Console.WriteLine("Player's inventory: empty!".PadRight(30));
            }
            else
            {
                Console.WriteLine("Player's inventory:".PadRight(30));
                for (int i = 0; i < player.Inventory.Count; i++)
                {
                    Console.SetCursorPosition(_playerStatsLeft, row++);
                    string displayName = player.Inventory[i].GetDisplayName();
                    Console.WriteLine($"{i + 1}) {displayName}".PadRight(30));
                }
            }
        }

        public void AddNotification(string message)
        {
            if (_notifications.Count > 5)
                _notifications.RemoveAt(0);
            _notifications.Add(message);
            DrawNotifications();
        }
        public void DrawNotifications()
        {
            for (int i = 0; i < 7; i++)
            {   
                Console.SetCursorPosition(_notificationsLeft, _notificationsTop + i);
                Console.WriteLine("".PadRight(200));
            }

            int row = _notificationsTop;
            Console.SetCursorPosition(_notificationsLeft, row++);
            Console.WriteLine("=== Notifications ===".PadRight(30));
            foreach (string message in _notifications)
            {
                Console.SetCursorPosition(_notificationsLeft, row++);
                Console.WriteLine(message.PadRight(80));
            }
        }
        public void DrawInstructions(string instructions)
        {
            int row = _instructionsTop;
            int col = _instructionsLeft;

            Console.SetCursorPosition(col, row++);
            Console.WriteLine("=== Instructions ===");

            var lines = instructions.Split('\n');
            foreach (var line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    Console.SetCursorPosition(col, row++);
                    Console.WriteLine(line.Trim());
                }
            }
        }
    }
}
