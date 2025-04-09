using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.Items.UnusableItems
{
    public class Book: ItemBaseClass
    {
        public string Description;
        public Book(string description, string title): base(title, 'B', false, false)
        {
            Description = description;
        }
    }
}
