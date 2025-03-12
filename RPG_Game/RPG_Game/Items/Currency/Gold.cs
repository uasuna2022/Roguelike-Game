using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.Items.Currency
{
    public class Gold: ItemBaseClass
    {
        public int Amount { get; private set; }
        public Gold(int amount) : base("Gold", '0')
        {
            Amount = amount;
        }
        public override string GetDisplayName()
        {
            return base.GetDisplayName() + $": ({Amount})";
        }
        public override void PickUp(Player player, Room room)
        {
            player.Gold += Amount;
            room.Grid[player.X, player.Y].RemoveTopItem();
            Console.WriteLine($"Picked up {Amount} of Gold!");
        }
    }
}
