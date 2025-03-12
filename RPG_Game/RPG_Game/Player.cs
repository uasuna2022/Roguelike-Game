using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_Game.Interfaces;

namespace RPG_Game
{
    public class Player
    {
        public int X {  get; set; }
        public int Y { get; set; }
        public int Strength { get; set; }
        public int Dexterity {  get; set; }
        public int Health { get; set; }
        public int Luck { get; set; }
        public int Aggression { get; set; }
        public int Wisdom {  get; set; }
        public List<IItem> Inventory {  get; set; }
        private int _maxInventorySize { get; }
        public IWeapon? LeftHand { get; private set; }
        public IWeapon? RightHand { get; private set; }
        public int Coins { get; set; }
        public int Gold { get; set; }

        public Player()
        {
            X = 0;
            Y = 0;
            Strength = 20;
            Dexterity = 20;
            Health = 100;
            Luck = 0;
            Aggression = 0;
            Wisdom = 0;
            Inventory = new List<IItem>();
            _maxInventorySize = 10;
            LeftHand = null;
            RightHand = null;
            Coins = 0;
            Gold = 0;
        }

        private bool isValidMove(int newX, int newY, Room room)
        {
            if (newX < 0 || newY < 0 || newX >= room.Height || newY >= room.Width) return false;
            Cell possibleCell = room.GetCell(newX, newY);
            if (possibleCell.isWall == true) return false;
            else return true;
        }
        public void Move (char direction, Room room)
        {
            int newX = X;
            int newY = Y;
            switch (char.ToUpper(direction))
            {
                case 'W':
                    newX--;
                    break;
                case 'A':
                    newY--;
                    break;
                case 'S':
                    newX++;
                    break;
                case 'D':
                    newY++;
                    break;
                default:
                    return;
            }

            if (isValidMove(newX, newY, room))
            {
                X = newX;
                Y = newY;
            }

            return;
        }
        public void AddItemToInventory(IItem item, Room room)
        {
            if (Inventory.Count < _maxInventorySize)
            {
                Inventory.Add(item);
                room.Grid[X, Y].RemoveTopItem();
                Console.WriteLine($"Picked up an item: {item.GetDisplayName()}");
            }

            else
            {
                Console.WriteLine($"The inventory is full! ({_maxInventorySize} out of {_maxInventorySize})");
            }
        }
        public void PickUpItem (Room room)
        {
            Cell currentCell = room.GetCell(X, Y);
            IItem? pickedItem = currentCell.GetTopItem();

            if (pickedItem != null)
            {
                pickedItem.PickUp(this, room);
            }

            else Console.WriteLine("The cell is currently empty!");
        }
        public void DropItem (IItem item, Room room)
        {
            if (!Inventory.Contains(item))
            {
                Console.WriteLine($"You don't have this item in your inventory!");
                return;
            }

            else
            {
                Inventory.Remove(item);
                Cell currentCell = room.GetCell(X, Y);
                currentCell.AddItem(item);
                Console.WriteLine($"Dropped: {item.GetDisplayName()}");
            }
        }

        public void EquipWeapon(IWeapon weapon)
        {
            if (!Inventory.Contains(weapon))
            {
                Console.WriteLine($"You don't have {weapon.GetDisplayName()} item in your inventory!");
                return;
            }

            if (LeftHand != null && RightHand != null)
            {
                Console.WriteLine($"Both of your arms are equipped... Unequip one of them to be able to reequip it.");
                return;
            }

            if (!weapon.IsTwoHanded)
            {
                if (LeftHand == null)
                {
                    Inventory.Remove(weapon);
                    LeftHand = weapon;
                    weapon.EquipPlayer(this);
                    Console.WriteLine($"Left hand equipped with an item: {weapon.GetDisplayName()}");
                    return;
                }

                else
                {
                    Inventory.Remove(weapon);
                    RightHand = weapon;
                    weapon.EquipPlayer(this);
                    Console.WriteLine($"Right hand equipped with an item: {weapon.GetDisplayName()}");
                    return;
                }
            }

            if (weapon.IsTwoHanded)
            {
                if (LeftHand != null || RightHand != null)
                {
                    Console.WriteLine($"One of your arms is already equipped. You can't equip a two-handed weapon then.");
                    return;
                }

                else
                {
                    Inventory.Remove(weapon);
                    RightHand = weapon;
                    LeftHand = weapon;
                    weapon.EquipPlayer(this);
                    Console.WriteLine($"Both of your arms equipped with an item: {weapon.GetDisplayName()}");
                    return;
                }
            }
        }

        public void UnequipWeapon(bool isLeftHand, Room room)
        {
            if (LeftHand != null && RightHand != null && LeftHand.IsTwoHanded && RightHand.IsTwoHanded) // unequipping two-handed weapon
            {
                LeftHand.UnequipPlayer(this);
                if (Inventory.Count < _maxInventorySize)
                {
                    Inventory.Add(LeftHand);
                    Console.WriteLine($"Two-handed weapon {LeftHand.GetDisplayName()} moved back to inventory!");
                }

                else
                {
                    DropItem(LeftHand, room);
                    Console.WriteLine($"Your inventory is full! The {LeftHand.GetDisplayName()} dropped on the cell ({this.X}, {this.Y})!");
                }
                
                LeftHand = null;
                RightHand = null;
                return;
            }

            if (isLeftHand)
            {
                if (LeftHand == null)
                {
                    Console.WriteLine($"You can't unequip left hand - it's free");
                    return;
                }

                else
                {
                    if (Inventory.Count < _maxInventorySize)
                    {
                        Inventory.Add(LeftHand);
                        Console.WriteLine($"Left hand is uneqipped. {LeftHand.GetDisplayName()} moved back to inventory!");

                    }
                    else
                    {
                        DropItem(LeftHand, room);
                        Console.WriteLine($"Your inventory is full! The {LeftHand.GetDisplayName()} dropped on the cell ({this.X}, {this.Y})!");
                    }
                    LeftHand.UnequipPlayer(this);
                    LeftHand = null;
                    return;
                }
            }

            else
            {
                if (RightHand == null)
                {
                    Console.WriteLine($"You can't unequip right hand - it's free");
                    return;
                }

                else
                {
                    if (Inventory.Count < _maxInventorySize)
                    {
                        Inventory.Add(RightHand);
                        Console.WriteLine($"Right hand is uneqipped. {RightHand} moved back to inventory!");

                    }
                    else
                    {
                        DropItem(RightHand, room);
                        Console.WriteLine($"Your inventory is full! The {RightHand.GetDisplayName()} dropped on the cell ({this.X}, {this.Y})!");
                    }
                    RightHand.UnequipPlayer(this);
                    RightHand = null;
                    return;
                }
            }
        }       
        public void PrintInventory()
        {
            Console.WriteLine("Player's inventory:");

            if (Inventory.Count == 0)
            {
                Console.Write(" empty!");
                return;
            }

            int i = 1;
            foreach (var item in Inventory)
            {
                Console.WriteLine($"{i}) {item.GetDisplayName()}");
                i++;
            }
            return;
        }
        public void PrintEquippedItems()
        {
            Console.WriteLine("Equipped items:");
            if (LeftHand == null)
            {
                Console.WriteLine("Left Hand: empty");
            }
            else
            {
                Console.WriteLine($"Left Hand: {LeftHand.GetDisplayName()}");
            }

            if (RightHand == null)
            {
                Console.WriteLine("Right Hand: empty");
            }
            else
            {
                Console.WriteLine($"Right Hand: {RightHand.GetDisplayName()}");
            }

            return;
        }
    }
}
