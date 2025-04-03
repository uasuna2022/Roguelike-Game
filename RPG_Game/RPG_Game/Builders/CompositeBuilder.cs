using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_Game.Interfaces;

namespace RPG_Game.Builders
{
    public class CompositeBuilder : IBuilder
    {
        public List<IBuilder> _builders;
        public CompositeBuilder()
        {
            _builders = new List<IBuilder>();
        }
        public void AddBuilderToList(IBuilder builder)
        {
            _builders.Add(builder);
        }
        public IBuilder BuildEmptyDungeon()
        {
            foreach (IBuilder builder in _builders)
            {
                builder.BuildEmptyDungeon();
            }
            return this;
        }
        public IBuilder BuildFilledDungeon()
        {
            foreach (IBuilder builder in _builders)
            {
                builder.BuildFilledDungeon();
            }
            return this;
        }
        public IBuilder AddPaths()
        {
            foreach (IBuilder builder in _builders)
            {
                builder.AddPaths();
            }
            return this;
        }
        public IBuilder AddCentralRoom()
        {
            foreach (IBuilder builder in _builders)
            {
                builder.AddCentralRoom();
            }
            return this;
        }
        public IBuilder AddChambers()
        {
            foreach (IBuilder builder in _builders)
            {
                builder.AddChambers();
            }
            return this;
        }
        public IBuilder AddItems()
        {
            foreach (IBuilder builder in _builders)
            {
                builder.AddItems();
            }
            return this;
        }
        public IBuilder AddWeapons()
        {
            foreach (IBuilder builder in _builders)
            {
                builder.AddWeapons();
            }
            return this;
        }
        public IBuilder AddModifiedWeapons()
        {
            foreach (IBuilder builder in _builders)
            {
                builder.AddModifiedWeapons();
            }
            return this;
        }
        public IBuilder AddPotions()
        {
            foreach (IBuilder builder in _builders)
            {
                builder.AddPotions();
            }
            return this;
        }
        public IBuilder AddEnemies()
        {
            foreach (IBuilder builder in _builders)
            {
                builder.AddEnemies();
            }
            return this;
        }
    }
    
}
