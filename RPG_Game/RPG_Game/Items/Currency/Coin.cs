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
        public Coin(int amount) : base("Coin", '$', false, false, ConsoleColor.Yellow) 
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
            //GameDisplayer.Instance.AddNotification($"Picked up {Amount} of coins!");
            player.Notify($"Picked up {Amount} of coins!");
            player.Refresh();
        }
    }
}
