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

        private Stack<IItem> _items;
        public IEnemy? Enemy { get; set; }
        public bool ContainsPlayer {  get; set; }
        public Cell (int x, int y)
        {
            X = x;
            Y = y;
            isWall = false;
            _items = new Stack<IItem>();
            ContainsPlayer = false;
            Enemy = null;
        }
        public void AddItem(IItem item)
        {
            _items.Push(item);
        }
        public void AddEnemy(IEnemy enemy)
        {
            Enemy = enemy; 
        }
        public void RemoveTopItem()
        {
            if (_items.Count == 0) return;
            _items.Pop();
        }

        public IItem? GetTopItem()
        {
            if (_items.Count == 0) return null;
            return _items.Peek();
        }

    }
}
