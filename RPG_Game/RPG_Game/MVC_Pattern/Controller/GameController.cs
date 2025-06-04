using RPG_Game.Interfaces;
using RPG_Game.JSON_Serialization;
using RPG_Game.MVC_Pattern.Model;
using RPG_Game.MVC_Pattern.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace RPG_Game.MVC_Pattern.Controller
{
    public class GameController
    {
        private readonly GameState _gameState;
        private readonly IInputHandler _rootHandler;
        private readonly ConsoleView _consoleView;
        private readonly int _localPlayerIdx;
        private bool _gameIsRunning;
        private int _stepCount = 0;

        public GameState GameState => _gameState;
        public int LocalPlayerIdx => _localPlayerIdx;
        public void RequestQuit() => _gameIsRunning = false;
        public GameController(int version)
        {
            var (gameState, instructions, rootHandler) = GameBuilder.BuildGame(version);
            _gameState = gameState;
            _rootHandler = rootHandler;
            _localPlayerIdx = 0; // na razie 0, potem zmienię

            foreach (Player p in _gameState.Players)
            {
                p.SetGameState(_gameState);
            }

            _consoleView = ConsoleView.Instance;
            _consoleView.Initialize(_gameState, _localPlayerIdx, instructions);

            _gameState.Players[_localPlayerIdx].PlayerDied += RequestQuit;
        }

        public void Run()
        {
            _gameIsRunning = true;

            while (_gameIsRunning)
            {
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
                _rootHandler.HandleInput(consoleKeyInfo, this);
                // count steps

                /*
                if (_gameState.StepCounter == 10)
                {
                    var dto = DTOMapper.ConvertToDTO(_gameState);
                    var jsonOpts = new JsonSerializerOptions { WriteIndented = true };
                    jsonOpts.Converters.Add(new JsonStringEnumConverter());
                    string json = JsonSerializer.Serialize(dto, jsonOpts);
                    File.WriteAllText("test.txt", json);
                }
                */
            }
        }      
    }
}
