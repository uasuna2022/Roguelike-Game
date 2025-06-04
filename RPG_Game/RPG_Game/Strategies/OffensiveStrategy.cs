using RPG_Game.Interfaces;
using RPG_Game.MVC_Pattern.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.Strategies
{
    public class OffensiveStrategy : IStrategy
    {
        public void React(IEnemy reactingEnemy, GameState gameState)
        {
            Player chasedPlayer;
            int distance;
            (chasedPlayer, distance) = reactingEnemy.FindNearestPlayer(gameState);

            if (distance > 4) // if a distance to the nearest player is way too big, an enemy doesn't chase him and just does nothing
                return;
            while (true)
            {
                // 1) enemy's direction is left
                if (chasedPlayer.X == reactingEnemy.X && chasedPlayer.Y < reactingEnemy.Y)
                {
                    Cell newCell = gameState.Room.GetCell(reactingEnemy.X, reactingEnemy.Y - 1);

                    if (newCell.isWall || newCell.Enemy != null || newCell.ContainsPlayer) // if an enemy can't make a move there, it does nothing
                        break;

                    gameState.Room.Grid[reactingEnemy.X, reactingEnemy.Y].Enemy = null;
                    newCell.Enemy = reactingEnemy;
                    reactingEnemy.Y--;

                    break;
                }

                // 2) enemy's direction is right
                if (chasedPlayer.X == reactingEnemy.X && chasedPlayer.Y > reactingEnemy.Y)
                {
                    Cell newCell = gameState.Room.GetCell(reactingEnemy.X, reactingEnemy.Y + 1);

                    if (newCell.isWall || newCell.Enemy != null || newCell.ContainsPlayer) // if an enemy can't make a move there, it does nothing
                        break;

                    gameState.Room.Grid[reactingEnemy.X, reactingEnemy.Y].Enemy = null;
                    newCell.Enemy = reactingEnemy;
                    reactingEnemy.Y++;

                    break;
                }

                // 3) enemy's direction is up
                if (chasedPlayer.X < reactingEnemy.X && chasedPlayer.Y == reactingEnemy.Y)
                {
                    Cell newCell = gameState.Room.GetCell(reactingEnemy.X - 1, reactingEnemy.Y);

                    if (newCell.isWall || newCell.Enemy != null || newCell.ContainsPlayer) // if an enemy can't make a move there, it does nothing
                        break;

                    gameState.Room.Grid[reactingEnemy.X, reactingEnemy.Y].Enemy = null;
                    newCell.Enemy = reactingEnemy;
                    reactingEnemy.X--;

                    break;
                }

                // 4) enemy's direction is down
                if (chasedPlayer.X > reactingEnemy.X && chasedPlayer.Y == reactingEnemy.Y)
                {
                    Cell newCell = gameState.Room.GetCell(reactingEnemy.X + 1, reactingEnemy.Y);

                    if (newCell.isWall || newCell.Enemy != null || newCell.ContainsPlayer) // if an enemy can't make a move there, it does nothing
                        break;

                    gameState.Room.Grid[reactingEnemy.X, reactingEnemy.Y].Enemy = null;
                    newCell.Enemy = reactingEnemy;
                    reactingEnemy.X++;

                    break;
                }

                // 5) enemy's direction is left-down
                if (chasedPlayer.X > reactingEnemy.X && chasedPlayer.Y < reactingEnemy.Y)
                {
                    Cell newCell = gameState.Room.GetCell(reactingEnemy.X + 1, reactingEnemy.Y);

                    if (newCell.isWall || newCell.Enemy != null || newCell.ContainsPlayer)
                    {
                        Cell newCell2 = gameState.Room.GetCell(reactingEnemy.X, reactingEnemy.Y - 1);
                        if (newCell2.isWall || newCell2.Enemy != null || newCell2.ContainsPlayer)
                            break;

                        gameState.Room.Grid[reactingEnemy.X, reactingEnemy.Y].Enemy = null;
                        newCell2.Enemy = reactingEnemy;
                        reactingEnemy.Y--;
                        break;
                    }

                    gameState.Room.Grid[reactingEnemy.X, reactingEnemy.Y].Enemy = null;
                    newCell.Enemy = reactingEnemy;
                    reactingEnemy.X++;

                    break;
                }

                // 6) enemy's direction is left-up
                if (chasedPlayer.X < reactingEnemy.X && chasedPlayer.Y < reactingEnemy.Y)
                {
                    Cell newCell = gameState.Room.GetCell(reactingEnemy.X - 1, reactingEnemy.Y);

                    if (newCell.isWall || newCell.Enemy != null || newCell.ContainsPlayer)
                    {
                        Cell newCell2 = gameState.Room.GetCell(reactingEnemy.X, reactingEnemy.Y - 1);
                        if (newCell2.isWall || newCell2.Enemy != null || newCell2.ContainsPlayer)
                            break;

                        gameState.Room.Grid[reactingEnemy.X, reactingEnemy.Y].Enemy = null;
                        newCell2.Enemy = reactingEnemy;
                        reactingEnemy.Y--;
                        break;
                    }

                    gameState.Room.Grid[reactingEnemy.X, reactingEnemy.Y].Enemy = null;
                    newCell.Enemy = reactingEnemy;
                    reactingEnemy.X--;

                    break;
                }

                // 7) enemy's direction is right-down
                if (chasedPlayer.X > reactingEnemy.X && chasedPlayer.Y > reactingEnemy.Y)
                {
                    Cell newCell = gameState.Room.GetCell(reactingEnemy.X + 1, reactingEnemy.Y);

                    if (newCell.isWall || newCell.Enemy != null || newCell.ContainsPlayer)
                    {
                        Cell newCell2 = gameState.Room.GetCell(reactingEnemy.X, reactingEnemy.Y + 1);
                        if (newCell2.isWall || newCell2.Enemy != null || newCell2.ContainsPlayer)
                            break;

                        gameState.Room.Grid[reactingEnemy.X, reactingEnemy.Y].Enemy = null;
                        newCell2.Enemy = reactingEnemy;
                        reactingEnemy.Y++;
                        break;
                    }

                    gameState.Room.Grid[reactingEnemy.X, reactingEnemy.Y].Enemy = null;
                    newCell.Enemy = reactingEnemy;
                    reactingEnemy.X++;

                    break;
                }

                // 8) enemy's direction is right-up
                if (chasedPlayer.X < reactingEnemy.X && chasedPlayer.Y > reactingEnemy.Y)
                {
                    Cell newCell = gameState.Room.GetCell(reactingEnemy.X - 1, reactingEnemy.Y);

                    if (newCell.isWall || newCell.Enemy != null || newCell.ContainsPlayer)
                    {
                        Cell newCell2 = gameState.Room.GetCell(reactingEnemy.X, reactingEnemy.Y + 1);
                        if (newCell2.isWall || newCell2.Enemy != null || newCell2.ContainsPlayer)
                            break;

                        gameState.Room.Grid[reactingEnemy.X, reactingEnemy.Y].Enemy = null;
                        newCell2.Enemy = reactingEnemy;
                        reactingEnemy.Y++;
                        break;
                    }

                    gameState.Room.Grid[reactingEnemy.X, reactingEnemy.Y].Enemy = null;
                    newCell.Enemy = reactingEnemy;
                    reactingEnemy.X--;

                    break;
                }
            }           


            
            if (Math.Abs(reactingEnemy.X - chasedPlayer.X) + Math.Abs(reactingEnemy.Y - chasedPlayer.Y) == 1)
            {
                if (!reactingEnemy.AttackPlayer(chasedPlayer))
                {
                    chasedPlayer.Notify($"You were KILLED by {reactingEnemy.Name}! Game OVER!");
                    chasedPlayer.OnPlayerDied();
                }
            }

        }
    }
}
