using RPG_Game.PotionEffects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.Interfaces
{
    public interface IPotion: IItem
    {
        void ConsumePotion(Player player);
        PotionEffectBaseClass CreatePotionEffect(Player player);
    }
}
