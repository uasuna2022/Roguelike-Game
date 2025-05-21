using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.PotionEffects
{
    public class LuckPotionEffect: PotionEffectBaseClass
    {
        public int TotalTurns { get; }
        public int OriginalLuck { get; }
        public LuckPotionEffect(Player player, int duration): base(player, duration)
        {
            TotalTurns = duration;
            OriginalLuck = player.Luck;
        }
        public override void AfterApply()
        {
            player.Luck = OriginalLuck * TotalTurns;
            //GameDisplayer.Instance.AddNotification($"Player's luck multiplied by {_totalTurns}!");
            player.Notify($"Player's luck multiplied by {TotalTurns}!");
        }
        protected override void ApplyTurnEffect()
        {
            if (turnsRemaining > 0)
            {
                player.Luck = OriginalLuck * turnsRemaining;
                //GameDisplayer.Instance.AddNotification($"Luck multiplier is now {turnsRemaining}");
                player.Notify($"Luck multiplier is now {turnsRemaining}");
            }
        }
        public override void AfterExpire()
        {
            player.Luck = OriginalLuck;
            //GameDisplayer.Instance.AddNotification($"Luck Potion expired! Player's luck is {_originalLuck} again!");
            player.Notify($"Luck Potion expired! Player's luck is {OriginalLuck} again!");
            player.activeEffects.Remove(this);
        }
        public override string ToString()
        {
            return $"Luck Potion: x{turnsRemaining} to player's luck, {turnsRemaining} turns left!";
        }
    }
}
