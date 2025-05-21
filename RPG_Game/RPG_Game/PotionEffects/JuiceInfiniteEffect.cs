using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.PotionEffects
{
    public class JuiceInfiniteEffect: PotionEffectBaseClass
    {
        public int DexterityBoost { get; }
        public JuiceInfiniteEffect(Player player, int duration, int dexterityBoost): base(player, duration)
        {
            DexterityBoost = dexterityBoost;
        }
        public override void AfterApply()
        {
            player.Dexterity += DexterityBoost;
            //GameDisplayer.Instance.AddNotification($"Player's dexterity increased by {_dexterityBoost} for infinite amount of steps!");
            player.Notify($"Player's dexterity increased by {DexterityBoost} for infinite amount of steps!");
        }
        public override void AfterExpire()
        {
            /*
            base.AfterExpire(); // as this is an eternal efect, this method can be left empty, cause it's never called,
                                // however after implementing antidotes it can potentially be called
            */
            player.Dexterity -= DexterityBoost;
            //GameDisplayer.Instance.AddNotification($"Juice's been cancelled. Player's dexterity decreased by {_dexterityBoost}!");
            player.Notify($"Juice's been cancelled. Player's dexterity decreased by {DexterityBoost}!");
            player.activeEffects.Remove(this);
        }
        public override string ToString()
        {
            return $"InfJuice Potion: +{DexterityBoost} to player's dexterity!";
        }
        public override void Update() { }
    }
}
