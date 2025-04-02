using RPG_Game.Interfaces;
using RPG_Game.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game
{
    public class Director
    {
        public Director() { }
        public void BuildDungeonWithoutWalls(CompositeBuilder compositeBuilder)
        {
            compositeBuilder.BuildEmptyDungeon().AddItems().AddWeapons().AddModifiedWeapons().
                AddEnemies().AddPotions();
        }
        public void BuildBasicDungeonWithWalls(CompositeBuilder compositeBuilder) // 'basic' means without modified weapons, potions and enemies
        {
            compositeBuilder.BuildFilledDungeon().AddItems().AddWeapons().AddPaths().
                AddChambers().AddCentralRoom();
        }
        public void BuildFullDungeonWithWalls(CompositeBuilder compositeBuilder) // 'full' means with all the possible items/enemies/etc.
        {
            compositeBuilder.BuildFilledDungeon().AddCentralRoom().AddChambers().AddPaths().
                AddWeapons().AddItems().AddEnemies().AddModifiedWeapons().AddPotions();
        }
    }
}
