using RPG_Game.Builders;
using RPG_Game.InputHandlers;
using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace RPG_Game
{
    public class Game
    {
        /*
        public Player player;
        public Room room = new Room();
        private string _instructions;
        public bool gameIsRunning;
        private readonly GameDisplayer _gameDisplayer = GameDisplayer.Instance;

        private IInputHandler? _inputHandler;
        
        public Game(int version)
        {
            player = new Player();
            gameIsRunning = true;
            BuildGame(version);
            _instructions = "";
        }
        public void BuildGame(int version)
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
                    Console.WriteLine("You have to enter 1, 2 or 3 to start a game!");
                    break;
            }

            inputHandlerChainBuilder.AddQuitAndDefaultHandlers();
            room = dungeonBuilder.GetFinalResult();
            _instructions = instructionBuilder.GetFinalResult();
            _inputHandler = inputHandlerChainBuilder.CreateChainFromList();
        }
        public void StartGame()
        {
            Console.WriteLine("Hi! Glad to see you here again! Tap any key to start a new game...");
            Console.ReadKey(true);
            _gameDisplayer.Initialize(room, player, _instructions);
            while (gameIsRunning)
            {
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
                _inputHandler?.HandleInput(consoleKeyInfo, this);
                _gameDisplayer.DrawPlayerStats(player);   
            }
        }
        */
    }
}
