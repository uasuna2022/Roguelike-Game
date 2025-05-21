using RPG_Game.Interfaces;
using RPG_Game.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game
{
    public class Cell
    {
        public int X {  get; set; }
        public int Y { get; set; }
        public bool isWall {  get; set; }

        public List<IItem> Items;
        public int maxListSize { get; }
        public IEnemy? Enemy { get; set; }
        public bool ContainsPlayer {  get; set; }
        public Cell (int x, int y)
        {
            X = x;
            Y = y;
            isWall = false;
            Items = new List<IItem>();
            maxListSize = 15;
            ContainsPlayer = false;
            Enemy = null;
        }
        public void AddItem(IItem item)
        {
            if (Items.Count < maxListSize)
            {
                Items.Add(item);
                return;
            }
        }
        public void AddEnemy(IEnemy enemy)
        {
            Enemy = enemy; 
        }
        public void RemoveTopItem()
        {
            if (Items.Count == 0) return;
            //_items.Pop();
            Items.RemoveAt(0);
        }

        public IItem? GetTopItem()
        {
            if (Items.Count == 0) return null;
            //return _items.Peek();
            return Items[0];
        }
        /*
        public void RemoveItemFromCell(IItem item)
        {
            if (!Items.Contains(item))
            {
                GameDisplayer.Instance.AddNotification($"There is no {item.GetDisplayName()} on ({X}, {Y}) cell");
                return;
            }
            Items.Remove(item);
        }
        */
    }
}
