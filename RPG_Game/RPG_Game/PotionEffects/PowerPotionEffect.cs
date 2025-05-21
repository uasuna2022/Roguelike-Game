using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.PotionEffects
{
    public class PowerPotionEffect: PotionEffectBaseClass
    {
        public int StrengthBoost { get; }
        public PowerPotionEffect(Player player, int duration, int strengthBoost): base(player, duration)
        {
            StrengthBoost = strengthBoost;
        }
        public override void AfterApply()
        {
            player.Strength += StrengthBoost;
            //GameDisplayer.Instance.AddNotification($"Player's strength increased by {_strengthBoost} for {turnsRemaining} steps!");
            player.Notify($"Player's strength increased by {StrengthBoost} for {turnsRemaining} steps!");
        }
        public override void AfterExpire()
        {
            player.Strength -= StrengthBoost;
            //GameDisplayer.Instance.AddNotification($"Power Potion expired! Player's strength decreased by {_strengthBoost}!");
            player.Notify($"Power Potion expired! Player's strength decreased by {StrengthBoost}!");
            player.activeEffects.Remove(this);
        }
        public override string ToString()
        {
            return $"Power Potion: +{StrengthBoost} to player's strength, {turnsRemaining} turns left!";
        }
    }
}
