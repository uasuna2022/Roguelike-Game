using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_Game.Enemies;
using RPG_Game.EnumClasses;
using RPG_Game.Interfaces;
using RPG_Game.Items.Currency;

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
        public bool newIsValidMove(Direction? direction, Room room)
        {
            int newX = X;
            int newY = Y;
            switch (direction)
            {
                case Direction.Up:
                    newX--;
                    break;
                case Direction.Down:
                    newX++;
                    break;
                case Direction.Left:
                    newY--;
                    break;
                case Direction.Right:
                    newY++;
                    break;
                default:
                    break;
            }
            if (newX < 0 || newY < 0 || newX >= room.Height || newY >= room.Width) return false;
            Cell possibleCell = room.Grid[newX, newY];
            if (possibleCell.isWall == true || possibleCell.Enemy != null) return false;
            else return true;
        }
        public void newMove(Direction? direction, Room room)
        {
            int newX = X;
            int newY = Y;
            switch (direction)
            {
                case Direction.Up:
                    newX--;
                    break;
                case Direction.Down:
                    newX++;
                    break;
                case Direction.Left:
                    newY--;
                    break;
                case Direction.Right:
                    newY++;
                    break;
                default:
                    break;
            }

            X = newX; 
            Y = newY;
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
            Cell currentCell = room.Grid[X, Y];
            IItem? pickedItem = currentCell.GetTopItem();

            if (pickedItem != null)
            {
                pickedItem.PickUp(this, room);
            }

            else GameDisplayer.Instance.AddNotification("The cell is currently empty!");
        }
        public void DropItemFromHand (IItem item, Room room)
        {
            Cell currentCell = room.Grid[X, Y];
            currentCell.AddItem(item);
            GameDisplayer.Instance.AddNotification($"Dropped: {item.GetDisplayName()}");
            GameDisplayer.Instance.DrawCellStats(room.GetCell(X, Y));
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
                        GameDisplayer.Instance.AddNotification($"Left hand is unequipped. {LeftHand.GetDisplayName()} moved back to inventory!");

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
                        GameDisplayer.Instance.AddNotification($"Right hand is unequipped. {RightHand.GetDisplayName()} moved back to inventory!");
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
        public void DropItemFromInventory(IItem item, Room room)
        {
            if (!Inventory.Contains(item))
            {
                GameDisplayer.Instance.AddNotification($"There is no {item.GetDisplayName()} in your inventory!");
                return;
            }
            Inventory.Remove(item);
            room.Grid[X, Y].AddItem(item);
            GameDisplayer.Instance.AddNotification($"Item {item.GetDisplayName()} dropped on the tile ({X}, {Y})");
        }
        public void newDropItemFromHand(Hand hand, Room room)
        {
            if (hand == Hand.Left)
            {
                if (LeftHand == null)
                {
                    GameDisplayer.Instance.AddNotification("Left hand is empty! Equip it first to be able to unequip it!");
                    return;
                }
                else if (RightHand != null && LeftHand.IsTwoHanded && RightHand.IsTwoHanded)
                {
                    GameDisplayer.Instance.AddNotification("Both of your hands are equipped with one two-handed weapon!");
                    RightHand = null;

                }

                room.Grid[X, Y].AddItem(LeftHand);
                LeftHand.UnequipPlayer(this);
                GameDisplayer.Instance.AddNotification($"Item {LeftHand.GetDisplayName()} dropped on the tile ({X}, {Y})");
                LeftHand = null;
                return;
            }
            if (hand == Hand.Right)
            {
                if (RightHand == null)
                {
                    GameDisplayer.Instance.AddNotification("Right hand is empty! Equip it first to be able to unequip it!");
                    return;
                }
                else if (LeftHand != null && LeftHand.IsTwoHanded && RightHand.IsTwoHanded)
                {
                    GameDisplayer.Instance.AddNotification("Both of your hands are equipped with one two-handed weapon!");
                    LeftHand = null;
                }

                room.Grid[X, Y].AddItem(RightHand);
                RightHand.UnequipPlayer(this);
                GameDisplayer.Instance.AddNotification($"Item {RightHand.GetDisplayName()} dropped on the tile ({X}, {Y})");
                RightHand = null;
                return;
            }
        }
    }
}
