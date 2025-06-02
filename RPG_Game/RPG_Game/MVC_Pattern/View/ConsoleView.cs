using RPG_Game.Interfaces;
using RPG_Game.MVC_Pattern.Model;
using RPG_Game.PotionEffects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
// 46px * 156px
namespace RPG_Game.MVC_Pattern.View
{
    public sealed class ConsoleView
    {
        private static ConsoleView? _instance;
        public static ConsoleView Instance => _instance ??= new ConsoleView();

        private List<string> _notifications = new List<string>();
        private GameState? _gameState;
        private int _localPlayerIdx;
        private ConsoleView() { }

        private const int _roomTop = 0;
        private const int _roomLeft = 0;
        private const int _playerStatsTop = 0;
        private const int _playerStatsLeft = 99;
        private const int _notificationsTop = 32;
        private const int _notificationsLeft = 0;
        private const int _instructionsTop = 21;
        private const int _instructionsLeft = 0;

        private const int _gameStatsTop = 0;
        private const int _gameStatsLeft = 43;
        private const int _cellStatsTop = 4;
        private const int _cellStatsLeft = 43;

        private const int _activePotionsLeft = 99;
        private const int _activePotionsTop = 26;

        private const int _enemyInfoTop = 0;
        private const int _enemyInfoLeft = 130;

