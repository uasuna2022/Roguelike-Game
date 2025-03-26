using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_Game.Decorators;
using RPG_Game.Enemies;
using RPG_Game.Interfaces;
using RPG_Game.Items.Currency;
using RPG_Game.Items.Potions;
using RPG_Game.Items.UnusableItems;
using RPG_Game.Items.Weapons;

namespace RPG_Game.Builders
{

    public class DungeonBuilder : IBuilder
    {
        private Room _room;
        private Random _random;

        public DungeonBuilder()
        {
            _room = new Room();
            _random = new Random();
        }
        public void BuildEmptyDungeon()
        {
            for (int i = 0; i < _room.Height; i++)
            {
                for (int j = 0; j < _room.Width; j++)
                {
                    _room.Grid[i, j].isWall = false;
                }
            }
        }
        public void BuildFilledDungeon()
        {
            for (int i = 0; i < _room.Height; i++)
            {
                for (int j = 0; j < _room.Width; j++)
                {
                    _room.Grid[i, j].isWall = true;
                }
            }
        }
        public void AddCentralRoom()
        {
            for (int i = _room.Height / 3; i <= _room.Height * 2 / 3; i++)
            {
                for (int j = _room.Width / 3; j <= _room.Width * 2 / 3; j++)
                {
                    _room.Grid[i, j].isWall = false;
                }
            }
        }
        public void AddEnemies()
        {
            for (int i = 0; i < _room.Height; i++)
            {
                for (int j = 0; j < _room.Width; j++)
                {
                    int randomValue = _random.Next(1, 101);
                    if (!_room.Grid[i, j].isWall)
                    {
                        switch (randomValue)
                        {
                            case 1:
                                _room.Grid[i, j].AddEnemy(new Goblin());
                                break;
                            case 2:
                                _room.Grid[i, j].AddEnemy(new Rat());
                                break;
                            case 3:
                                _room.Grid[i, j].AddEnemy(new Dragon());
                                break;
                            default:
                                continue;
                        }
                    }
                }
            }
        }
        public void AddPotions()
        {
            for (int i = 0; i < _room.Height; i++)
            {
                for (int j = 0; j < _room.Width; j++)
                {
                    int randomValue = _random.Next(1, 101);
                    int volumeRandomValue = _random.Next(25, 101);
                    double healingRandomValue = _random.Next(1, 11) / 10.0;
                    if (!_room.Grid[i, j].isWall)
                    {
                        switch (randomValue)
                        {
                            case 1:
                                _room.Grid[i, j].AddItem(new ElixirPotion(volumeRandomValue, healingRandomValue));
                                break;
                            case 2:
                                _room.Grid[i, j].AddItem(new JuicePotion(volumeRandomValue, healingRandomValue));
                                break;
                            default:
                                continue;
                        }
                    }
                }
            }
        }
        public void AddWeapons()
        {
            for (int i = 0; i < _room.Height; i++)
            {
                for (int j = 0; j < _room.Width; j++)
                {
                    int randomValue = _random.Next(1, 101);
                    if (!_room.Grid[i, j].isWall)
                    {
                        switch (randomValue)
                        {
                            case 1:
                                _room.Grid[i, j].AddItem(new Gun());
                                break;
                            case 2:
                                _room.Grid[i, j].AddItem(new _2HandedSword());
                                break;
                            case 3:
                                _room.Grid[i, j].AddItem(new VerbalAbuse());
                                break;
                            default:
                                continue;
                        }
                    }
                }
            }
        }

