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
        private int _maxHealth { get; }
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
            Health = 30;
            _maxHealth = 100;
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

        public int GetMaxHealth => _maxHealth;

        private bool isValidMove(int newX, int newY, Room room)
        {
            if (newX < 0 || newY < 0 || newX >= room.Height || newY >= room.Width) return false;
            Cell possibleCell = room.GetCell(newX, newY);
            if (possibleCell.isWall == true || possibleCell.Enemy != null) return false;
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
                GameDisplayer.Instance.AddNotification($"Picked up an item: {item.GetDisplayName()}");
            }

            else
            {
                GameDisplayer.Instance.AddNotification($"The inventory is full! ({_maxInventorySize} out of {_maxInventorySize})");
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

            else GameDisplayer.Instance.AddNotification("The cell is currently empty!");
        }
        public void DropItemFromHand (IItem item, Room room)
        {
            Cell currentCell = room.GetCell(X, Y);
            currentCell.AddItem(item);
            GameDisplayer.Instance.AddNotification($"Dropped: {item.GetDisplayName()}");
            /*
            if (!Inventory.Contains(item))
            {
                GameDisplayer.Instance.AddNotification($"You don't have this item in your inventory!");
                return;
            }

            else
            {
                Inventory.Remove(item);
                Cell currentCell = room.GetCell(X, Y);
                currentCell.AddItem(item);
                GameDisplayer.Instance.AddNotification($"Dropped: {item.GetDisplayName()}");
            }
            */
        }

        public void EquipWeapon(IWeapon weapon)
        {
            if (!Inventory.Contains(weapon))
            {
                GameDisplayer.Instance.AddNotification($"You don't have {weapon.GetDisplayName()} item in your inventory!");
                return;
            }

            if (LeftHand != null && RightHand != null)
            {
                GameDisplayer.Instance.AddNotification($"Both of your arms are equipped... Unequip one of them to be able to reequip it.");
                return;
            }

            if (!weapon.IsTwoHanded)
            {
                if (LeftHand == null)
                {
                    Inventory.Remove(weapon);
                    LeftHand = weapon;
                    weapon.EquipPlayer(this);
                    GameDisplayer.Instance.AddNotification($"Left hand equipped with an item: {weapon.GetDisplayName()}");
                    return;
                }

                else
                {
                    Inventory.Remove(weapon);
                    RightHand = weapon;
                    weapon.EquipPlayer(this);
                    GameDisplayer.Instance.AddNotification($"Right hand equipped with an item: {weapon.GetDisplayName()}");
                    return;
                }
            }

            if (weapon.IsTwoHanded)
            {
                if (LeftHand != null || RightHand != null)
                {
                    GameDisplayer.Instance.AddNotification($"One of your arms is already equipped. You can't equip a two-handed weapon then.");
                    return;
                }

                else
                {
                    Inventory.Remove(weapon);
                    RightHand = weapon;
                    LeftHand = weapon;
                    weapon.EquipPlayer(this);
                    GameDisplayer.Instance.AddNotification($"Both of your arms equipped with an item: {weapon.GetDisplayName()}");
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
                    GameDisplayer.Instance.AddNotification($"Two-handed weapon {LeftHand.GetDisplayName()} moved back to inventory!");
                }

                else
                {
                    this.DropItemFromHand(LeftHand, room);
                    GameDisplayer.Instance.AddNotification($"Your inventory is full! The {LeftHand.GetDisplayName()} dropped on the cell ({this.X}, {this.Y})!");
                }
                
                LeftHand = null;
                RightHand = null;
                return;
            }

            if (isLeftHand)
            {
                if (LeftHand == null)
                {
                    GameDisplayer.Instance.AddNotification($"You can't unequip left hand - it's free");
                    return;
                }

                else
                {
                    if (Inventory.Count < _maxInventorySize)
                    {
                        Inventory.Add(LeftHand);
                        GameDisplayer.Instance.AddNotification($"Left hand is uneqipped. {LeftHand.GetDisplayName()} moved back to inventory!");

                    }
                    else
                    {
                        this.DropItemFromHand(LeftHand, room);
                        GameDisplayer.Instance.AddNotification($"Your inventory is full! The {LeftHand.GetDisplayName()} dropped on the cell ({this.X}, {this.Y})!");
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
                    GameDisplayer.Instance.AddNotification($"You can't unequip right hand - it's free");
                    return;
                }

                else
                {
                    if (Inventory.Count < _maxInventorySize)
                    {
                        Inventory.Add(RightHand);
                        GameDisplayer.Instance.AddNotification($"Right hand is uneqipped. {RightHand} moved back to inventory!");

                    }
                    else
                    {
                        this.DropItemFromHand(RightHand, room);
                        GameDisplayer.Instance.AddNotification($"Your inventory is full! The {RightHand.GetDisplayName()} dropped on the cell ({this.X}, {this.Y})!");
                    }
                    RightHand.UnequipPlayer(this);
                    RightHand = null;
                    return;
                }
            }
        }       
    }
}
