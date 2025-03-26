using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.Items.Potions
{
    public class ElixirPotion: ItemBaseClass, IPotion
    {
        public int Volume { get; private set; }
        public double HealingEffect { get; private set; }
        public ElixirPotion(int volume, double healingEffect) : base("Elixir Potion", 'E')
        {
            Volume = volume;
            HealingEffect = healingEffect;
        }
        public override string GetDisplayName() =>
            $"{Name} (Volume: {Volume}, Healing Effect: {HealingEffect}, Total Heal: {(int)(Volume * HealingEffect)})";
        public void ConsumePotion(Player player)
        {
            player.Health += (int)(HealingEffect * Volume);
            if (player.Health > player.GetMaxHealth) player.Health = player.GetMaxHealth;
            player.Inventory.Remove(this);
        }
        public override void PickUp(Player player, Room room)
        {
            player.AddItemToInventory(this, room);
            Thread.Sleep(1000);
            ConsumePotion(player);
        }
    }
}
