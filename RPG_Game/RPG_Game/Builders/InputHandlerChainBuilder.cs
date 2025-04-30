using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_Game.InputHandlers;
using RPG_Game.Interfaces;

namespace RPG_Game.Builders
{
    public class InputHandlerChainBuilder: IBuilder
    {
        private readonly List<IInputHandler> _inputHandlers = new List<IInputHandler>();
        public InputHandlerChainBuilder() { }
        public IBuilder BuildEmptyDungeon()
        {
            _inputHandlers.Add(new MoveHandler());
            return this;
        }
        public IBuilder BuildFilledDungeon()
        {
            _inputHandlers.Add(new MoveHandler());
            return this;
        }
        public IBuilder AddPaths() { return this; }
        public IBuilder AddCentralRoom() { return this; }
        public IBuilder AddChambers() { return this; }  
        public IBuilder AddItems()
        {
            _inputHandlers.Add(new PickingUpHandler());
            _inputHandlers.Add(new DropHandler());
            return this;
        }
        public IBuilder AddWeapons()
        {
            _inputHandlers.Add(new EquipHandler());
            _inputHandlers.Add(new UnequipHandler());
            return this;
        }
        public IBuilder AddModifiedWeapons()
        {
            _inputHandlers.Add(new EquipHandler());
            _inputHandlers.Add(new UnequipHandler());
            return this;
        }
        public IBuilder AddPotions()
        {
            _inputHandlers.Add(new DrinkPotionHandler());
            return this;
        }
        public IBuilder AddEnemies()
        {
            _inputHandlers.Add(new FightHandler());
            return this;
        }
        public InputHandlerChainBuilder AddQuitAndDefaultHandlers()
        {
            _inputHandlers.Add(new QuitHandler());
            _inputHandlers.Add(new DefaultHandler());
            return this;
        }
        public IInputHandler CreateChainFromList()
        {
            IInputHandler root = _inputHandlers[0];
            IInputHandler current = root;

            for (int i = 1; i < _inputHandlers.Count; i++)
            {
                current.SetNext(_inputHandlers[i]);
                current = _inputHandlers[i];
            }

            return root;
        }

    }
}