        public void Initialize(GameState gameState, int localPlayerIndex, string instructions)
        {
            _gameState = gameState;
            _localPlayerIdx = localPlayerIndex;

            if (_gameState != null)
            {
                _gameState.StateChanged += StateChangedHandler;
                _gameState.NotificationAdded += NotificationHandler;
            }

            Console.Clear();
            DrawRoom();
            DrawPlayerStats();
            DrawInstructions(instructions);
            InitializeNotificationsPanel();
            DrawGameStats();
            DrawCellStats();
            DrawActivePotionEffects();
            DrawNearbyEnemies();
        }  // changed
        public void InitializeNotificationsPanel()
        {
            Console.SetCursorPosition(_notificationsLeft, _notificationsTop);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=== Notifications ===".PadRight(30));
            Console.ResetColor();
        } // added
        public void DrawRoom()  // changed 
        {
            if (_gameState == null) return;

            Room room = _gameState.Room;

            for (int i = 0; i < room.Height; i++)
            {
                Console.SetCursorPosition(_roomLeft, _roomTop + i);
                for (int j = 0; j < room.Width; j++)
                {
                    bool anyOther = false;
                    for (int playerIdx = 0; playerIdx < _gameState.Players.Count; playerIdx++)
                    {
                        Player pl = _gameState.Players[playerIdx];
                        if (pl.X == i && pl.Y == j)
                        {
                            Console.ForegroundColor = (playerIdx == _localPlayerIdx) ? ConsoleColor.DarkYellow : ConsoleColor.DarkCyan;
                            Console.Write($"{playerIdx + 1}"); 
                            Console.ResetColor();
                            anyOther = true;
                            break;
                        }
                    }

                    if (anyOther) continue;

                    if (room.Grid[i, j].Enemy != null)
                    {
                        Console.ForegroundColor = room!.Grid[i, j].Enemy!.Color;
                        Console.Write(room!.Grid[i, j].Enemy!.Symbol);
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
                            Console.ForegroundColor = topItem.ConsoleColor;
                            Console.Write(topItem.Symbol);
                            Console.ResetColor();
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
        } // TODO
        private void RedrawRoomCell(int row, int col, Room room, Player player)
        {
            Console.SetCursorPosition(_roomLeft + col, _roomTop + row);
            if (row == player.X && col == player.Y)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.OutputEncoding = Encoding.UTF8;
                Console.Write("¶");
                Console.ResetColor();
            }

            else if (room.Grid[row, col].Enemy != null)
            {
                Console.ForegroundColor = room!.Grid[row, col].Enemy!.Color;
                Console.Write(room!.Grid[row, col].Enemy!.Symbol);
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
                    Console.ForegroundColor = topItem.ConsoleColor;
                    Console.Write(topItem.Symbol);
                    Console.ResetColor();
                }
                else Console.Write(" ");
            }
        } // TODO
        public void DrawPlayerStats() // changed
        {
            if (_gameState == null) return;
            Player player = _gameState.Players[_localPlayerIdx];

            Console.ForegroundColor = ConsoleColor.Yellow;
            int row = _playerStatsTop;
            Console.SetCursorPosition(_playerStatsLeft, row++);
            Console.WriteLine("=== Player Stats ===");
            Console.ResetColor();
            Console.SetCursorPosition(_playerStatsLeft, row++);
            Console.WriteLine($"Health: {player.Health} / {player.GetMaxHealth}".PadRight(25));
            Console.SetCursorPosition(_playerStatsLeft, row++);
            Console.WriteLine($"Strength: {player.Strength}".PadRight(25));
            Console.SetCursorPosition(_playerStatsLeft, row++);
            Console.WriteLine($"Dexterity: {player.Dexterity}".PadRight(25));
            Console.SetCursorPosition(_playerStatsLeft, row++);
            Console.WriteLine($"Luck: {player.Luck}".PadRight(25));
            Console.SetCursorPosition(_playerStatsLeft, row++);
            Console.WriteLine($"Aggression: {player.Aggression}".PadRight(25));
            Console.SetCursorPosition(_playerStatsLeft, row++);
            Console.WriteLine($"Wisdom: {player.Wisdom}".PadRight(25));
            Console.SetCursorPosition(_playerStatsLeft, row++);
            Console.WriteLine($"Coins: {player.Coins}  Gold: {player.Gold}".PadRight(25));
            row++;

            Console.SetCursorPosition(_playerStatsLeft, row++);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=== Equipped Items ===".PadRight(57));
            Console.ResetColor();

            Console.SetCursorPosition(_playerStatsLeft, row++);
            string leftHandText = "empty";
            if (player.LeftHand != null)
            {
                leftHandText = player.LeftHand.GetDisplayName();
            }
            Console.WriteLine($"Left Hand: {leftHandText}".PadRight(57));

            Console.SetCursorPosition(_playerStatsLeft, row++);
            string rightHandText = "empty";
            if (player.RightHand != null)
            {
                rightHandText = player.RightHand.GetDisplayName();
            }
            Console.WriteLine($"Right Hand: {rightHandText}".PadRight(57));

            int startRow = row;
            for (int i = 0; i < 13; i++)
            {
                Console.SetCursorPosition(_playerStatsLeft, startRow + i);
                Console.WriteLine("".PadRight(57));
            }
            Console.SetCursorPosition(_playerStatsLeft, row += 2);
            if (player.Inventory.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("=== Player's inventory ===".PadRight(57));
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("=== Player's inventory ===".PadRight(57));
                Console.ResetColor();
                Console.SetCursorPosition(_playerStatsLeft, row++);
                for (int i = 0; i < player.Inventory.Count; i++)
                {
                    Console.SetCursorPosition(_playerStatsLeft, row++);
                    string displayName = player.Inventory[i].GetDisplayName();
                    Console.WriteLine($"{i + 1}) {displayName}".PadRight(57));
                }
            }
        }
        public void AddNotification(string message)
        {
            if (_notifications.Count > 5)
                _notifications.RemoveAt(0);
            _notifications.Add(message);
            DrawNotifications();
        } // no changes
        public void ClearNotifications()
        {
            _notifications.Clear();
        } // no changes
        public void DrawNotifications()
        {
            for (int i = 1; i < 10; i++)
            {
                Console.SetCursorPosition(_notificationsLeft, _notificationsTop + i);
                Console.WriteLine("".PadRight(99));
            }

            int row = _notificationsTop;
            Console.SetCursorPosition(_notificationsLeft, row++);
            foreach (string message in _notifications)
            {
                Console.SetCursorPosition(_notificationsLeft, row++);
                Console.WriteLine(message.PadRight(99));
            }
        } // no changes
        public void DrawInstructions(string instructions)
        {
            int row = _instructionsTop;
            int col = _instructionsLeft;

            Console.SetCursorPosition(col, row++);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=== Instructions ===");
            Console.ResetColor();

            var lines = instructions.Split('\n');
            foreach (var line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    Console.SetCursorPosition(col, row++);
                    Console.WriteLine(line.Trim());
                }
            }
        } // no changes
        public void DrawGameStats()
        {
            if (_gameState == null) return;
            Player player = _gameState.Players[_localPlayerIdx];

            Console.ForegroundColor = ConsoleColor.Yellow;
            int row = _gameStatsTop;
            Console.SetCursorPosition(_gameStatsLeft, row++);
            Console.WriteLine("=== Game Stats ===".PadRight(30));
            Console.ResetColor();
            Console.SetCursorPosition(_gameStatsLeft, row++);
            Console.WriteLine($"Current Tile: ({player.X}, {player.Y})".PadRight(30));
            Console.SetCursorPosition(_gameStatsLeft, row++);
            Console.WriteLine($"Step Counter: {_gameState.StepCounter}");
            //_stepCount++;
        } // changed
        public void DrawCellStats()
        {
            if (_gameState == null) return;
            Cell cell = _gameState.Room.GetCell(_gameState.Players[_localPlayerIdx].X, _gameState.Players[_localPlayerIdx].Y);

            Console.ForegroundColor = ConsoleColor.Yellow;
            int row = _cellStatsTop;
            Console.SetCursorPosition(_cellStatsLeft, row++);
            Console.WriteLine("=== Current Cell Stats ===".PadRight(56));
            Console.ResetColor();
            int startRow = row;
            for (int i = 0; i < 15; i++)
            {
                Console.SetCursorPosition(_cellStatsLeft, startRow + i);
                Console.WriteLine("".PadRight(56));
            }
            if (cell.Items.Count > 0)
            {
                for (int i = 0; i < cell.Items.Count; i++)
                {
                    Console.SetCursorPosition(_cellStatsLeft, row++);
                    string displayName = cell.Items[i].GetDisplayName();
                    Console.WriteLine($"{i + 1}) {displayName}".PadRight(56));
                }
            }
        }  // changed
        public void DrawActivePotionEffects()
        {
            if (_gameState == null) return;
            Player player = _gameState.Players[_localPlayerIdx];

            Console.ForegroundColor = ConsoleColor.Yellow;
            int row = _activePotionsTop;
            Console.SetCursorPosition(_activePotionsLeft, row++);
            Console.WriteLine("=== Active Potion Effects ===".PadRight(56));
            Console.ResetColor();

            int startRow = row;
            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(_activePotionsLeft, startRow + i);
                Console.WriteLine("".PadRight(56));
            }

