using RPG_Game.Interfaces;
using RPG_Game.PotionEffects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.Items.Potions
{
    public class LuckPotion: ItemBaseClass, IPotion 
    {
        public LuckPotion() : base("Luck Potion", 'L', false) { }
        public override string GetDisplayName() => 
            $"Luck Potion";
        public PotionEffectBaseClass CreatePotionEffect(Player player)
        {
            return new LuckPotionEffect(player, 4);
        }
        public void ConsumePotion(Player player)
        {

        }
    }
}
