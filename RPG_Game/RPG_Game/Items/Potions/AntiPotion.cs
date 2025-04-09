using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_Game.Interfaces;
using RPG_Game.PotionEffects;

namespace RPG_Game.Items.Potions
{
    public class AntiPotion: ItemBaseClass, IPotion
    {
        public AntiPotion() : base("Antidot", 'A', false, true, ConsoleColor.Magenta) { }
        public void ConsumePotion(Player player)
        {
            player.Inventory.Remove(this);
            PotionEffectBaseClass antiPotionEffect = CreatePotionEffect(player);
            player.Attach(antiPotionEffect);
            antiPotionEffect.AfterApply();
            GameDisplayer.Instance.DrawActivePotionEffects(player);
        }
        public PotionEffectBaseClass CreatePotionEffect(Player player)
        {
            return new AntiPotionEffect(player, -1);
        }
    }
}
