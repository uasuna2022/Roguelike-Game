using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.PotionEffects
{
    public class LuckPotionEffect: PotionEffectBaseClass
    {
        private int _totalTurns;
        private int _originalLuck;
        public LuckPotionEffect(Player player, int duration): base(player, duration)
        {
            _totalTurns = duration;
            _originalLuck = player.Luck;
        }
        public override void AfterApply()
        {
            player.Luck = _originalLuck * _totalTurns;
            GameDisplayer.Instance.AddNotification($"Player's luck multiplied by {_totalTurns}!");
        }
        protected override void ApplyTurnEffect()
        {
            if (turnsRemaining > 0)
            {
                player.Luck = _originalLuck * turnsRemaining;
                GameDisplayer.Instance.AddNotification($"Luck multiplier is now {turnsRemaining}");
            }
        }
        protected override void AfterExpire()
        {
            player.Luck = _originalLuck;
            GameDisplayer.Instance.AddNotification($"Luck Potion expired! Player's luck is {_originalLuck} again!");
        }
        public override string ToString()
        {
            return $"{this.GetType().ToString}: x{turnsRemaining} to player's luck, {turnsRemaining} turns left!";
        }
    }
}
