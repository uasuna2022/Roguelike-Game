using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.Items.Currency
{
    public class Coin: ItemBaseClass
    {
        public int Amount { get; private set; }
        public Coin(int amount) : base("Coin", '$') 
        {
            Amount = amount;
        }
        public override string GetDisplayName()
        {
            return base.GetDisplayName() + $": ({Amount})";
        }

        public override void PickUp(Player player, Room room)
        {
            player.Coins += Amount;
            room.Grid[player.X, player.Y].RemoveTopItem();
            Console.WriteLine($"Picked up {Amount} of coins!");
        }
    }
}
