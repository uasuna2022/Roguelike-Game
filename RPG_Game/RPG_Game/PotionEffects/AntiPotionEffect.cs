using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.PotionEffects
{
    public class AntiPotionEffect: PotionEffectBaseClass
    {
        public AntiPotionEffect(Player player, int duration) : base(player, duration) { }
        public override void AfterApply()
        {
            foreach (var effect in player.activeEffects.ToList())
            {
                effect.AfterExpire();
            }
            player.observers.Clear();
            //GameDisplayer.Instance.ClearNotifications();
            //GameDisplayer.Instance.AddNotification("All effects cancelled!");
            player.Notify("All effects cancelled!");
        }
        public override void Update() { }
        public override string ToString()
        {
            return $"Antidot - all effects to be deleted!";
        }
    }
}
