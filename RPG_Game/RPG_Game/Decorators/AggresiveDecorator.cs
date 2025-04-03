using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.Decorators
{
    public class AggresiveDecorator: WeaponDecoratorBaseClass
    {
        public AggresiveDecorator(IWeapon weapon): base(weapon) { }
        public override string GetDisplayName()
        {
            return wrappedWeapon.GetDisplayName() + $" (Aggresive)";
        }
        public override void EquipPlayer(Player player)
        {
            base.EquipPlayer(player);
            player.Aggression += 3;
            player.Strength += 3;
            GameDisplayer.Instance.AddNotification($"Equipped with {this.GetDisplayName()}");
        }

        public override void UnequipPlayer(Player player)
        {
            base.UnequipPlayer(player);
            player.Aggression -= 3;
            player.Strength -= 3;
            GameDisplayer.Instance.AddNotification($"Unequipped with {this.GetDisplayName()}");
        }
    }
}
