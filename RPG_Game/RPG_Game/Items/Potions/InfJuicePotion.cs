using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_Game.Interfaces;
using RPG_Game.PotionEffects;

namespace RPG_Game.Items.Potions
{
    public class InfJuicePotion: ItemBaseClass, IPotion
    {
        public InfJuicePotion() : base("Juice", 'J', false, true) { }
        public PotionEffectBaseClass CreatePotionEffect(Player player)
        {
            return new JuiceInfiniteEffect(player, -1, 50);
        }
        public void ConsumePotion(Player player)
        {
            player.Inventory.Remove(this);
            PotionEffectBaseClass juiceInfiniteEffect = CreatePotionEffect(player);
            player.Attach(juiceInfiniteEffect);
            juiceInfiniteEffect.AfterApply();
            player.activeEffects.Add(juiceInfiniteEffect);
            GameDisplayer.Instance.DrawActivePotionEffects(player);
        }
    }
}
