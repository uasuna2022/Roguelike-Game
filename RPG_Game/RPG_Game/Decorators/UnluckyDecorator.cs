using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.Decorators
{
    public class UnluckyDecorator: WeaponDecoratorBaseClass
    {
        public UnluckyDecorator(IWeapon weapon): base(weapon) { }

        public override string GetDisplayName()
        {
            return wrappedWeapon.GetDisplayName() + " (Unlucky)";
        }
        public override void UnequipPlayer(Player player)
        {
            base.UnequipPlayer(player);
            player.Luck += 5;
            GameDisplayer.Instance.AddNotification($"Unequipped: {this.GetDisplayName()}");
        }
        public override void EquipPlayer(Player player)
        {
            base.EquipPlayer(player);
            player.Luck -= 5;
            GameDisplayer.Instance.AddNotification($"Equipped with: {this.GetDisplayName()}");
        }
    }
}
