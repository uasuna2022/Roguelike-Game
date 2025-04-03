using RPG_Game.Builders;
using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace RPG_Game
{
    public class Game
    {
        public Player player;
        public Room room = new Room();
        private string _instructions;
        private bool _gameIsRunning;
        private readonly GameDisplayer _gameDisplayer = GameDisplayer.Instance;
        
        public Game(int version)
        {
            player = new Player();
            _gameIsRunning = true;
            CreateDungeon(version);
            _instructions = "";
        }
        public void CreateDungeon(int version)
        {
            Director director = new Director();
            CompositeBuilder compositeBuilder = new CompositeBuilder();
            
            switch (version)
            {
                case 1:                  
                    director.BuildBasicDungeonWithWalls(compositeBuilder);
                    break;
                case 2:
                    director.BuildFullDungeonWithWalls(compositeBuilder);
                    break;
                case 3:
                    director.BuildDungeonWithoutWalls(compositeBuilder);
                    break;
                default:
                    Console.WriteLine("You have to enter 1, 2 or 3 to start a game!");
                    break;
            }

            room = ((DungeonBuilder)(compositeBuilder._builders[0])).GetFinalResult();
            _instructions = ((InstructionBuilder)(compositeBuilder._builders[1])).GetFinalResult();
        }
        public void StartGame()
        {
            Console.WriteLine("Hi! Glad to see you here again! Maximize your console window and tap any key to start a new game...");
            Console.ReadKey(true);
            _gameDisplayer.Initialize(room, player, _instructions);
            while (_gameIsRunning)
            {
                ProcessInput();
                _gameDisplayer.DrawStats(player);   
            }
        }
        public void ProcessInput()
        {
            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
            char inputSymbol = consoleKeyInfo.KeyChar;

            switch (char.ToUpper(inputSymbol))
            {
                case 'W':
                case 'A':
                case 'S':
                case 'D':
                    int oldX = player.X;
                    int oldY = player.Y;
                    player.Move(inputSymbol, room);
                    _gameDisplayer.UpdateMapCells(oldX, oldY, player.X, player.Y, room, player);
                    break;
                case 'E':
                    player.PickUpItem(room);
                    break;
                case 'Q':
                    _gameIsRunning = false;
                    _gameDisplayer.AddNotification("Exiting game...");
                    Console.ReadKey();
                    break;
                case 'I':
                    bool containsEquipableItems = false;
                    foreach (IItem item in player.Inventory)
                    {
                        if (item.IsEquippable)
                        {
                            containsEquipableItems = true;
                            break;
                        }
                    }
                    if (player.Inventory.Count == 0)
                    {
                        _gameDisplayer.AddNotification($"Your inventory is empty & you can't equip any item!");
                        break;
                    }
                    else if (!containsEquipableItems)
                    {
                        _gameDisplayer.AddNotification($"All the items in your inventory are unequippable!");
                        break;
                    }
                    else
                    {
                        _gameDisplayer.AddNotification($"Which item would you like to equip? Choose a number from 1 to 0 (10)");
                        int index = (char)Console.ReadKey(true).KeyChar - 48;
                        if (index < 0 || index > 9)
                        {
                            _gameDisplayer.AddNotification($"Invalid number! Choose a digit (0-9), not a letter or any other character");
                            break;
                        }
                        if (index == 0) index += 10;
                        if (index > player.Inventory.Count)
                        {
                            _gameDisplayer.AddNotification($"You don't have an item with this number in your inventory!");
                            break;
                        }
                        if (!player.Inventory[index - 1].IsEquippable)
                        {
                            _gameDisplayer.AddNotification($"You can't equip {player.Inventory[index - 1].GetDisplayName()}! " +
                                $"It's an unequippable item!");
                            break;
                        }
                        IWeapon chosenWeapon = (IWeapon)player.Inventory[index - 1];
                        player.EquipWeapon(chosenWeapon);
                        _gameDisplayer.DrawStats(player);
                        break;
                    }
                case 'O':
                    _gameDisplayer.AddNotification("Which hand would you like to unequip?");
                    char handChar = Console.ReadKey(true).KeyChar;
                    switch (char.ToUpper(handChar))
                    {
                        case 'L':
                            player.UnequipWeapon(true, room);
                            _gameDisplayer.DrawStats(player);
                            break;
                        case 'R':
                            player.UnequipWeapon(false, room);
                            _gameDisplayer.DrawStats(player);
                            break;
                        default:
                            _gameDisplayer.AddNotification("Invalid choice. Press 'L' or 'R'.");
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
