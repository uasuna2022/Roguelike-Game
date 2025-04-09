using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.PotionEffects
{
    public class PowerPotionEffect: PotionEffectBaseClass
    {
        private int _strengthBoost;
        public PowerPotionEffect(Player player, int duration, int strengthBoost): base(player, duration)
        {
            _strengthBoost = strengthBoost;
        }
        public override void AfterApply()
        {
            player.Strength += _strengthBoost;
            GameDisplayer.Instance.AddNotification($"Player's strength increased by {_strengthBoost} for {turnsRemaining} steps!");
        }
        protected override void AfterExpire()
        {
            player.Strength -= _strengthBoost;
            GameDisplayer.Instance.AddNotification($"Power Potion expired! Player's strength decreased by {_strengthBoost}!");
            player.activeEffects.Remove(this);
        }
        public override string ToString()
        {
            return $"Power Potion: +{_strengthBoost} to player's strength, {turnsRemaining} turns left!";
        }
    }
}
