using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RPG_Game
{
    public class Game
    {

        public Room room;
        public Player player;
        private bool _gameIsRunning;
        
        public Game()
        {
            room = new Room();
            player = new Player();
            _gameIsRunning = true;
        }

        public void StartGame()
        {
            Console.WriteLine("Hi! Glad to see you here again! Tap any key to start a new game...");
            Console.ReadKey(true);
            while (_gameIsRunning)
            {             
                UpdateGameState();
                ProcessInput();
            }
        }
        public void UpdateGameState()
        {
            // Console.Clear();
            room.DrawRoom(player);
            Console.WriteLine();
            Console.WriteLine("Player Stats:");
            Console.WriteLine($" Strength: {player.Strength}  Dexterity: {player.Dexterity}  Health: {player.Health}");
            Console.WriteLine($" Luck: {player.Luck}   Aggression:  {player.Aggression}  Wisdom: {player.Wisdom}");
            Console.WriteLine($" Coins: {player.Coins}  Gold: {player.Gold}");
            Console.WriteLine();
            player.PrintEquippedItems();
            Console.WriteLine();
            player.PrintInventory();
            Console.WriteLine();
            Console.WriteLine("Controls: WASD to move, E to pick up item, Q to quit, I to equip, O to unequip.");
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
                    player.Move(inputSymbol, room);
                    break;
                case 'E':
                    player.PickUpItem(room);
                    break;
                case 'Q':
                    _gameIsRunning = false;
                    Console.WriteLine("Exiting game...");
                    Console.ReadKey();
                    break;
                case 'I':
                    if (player.Inventory.Count == 0)
                    {
                        Console.WriteLine($"Your inventory is empty & you can't equip any item!");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Which item would you like to equip? Choose a number from 1 to 0 (10)");
                        int index = (char)Console.ReadKey(true).KeyChar - 48;
                        if (index == 0) index += 10;
                        IWeapon chosenWeapon = (IWeapon)player.Inventory[index - 1];
                        player.EquipWeapon(chosenWeapon);
                        break;
                    }
                case 'O':
                    Console.WriteLine("Which hand would you like to unequip?");
                    char handChar = Console.ReadKey(true).KeyChar;
                    switch (char.ToUpper(handChar))
                    {
                        case 'L':
                            player.UnequipWeapon(true, room);
                            break;
                        case 'R':
                            player.UnequipWeapon(false, room);
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Press 'L' or 'R'.");
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
