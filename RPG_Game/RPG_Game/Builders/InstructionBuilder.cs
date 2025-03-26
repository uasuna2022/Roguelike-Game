using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.Builders
{
    public class InstructionBuilder
    {
        private readonly List<string> _instructions = new List<string>();
        private readonly StringBuilder _stringBuilder = new StringBuilder();
        public InstructionBuilder() { }
        public InstructionBuilder AddBasicControls()
        {
            _instructions.Add("WASD - Move");
            _instructions.Add("Q - Quit");
            return this;
        }
        public InstructionBuilder AddItemInstructions() // Potentially can add 'if' to check if there are items on the ground
        {
            _instructions.Add("E - Pick up item");
            return this;
        }
        public InstructionBuilder AddWeaponInstructions() // Potentially can add 'if' to check is the player's inventory empty 
        {
            _instructions.Add("I + 1,2,...,0 - Equip concrete item from the inventory");
            _instructions.Add("O + L,R - Unequip left or right hand");
            return this;
        }
        public string Build()
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
