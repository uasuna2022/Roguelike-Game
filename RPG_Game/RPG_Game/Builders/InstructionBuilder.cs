using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
            return BuildFilledDungeon();
        }
        public IBuilder BuildFilledDungeon()
        {
            _instructions.Add($"{FormatKey(InputKeyConfiguration.MoveUp)}{FormatKey(InputKeyConfiguration.MoveLeft)}" +
                $"{FormatKey(InputKeyConfiguration.MoveDown)}{FormatKey(InputKeyConfiguration.MoveRight)} - Move");
            _instructions.Add($"{FormatKey(InputKeyConfiguration.Quit)} - Quit");
            return this;
        }
        public IBuilder AddItems() // Potentially can add 'if' to check if there are items on the ground
        {
            _instructions.Add($"{FormatKey(InputKeyConfiguration.PickItem)} - Pick up item");
            _instructions.Add($"{FormatKey(InputKeyConfiguration.DropItem)} + 1,2,...,0 - Drop item from inventory");
            _instructions.Add($"{FormatKey(InputKeyConfiguration.DropItem)} + A - Drop all items from both hands and inventory");
            return this;
        }
        public IBuilder AddWeapons() // Potentially can add 'if' to check is the player's inventory empty 
        {
            _instructions.Add($"{FormatKey(InputKeyConfiguration.DropItem)} + L,R - Drop weapon from left or right hand");
            _instructions.Add($"{FormatKey(InputKeyConfiguration.Equip)} + 1,2,...,0 - Equip concrete item from the inventory");
            _instructions.Add($"{FormatKey(InputKeyConfiguration.Unequip)} + L,R - Unequip left or right hand");
            return this;
        }
        public IBuilder AddModifiedWeapons()
        {
            //return AddWeapons();
            return this;
        }
        public IBuilder AddPotions()
        {
            _instructions.Add($"{FormatKey(InputKeyConfiguration.DrinkPotion)} - Drink Potion");
            return this;
        }
        public IBuilder AddCentralRoom() { return this; }
        public IBuilder AddChambers() { return this; }
        public IBuilder AddPaths() { return this; }
        public IBuilder AddEnemies()
        {
            _instructions.Add($"{FormatKey(InputKeyConfiguration.Fight)} - Attack an enemy");
            return this;
        }
        public string GetFinalResult()
        {
            foreach (string instruction in _instructions)
            {
                _stringBuilder.Append(instruction);
                _stringBuilder.Append('\n');
            }
            return _stringBuilder.ToString();
        }
        private string FormatKey(ConsoleKey consoleKey) => consoleKey.ToString();
    }
}
