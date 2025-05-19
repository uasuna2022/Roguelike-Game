using RPG_Game.Builders;
using RPG_Game.InputHandlers;
using RPG_Game.Interfaces;
using RPG_Game.MVC_Pattern.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.MVC_Pattern.Controller
{
    public static class GameBuilder
    {
        public static (GameState State, string Instructions, IInputHandler root) BuildGame(int version)
        {
            Director director = new Director();

            CompositeBuilder compositeBuilder = new CompositeBuilder();
            DungeonBuilder dungeonBuilder = new DungeonBuilder();
            InstructionBuilder instructionBuilder = new InstructionBuilder();
            InputHandlerChainBuilder inputHandlerChainBuilder = new InputHandlerChainBuilder();

            compositeBuilder.AddBuilderToList(dungeonBuilder);
            compositeBuilder.AddBuilderToList(instructionBuilder);
            compositeBuilder.AddBuilderToList(inputHandlerChainBuilder);

            switch (version)
            {
                case 1:
                    director.BuildBasicDungeonWithWalls(compositeBuilder);
                    break;
                case 2:
                    director.BuildFullDungeonWithWalls(compositeBuilder);
                    break;
                case 3:
                    director.BuildDungeonWithoutWalls(compositeBuilder);
                    break;
                default:
                    // Console.WriteLine("You have to enter 1, 2 or 3 to start a game!");
                    break;
            }

            inputHandlerChainBuilder.AddQuitAndDefaultHandlers();

            Room room = dungeonBuilder.GetFinalResult();
            string instructions = instructionBuilder.GetFinalResult();
            IInputHandler root = inputHandlerChainBuilder.CreateChainFromList();

            List<Player> playerList = [new Player()];

            GameState State = new GameState(playerList, room, version);

            return (State, instructions, root);
        }
    }
}