            List<PotionEffectBaseClass> activeEffects = player.activeEffects;
            if (activeEffects.Count > 0)
            {
                for (int i = 0; i < activeEffects.Count; i++)
                {
                    Console.SetCursorPosition(_activePotionsLeft, row++);
                    Console.WriteLine($"{i + 1}) {activeEffects[i].ToString()}".PadRight(56));
                }
            }
        } // changed
        public void DrawNearbyEnemies()
        {
            if (_gameState == null) return;
            Player player = _gameState.Players[_localPlayerIdx];

            IEnemy? up = player.nearbyEnemies[EnumClasses.Direction.Up];
            IEnemy? down = player.nearbyEnemies[EnumClasses.Direction.Down];
            IEnemy? left = player.nearbyEnemies[EnumClasses.Direction.Left];
            IEnemy? right = player.nearbyEnemies[EnumClasses.Direction.Right];
            Console.ForegroundColor = ConsoleColor.Yellow;
            int row = _enemyInfoTop;
            Console.SetCursorPosition(_enemyInfoLeft, row++);
            Console.WriteLine("=== Nearby enemies ===");
            Console.ResetColor();
            Console.SetCursorPosition(_enemyInfoLeft, row++);
            if (up != null)
            {
                Console.WriteLine($"Up: {up.Name}  {up.Health}/{up.MaxHealth} HP".PadRight(26));
            }
            else Console.WriteLine($"Up: nobody".PadRight(26));
            Console.SetCursorPosition(_enemyInfoLeft, row++);
            if (down != null)
            {
                Console.WriteLine($"Down: {down.Name}  {down.Health}/{down.MaxHealth} HP".PadRight(26));
            }
            else Console.WriteLine($"Down: nobody".PadRight(26));
            Console.SetCursorPosition(_enemyInfoLeft, row++);
            if (left != null)
            {
                Console.WriteLine($"Left: {left.Name}  {left.Health}/{left.MaxHealth} HP".PadRight(26));
            }
            else Console.WriteLine("Left: nobody".PadRight(26));
            Console.SetCursorPosition(_enemyInfoLeft, row++);
            if (right != null)
            {
                Console.WriteLine($"Right: {right.Name}  {right.Health}/{right.MaxHealth} HP".PadRight(26));
            }
            else Console.WriteLine("Right: nobody".PadRight(26));
        } // changed
        private void NotificationHandler(string message)
        {
            AddNotification(message);
        } // event handler method added
        private void StateChangedHandler(object? sender, EventArgs eventArgs)
        {
            DrawRoom();
            DrawPlayerStats();
            DrawGameStats();
            DrawCellStats();
            DrawActivePotionEffects();
            DrawNearbyEnemies();
        } // event handler method added
    }
}
