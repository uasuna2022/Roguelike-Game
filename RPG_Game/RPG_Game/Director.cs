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
        private readonly IBuilder _builder;
        private readonly InstructionBuilder _instructionBuilder;
        public Director(IBuilder builder)
        {
            _builder = builder;
            _instructionBuilder = new InstructionBuilder();
        }
        public void BuildDungeonWithoutWalls()
        {
            _builder.BuildEmptyDungeon();
            _builder.AddItems();
            _builder.AddWeapons();
            _builder.AddModifiedWeapons();
            _builder.AddEnemies();
            _builder.AddPotions();
            _instructionBuilder.AddBasicControls();
            _instructionBuilder.AddWeaponInstructions();
            _instructionBuilder.AddItemInstructions();
        }
        public void BuildBasicDungeonWithWalls() // 'basic' means without modified weapons, potions and enemies
        {
            _builder.BuildFilledDungeon();
            _builder.AddCentralRoom();
            _builder.AddChambers();
            _builder.AddPaths();
            _builder.AddWeapons();
            _builder.AddItems();
            _instructionBuilder.AddBasicControls();
            _instructionBuilder.AddWeaponInstructions();
            _instructionBuilder.AddItemInstructions();
        }
        public void BuildFullDungeonWithWalls() // 'full' means with all the possible items/enemies/etc.
        {
            _builder.BuildFilledDungeon();
            _builder.AddCentralRoom();
            _builder.AddChambers();
            _builder.AddPaths();
            _builder.AddWeapons();
            _builder.AddItems();
            _builder.AddEnemies();
            _builder.AddModifiedWeapons();
            _builder.AddPotions();
            _instructionBuilder.AddBasicControls();
            _instructionBuilder.AddWeaponInstructions();
            _instructionBuilder.AddItemInstructions();
        }
        public (Room, string) GetFinalResult()
        {
            return (_builder.GetFinalResult(), _instructionBuilder.Build());
        }
    }
}
