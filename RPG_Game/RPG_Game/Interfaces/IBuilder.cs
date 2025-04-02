using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.Interfaces
{
    public interface IBuilder
    {
        IBuilder BuildEmptyDungeon();
        IBuilder BuildFilledDungeon();
        IBuilder AddPaths();
        IBuilder AddChambers();
        IBuilder AddCentralRoom();
        IBuilder AddItems();
        IBuilder AddWeapons();
        IBuilder AddModifiedWeapons();
        IBuilder AddPotions();
        IBuilder AddEnemies();
    }
}