        private IWeapon ApplyRandomDecorators(IWeapon weapon)
        {
            List<Func<IWeapon, IWeapon>> availableDecorators = new List<Func<IWeapon, IWeapon>>
            {
                weapon => new AggresiveDecorator(weapon),
                weapon => new PowerfulDecorator(weapon),
                weapon => new UnluckyDecorator(weapon)
            };

            List<Func<IWeapon, IWeapon>> randomlySwappedDecorators =
                availableDecorators.OrderBy(r => _random.Next()).ToList();

            int decoratorCount = _random.Next(1, 4);

            for (int i = 0; i < decoratorCount; i++)
            {
                weapon = randomlySwappedDecorators[i](weapon);
            }

            return weapon;
        }
        public void AddModifiedWeapons()
        {
            for (int i = 0; i < _room.Height; i++)
            {
                for (int j = 0; j < _room.Width; j++)
                {
                    int randomValue = _random.Next(1, 101);
                    if (!_room.Grid[i, j].isWall)
                    {
                        switch (randomValue)
                        {
                            case 1:
                                {
                                    IWeapon newWeapon = new Gun();
                                    newWeapon = ApplyRandomDecorators(newWeapon);
                                    _room.Grid[i, j].AddItem(newWeapon);
                                    break;
                                }
                            case 2:
                                {
                                    IWeapon newWeapon = new VerbalAbuse();
                                    newWeapon = ApplyRandomDecorators(newWeapon);
                                    _room.Grid[i, j].AddItem(newWeapon);
                                    break;
                                }
                            case 3:
                                {
                                    IWeapon newWeapon = new _2HandedSword();
                                    newWeapon = ApplyRandomDecorators(newWeapon);
                                    _room.Grid[i, j].AddItem(newWeapon);
                                    break;
                                }
                            default:
                                continue;
                        }
                    }
                }
            }
        }
        public void AddItems()
        {
            for (int i = 0; i < _room.Height; i++)
            {
                for (int j = 0; j < _room.Width; j++)
                {
                    int typeRandom = _random.Next(1, 3);
                    int probability = _random.Next(1, 101);
                    if (typeRandom == 1 && probability <= 2) // currency
                    {
                        switch (probability)
                        {
                            case 1:
                                {
                                    _room.Grid[i, j].AddItem(new Coin(_random.Next(5, 26)));
                                    break;
                                }
                            case 2:
                                {
                                    _room.Grid[i, j].AddItem(new Gold(_random.Next(5, 26)));
                                    break;
                                }
                        }
                    }

                    if (typeRandom == 2 && probability <= 2) // unusable items
                    {
                        switch (probability)
                        {
                            case 1:
                                {
                                    _room.Grid[i, j].AddItem(new Book($"Book with ID: " +
                                        $"{_random.Next(100, 1000)}", $"Some Title {_random.Next(100, 1000)}"));
                                    break;
                                }
                            case 2:
                                {
                                    _room.Grid[i, j].AddItem(new Ring($"Ring with ID: {_random.Next(1000, 10000)}"));
                                    break;
                                }
                        }
                    }
                }
            }
        }
        public void AddPaths()
        {
            // creating the first path
            int FPStartX = 0;
            int FPStartY = 0;
            int FPEndX = _random.Next(_room.Height / 3, _room.Height * 2 / 3);
            int FPEndY = _random.Next(_room.Width / 3, _room.Width * 2 / 3);
            while (FPStartX != FPEndX || FPStartY != FPEndY)
            {
                _room.Grid[FPStartX, FPStartY].isWall = false;

                List<char> possibleMoves = new List<char>();
                if (FPEndX - FPStartX != 0) possibleMoves.Add('D');
                if (FPEndY - FPStartY != 0) possibleMoves.Add('R');

                char move = possibleMoves[_random.Next(possibleMoves.Count)];

                switch (move)
                {
                    case 'D': FPStartX++; break;
                    case 'R': FPStartY++; break;

                }
            }

            const int numberOfPaths = 20;
            for (int i = 0; i < numberOfPaths; i++)
            {
                int length = 0;
                int startX = 0;
                int startY = 0;
                int endX = 0;
                int endY = 0;
                while (length < 20)
                {
                    length = 0;
                    startX = _random.Next(0, _room.Height);
                    startY = _random.Next(0, _room.Width);
                    endX = _random.Next(0, _room.Height);
                    endY = _random.Next(0, _room.Width);
                    length = Math.Abs(startX - endX) + Math.Abs(startY - endY);
                }

                int currentX = startX;
                int currentY = startY;

                while (currentX != endX || currentY != endY)
                {
                    _room.Grid[currentX, currentY].isWall = false;

                    List<char> possibleMoves = new List<char>();
                    if (endX - currentX != 0) possibleMoves.Add(endX - currentX > 0 ? 'D' : 'U');
                    if (endY - currentY != 0) possibleMoves.Add(endY - currentY > 0 ? 'R' : 'L');

                    char move = possibleMoves[_random.Next(possibleMoves.Count)];

                    switch (move)
                    {
                        case 'D': currentX++; break;
                        case 'U': currentX--; break;
                        case 'R': currentY++; break;
                        case 'L': currentY--; break;
                    }
                }
                _room.Grid[currentX, currentY].isWall = false;
            }


        }
        public void AddChambers()
        {
            const int numberOfChambers = 5;

            for (int i = 0; i < numberOfChambers; i++)
            {
                int width = 0;
                int height = 0;
                while (width * height > 12)
                {
                    width = _random.Next(2, 7);
                    height = _random.Next(2, 7);
                }

                int startX = _random.Next(1, _room.Height - height - 1);
                int startY = _random.Next(1, _room.Width - width - 1);

                for (int x = startX; x < startX + height; x++)
                {
                    for (int y = startY; y < startY + width; y++)
                    {
                        _room.Grid[x, y].isWall = false;
                    }
                }
            }
        }
        public Room GetFinalResult() 
        {
            return _room;
        }
    }
}
