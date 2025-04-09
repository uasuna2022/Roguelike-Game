using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_Game.Interfaces;

namespace RPG_Game.PotionEffects
{
    public abstract class PotionEffectBaseClass: IObserver
    {
        protected Player player;
        protected int turnsRemaining;
        public PotionEffectBaseClass(Player player, int duration)
        {
            this.player = player;
            this.turnsRemaining = duration;
        }
        public virtual void AfterApply() { }
        public virtual void Update()
        {
            turnsRemaining--;
            ApplyTurnEffect();
            if (turnsRemaining == 0)
            {
                AfterExpire();
                player.Detach(this);
            }
        }
        protected virtual void ApplyTurnEffect() { }
        public virtual void AfterExpire() { }
        public override string ToString()
        {
            return $"{this.GetType().Name}: {turnsRemaining} turns left";
        }
    }
}
