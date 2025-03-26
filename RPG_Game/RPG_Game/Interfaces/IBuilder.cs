using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.Interfaces
{
    public interface IBuilder
    {
        void BuildEmptyDungeon();
        void BuildFilledDungeon();
        void AddPaths();
        void AddChambers();
        void AddCentralRoom();
        void AddItems();
        void AddWeapons();
        void AddModifiedWeapons();
        void AddPotions();
        void AddEnemies();
        Room GetFinalResult();
    }
}
