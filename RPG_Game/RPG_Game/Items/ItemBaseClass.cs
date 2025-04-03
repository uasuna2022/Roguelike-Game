using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.Items
{
    public abstract class ItemBaseClass: IItem
    {
        public string Name { get; }
        public char Symbol { get; }
        public bool IsEquippable { get; }
        public virtual string GetDisplayName() => Name;
        protected ItemBaseClass(string name, char symbol, bool isEquippable) 
        {
            Name = name;
            Symbol = symbol;
            IsEquippable = isEquippable;
        }
        public virtual void EquipPlayer(Player player) { } // do nothing 
        public virtual void UnequipPlayer(Player player) { } // do nothing
        public virtual void PickUp(Player player, Room room) //
        {
            player.AddItemToInventory(this, room);
        }
    }
}
