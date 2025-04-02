using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.Builders
{
    public class InstructionBuilder: IBuilder
    {
        private readonly List<string> _instructions = new List<string>();
        private readonly StringBuilder _stringBuilder = new StringBuilder();
        public InstructionBuilder() { }
        public IBuilder BuildEmptyDungeon()
        {
            _instructions.Add("WASD - Move");
            _instructions.Add("Q - Quit");
            return this;
        }
        public IBuilder BuildFilledDungeon()
        {
            _instructions.Add("WASD - Move");
            _instructions.Add("Q - Quit");
            return this;
        }
        public IBuilder AddItems() // Potentially can add 'if' to check if there are items on the ground
        {
            _instructions.Add("E - Pick up item");
            return this;
        }
        public IBuilder AddWeapons() // Potentially can add 'if' to check is the player's inventory empty 
        {
            _instructions.Add("I + 1,2,...,0 - Equip concrete item from the inventory");
            _instructions.Add("O + L,R - Unequip left or right hand");
            return this;
        }
        public IBuilder AddModifiedWeapons()
        {
            _instructions.Add("I + 1,2,...,0 - Equip concrete item from the inventory");
            _instructions.Add("O + L,R - Unequip left or right hand");
            return this;
        }
        public IBuilder AddPotions() { return this; }
        public IBuilder AddCentralRoom() { return this; }
        public IBuilder AddChambers() { return this; }
        public IBuilder AddPaths() { return this; }
        public IBuilder AddEnemies() { return this; }
        public string GetFinalResult()
        {
            foreach (string instruction in _instructions)
            {
                _stringBuilder.Append(instruction);
                _stringBuilder.Append('\n');
            }
            return _stringBuilder.ToString();
        }
    }
}
