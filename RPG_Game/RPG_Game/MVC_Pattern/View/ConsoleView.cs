using RPG_Game.Interfaces;
using RPG_Game.MVC_Pattern.Model;
using RPG_Game.PotionEffects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        // Diff buffering caches to prevent redundant Win32 Console API calls & eliminate input lag
        private string[,] _lastRoomSymbol = new string[100, 100];
        private ConsoleColor[,] _lastRoomColor = new ConsoleColor[100, 100];
        private Dictionary<(int left, int top), string> _lastWrittenText = new();
        private Dictionary<(int left, int top), ConsoleColor?> _lastWrittenColor = new();

        private void ResetCaches()
        {
            _lastWrittenText.Clear();
            _lastWrittenColor.Clear();
            Array.Clear(_lastRoomSymbol, 0, _lastRoomSymbol.Length);
            Array.Clear(_lastRoomColor, 0, _lastRoomColor.Length);
        }

        private void SafeWriteAt(int left, int top, string text, ConsoleColor? color = null)
        {
            try
            {
                if (left >= 0 && top >= 0 && left + text.Length < Console.BufferWidth && top < Console.BufferHeight)
                {
                    var key = (left, top);
                    if (_lastWrittenText.TryGetValue(key, out string? oldText) &&
                        _lastWrittenColor.TryGetValue(key, out ConsoleColor? oldColor) &&
                        oldText == text && oldColor == color)
                    {
                        return; // Cell already contains exact text & color — skip Win32 call!
                    }

                    _lastWrittenText[key] = text;
                    _lastWrittenColor[key] = color;

                    Console.SetCursorPosition(left, top);
                    if (color.HasValue) Console.ForegroundColor = color.Value;
                    Console.Write(text);
                    if (color.HasValue) Console.ResetColor();
                }
            }
            catch
            {
                // Silently skip out-of-bounds draws
            }
        }

        private void SafeWriteRoomCell(int row, int col, string symbol, ConsoleColor color)
        {
            if (row < 0 || row >= 100 || col < 0 || col >= 100) return;

            if (_lastRoomSymbol[row, col] == symbol && _lastRoomColor[row, col] == color)
            {
                return; // Tile hasn't changed since last frame — skip Win32 call!
            }

            _lastRoomSymbol[row, col] = symbol;
            _lastRoomColor[row, col] = color;

            SafeWriteCell(_roomLeft + col, _roomTop + row, symbol, color);
        }

        private void SafeWriteCell(int left, int top, object symbol, ConsoleColor color)
        {
            try
            {
                if (left >= 0 && top >= 0 && left < Console.BufferWidth && top < Console.BufferHeight)
                {
                    Console.SetCursorPosition(left, top);
                    Console.ForegroundColor = color;
                    Console.Write(symbol);
                    Console.ResetColor();
                }
            }
            catch
            {
                // Silently skip out-of-bounds cell draws
            }
        }

        public void Initialize(GameState gameState, int localPlayerIndex, string instructions)
        {
            _gameState = gameState;
            _localPlayerIdx = localPlayerIndex;

            if (_gameState != null)
            {
                _gameState.StateChanged += StateChangedHandler;
                _gameState.NotificationAdded += NotificationHandler;
            }

            ResetCaches();
            try { Console.Clear(); } catch { }
            DrawRoom();
            DrawPlayerStats();
            DrawInstructions(instructions);
            InitializeNotificationsPanel();
            DrawGameStats();
            DrawCellStats();
            DrawActivePotionEffects();
            DrawNearbyEnemies();
        }

        public void InitializeNotificationsPanel()
        {
            SafeWriteAt(_notificationsLeft, _notificationsTop, "=== Notifications ===".PadRight(30), ConsoleColor.Yellow);
        }

        public void DrawRoom()
        {
            if (_gameState == null) return;
            Room room = _gameState.Room;

            for (int i = 0; i < room.Height; i++)
            {
                for (int j = 0; j < room.Width; j++)
                {
                    bool anyOther = false;
                    for (int playerIdx = 0; playerIdx < _gameState.Players.Count; playerIdx++)
                    {
                        Player pl = _gameState.Players[playerIdx];
                        if (pl.X == i && pl.Y == j)
                        {
                            ConsoleColor color = (playerIdx == _localPlayerIdx) ? ConsoleColor.DarkYellow : ConsoleColor.DarkCyan;
                            SafeWriteRoomCell(i, j, $"{playerIdx + 1}", color);
                            anyOther = true;
                            break;
                        }
                    }

                    if (anyOther) continue;

                    if (room.Grid[i, j].Enemy != null)
                    {
                        SafeWriteRoomCell(i, j, room.Grid[i, j].Enemy!.Symbol.ToString(), room.Grid[i, j].Enemy!.Color);
                    }
                    else if (room.Grid[i, j].isWall == true)
                    {
                        SafeWriteRoomCell(i, j, "█", ConsoleColor.DarkGray);
                    }
                    else
                    {
                        IItem? topItem = room.Grid[i, j].GetTopItem();
                        if (topItem != null)
                        {
                            SafeWriteRoomCell(i, j, topItem.Symbol.ToString(), topItem.ConsoleColor);
                        }
                        else
                        {
                            SafeWriteRoomCell(i, j, " ", ConsoleColor.Gray);
                        }
                    }
                }
            }
        }

        public void UpdateMapCells(int oldX, int oldY, int newX, int newY, Room room, Player player)
        {
            RedrawRoomCell(oldX, oldY, room, player);
            RedrawRoomCell(newX, newY, room, player);
        }

        private void RedrawRoomCell(int row, int col, Room room, Player player)
        {
            if (row == player.X && col == player.Y)
            {
                SafeWriteRoomCell(row, col, "¶", ConsoleColor.DarkYellow);
            }
            else if (room.Grid[row, col].Enemy != null)
            {
                SafeWriteRoomCell(row, col, room.Grid[row, col].Enemy!.Symbol.ToString(), room.Grid[row, col].Enemy!.Color);
            }
            else if (room.Grid[row, col].isWall == true)
            {
                SafeWriteRoomCell(row, col, "█", ConsoleColor.DarkGray);
            }
            else
            {
                IItem? topItem = room.Grid[row, col].GetTopItem();
                if (topItem != null)
                {
                    SafeWriteRoomCell(row, col, topItem.Symbol.ToString(), topItem.ConsoleColor);
                }
                else
                {
                    SafeWriteRoomCell(row, col, " ", ConsoleColor.Gray);
                }
            }
        }

        public void DrawPlayerStats()
        {
            if (_gameState == null) return;
            Player player = _gameState.Players[_localPlayerIdx];

            int row = _playerStatsTop;
            SafeWriteAt(_playerStatsLeft, row++, "=== Player Stats ===", ConsoleColor.Yellow);
            SafeWriteAt(_playerStatsLeft, row++, $"Health: {player.Health} / {player.GetMaxHealth}".PadRight(25));
            SafeWriteAt(_playerStatsLeft, row++, $"Strength: {player.Strength}".PadRight(25));
            SafeWriteAt(_playerStatsLeft, row++, $"Dexterity: {player.Dexterity}".PadRight(25));
            SafeWriteAt(_playerStatsLeft, row++, $"Luck: {player.Luck}".PadRight(25));
            SafeWriteAt(_playerStatsLeft, row++, $"Aggression: {player.Aggression}".PadRight(25));
            SafeWriteAt(_playerStatsLeft, row++, $"Wisdom: {player.Wisdom}".PadRight(25));
            SafeWriteAt(_playerStatsLeft, row++, $"Coins: {player.Coins}  Gold: {player.Gold}".PadRight(25));
            row++;

            SafeWriteAt(_playerStatsLeft, row++, "=== Equipped Items ===".PadRight(57), ConsoleColor.Yellow);

            string leftHandText = player.LeftHand != null ? player.LeftHand.GetDisplayName() : "empty";
            SafeWriteAt(_playerStatsLeft, row++, $"Left Hand: {leftHandText}".PadRight(57));

            string rightHandText = player.RightHand != null ? player.RightHand.GetDisplayName() : "empty";
            SafeWriteAt(_playerStatsLeft, row++, $"Right Hand: {rightHandText}".PadRight(57));

            int startRow = row;
            for (int i = 0; i < 13; i++)
            {
                SafeWriteAt(_playerStatsLeft, startRow + i, "".PadRight(57));
            }
            row += 2;
            if (player.Inventory.Count == 0)
            {
                SafeWriteAt(_playerStatsLeft, row, "=== Player's inventory ===".PadRight(57), ConsoleColor.Yellow);
            }
            else
            {
                SafeWriteAt(_playerStatsLeft, row++, "=== Player's inventory ===".PadRight(57), ConsoleColor.Yellow);
                for (int i = 0; i < player.Inventory.Count; i++)
                {
                    string displayName = player.Inventory[i].GetDisplayName();
                    SafeWriteAt(_playerStatsLeft, row++, $"{i + 1}) {displayName}".PadRight(57));
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

        public void ClearNotifications()
        {
            _notifications.Clear();
        }

        public void DrawNotifications()
        {
            for (int i = 1; i < 10; i++)
            {
                SafeWriteAt(_notificationsLeft, _notificationsTop + i, "".PadRight(99));
            }

            int row = _notificationsTop;
            SafeWriteAt(_notificationsLeft, row++, "=== Notifications ===".PadRight(30), ConsoleColor.Yellow);
            foreach (string message in _notifications)
            {
                SafeWriteAt(_notificationsLeft, row++, message.PadRight(99));
            }
        }

        public void DrawInstructions(string instructions)
        {
            int row = _instructionsTop;
            int col = _instructionsLeft;

            SafeWriteAt(col, row++, "=== Instructions ===", ConsoleColor.Yellow);

            var lines = instructions.Split('\n');
            foreach (var line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    SafeWriteAt(col, row++, line.Trim());
                }
            }
        }

        public void DrawGameStats()
        {
            if (_gameState == null) return;
            Player player = _gameState.Players[_localPlayerIdx];

            int row = _gameStatsTop;
            SafeWriteAt(_gameStatsLeft, row++, "=== Game Stats ===".PadRight(30), ConsoleColor.Yellow);
            SafeWriteAt(_gameStatsLeft, row++, $"Current Tile: ({player.X}, {player.Y})".PadRight(30));
            SafeWriteAt(_gameStatsLeft, row++, $"Step Counter: {_gameState.StepCounter}");
        }

        public void DrawCellStats()
        {
            if (_gameState == null) return;
            Cell cell = _gameState.Room.GetCell(_gameState.Players[_localPlayerIdx].X, _gameState.Players[_localPlayerIdx].Y);

            int row = _cellStatsTop;
            SafeWriteAt(_cellStatsLeft, row++, "=== Current Cell Stats ===".PadRight(56), ConsoleColor.Yellow);
            int startRow = row;
            for (int i = 0; i < 15; i++)
            {
                SafeWriteAt(_cellStatsLeft, startRow + i, "".PadRight(56));
            }
            if (cell.Items.Count > 0)
            {
                for (int i = 0; i < cell.Items.Count; i++)
                {
                    string displayName = cell.Items[i].GetDisplayName();
                    SafeWriteAt(_cellStatsLeft, row++, $"{i + 1}) {displayName}".PadRight(56));
                }
            }
        }

        public void DrawActivePotionEffects()
        {
            if (_gameState == null) return;
            Player player = _gameState.Players[_localPlayerIdx];

            int row = _activePotionsTop;
            SafeWriteAt(_activePotionsLeft, row++, "=== Active Potion Effects ===".PadRight(56), ConsoleColor.Yellow);

            int startRow = row;
            for (int i = 0; i < 10; i++)
            {
                SafeWriteAt(_activePotionsLeft, startRow + i, "".PadRight(56));
            }

            List<PotionEffectBaseClass> activeEffects = player.activeEffects;
            if (activeEffects.Count > 0)
            {
                for (int i = 0; i < activeEffects.Count; i++)
                {
                    SafeWriteAt(_activePotionsLeft, row++, $"{i + 1}) {activeEffects[i].ToString()}".PadRight(56));
                }
            }
        }

        public void DrawNearbyEnemies()
        {
            if (_gameState == null) return;
            Player player = _gameState.Players[_localPlayerIdx];

            IEnemy? up = player.nearbyEnemies[EnumClasses.Direction.Up];
            IEnemy? down = player.nearbyEnemies[EnumClasses.Direction.Down];
            IEnemy? left = player.nearbyEnemies[EnumClasses.Direction.Left];
            IEnemy? right = player.nearbyEnemies[EnumClasses.Direction.Right];

            int row = _enemyInfoTop;
            SafeWriteAt(_enemyInfoLeft, row++, "=== Nearby enemies ===", ConsoleColor.Yellow);

            if (up != null)
                SafeWriteAt(_enemyInfoLeft, row++, $"Up: {up.Name}  {up.Health}/{up.MaxHealth} HP".PadRight(26));
            else
                SafeWriteAt(_enemyInfoLeft, row++, "Up: nobody".PadRight(26));

            if (down != null)
                SafeWriteAt(_enemyInfoLeft, row++, $"Down: {down.Name}  {down.Health}/{down.MaxHealth} HP".PadRight(26));
            else
                SafeWriteAt(_enemyInfoLeft, row++, "Down: nobody".PadRight(26));

            if (left != null)
                SafeWriteAt(_enemyInfoLeft, row++, $"Left: {left.Name}  {left.Health}/{left.MaxHealth} HP".PadRight(26));
            else
                SafeWriteAt(_enemyInfoLeft, row++, "Left: nobody".PadRight(26));

            if (right != null)
                SafeWriteAt(_enemyInfoLeft, row++, $"Right: {right.Name}  {right.Health}/{right.MaxHealth} HP".PadRight(26));
            else
                SafeWriteAt(_enemyInfoLeft, row++, "Right: nobody".PadRight(26));
        }

        private void NotificationHandler(string message)
        {
            AddNotification(message);
        }

        private void StateChangedHandler(object? sender, EventArgs eventArgs)
        {
            DrawRoom();
            DrawPlayerStats();
            DrawGameStats();
            DrawCellStats();
            DrawActivePotionEffects();
            DrawNearbyEnemies();
        }
    }
}
