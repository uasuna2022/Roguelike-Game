using RPG_Game.Interfaces;
using RPG_Game.PotionEffects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.Items.Potions
{
    public class PowerPotion: ItemBaseClass, IPotion
    {
        public PowerPotion() : base("Power Potion", 'P', false, true) { }
        public PotionEffectBaseClass CreatePotionEffect(Player player)
        {
            return new PowerPotionEffect(player, 5, 2);
        }
        public void ConsumePotion(Player player)
        {
            player.Inventory.Remove(this);
            PotionEffectBaseClass powerPotionEffect = CreatePotionEffect(player);
            player.Attach(powerPotionEffect);
            powerPotionEffect.AfterApply();
            player.activeEffects.Add(powerPotionEffect);
            GameDisplayer.Instance.DrawActivePotionEffects(player);
        }
        
    }
}
