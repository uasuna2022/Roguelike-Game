using RPG_Game.EnumClasses;
using RPG_Game.Interfaces;
using RPG_Game.MVC_Pattern.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.Strategies
{
    public class DefensiveStrategy : IStrategy
    {
        public void React(IEnemy reactingEnemy, GameState gameState)
        {
            Player threatPlayer;
            int distance;
            (threatPlayer, distance) = reactingEnemy.FindNearestPlayer(gameState);

            if (distance > 7) // if a distance to the nearest enemy is way too big, the enemy stops running from him
                return;

            List<Direction> possibleDirectionsToRun = new List<Direction>();

            // 1) A player is to the left
            if (threatPlayer.X == reactingEnemy.X && threatPlayer.Y < reactingEnemy.Y)
            {
                possibleDirectionsToRun.Add(Direction.Up);
                possibleDirectionsToRun.Add(Direction.Down);
                possibleDirectionsToRun.Add(Direction.Right);
                TryToRunSomewhere(possibleDirectionsToRun, reactingEnemy, gameState.Room);
                return;
            }

            // 2) A player is to the right
            if (threatPlayer.X == reactingEnemy.X && threatPlayer.Y > reactingEnemy.Y)
            {
                possibleDirectionsToRun.Add(Direction.Up);
                possibleDirectionsToRun.Add(Direction.Down);
                possibleDirectionsToRun.Add(Direction.Left);
                TryToRunSomewhere(possibleDirectionsToRun, reactingEnemy, gameState.Room);
                return;
            }

            // 3) A player is to the top
            if (threatPlayer.X < reactingEnemy.X && threatPlayer.Y == reactingEnemy.Y)
            {
                possibleDirectionsToRun.Add(Direction.Left);
                possibleDirectionsToRun.Add(Direction.Down);
                possibleDirectionsToRun.Add(Direction.Right);
                TryToRunSomewhere(possibleDirectionsToRun, reactingEnemy, gameState.Room);
                return;
            }

            // 4) A player is to the bottom
            if (threatPlayer.X > reactingEnemy.X && threatPlayer.Y == reactingEnemy.Y)
            {
                possibleDirectionsToRun.Add(Direction.Up);
                possibleDirectionsToRun.Add(Direction.Left);
                possibleDirectionsToRun.Add(Direction.Right);
                TryToRunSomewhere(possibleDirectionsToRun, reactingEnemy, gameState.Room);
                return;
            }

            // 5) A player is to the top-left
            if (threatPlayer.X < reactingEnemy.X && threatPlayer.Y < reactingEnemy.Y)
            {
                possibleDirectionsToRun.Add(Direction.Down);
                possibleDirectionsToRun.Add(Direction.Right);
                TryToRunSomewhere(possibleDirectionsToRun, reactingEnemy, gameState.Room);
                return;
            }

            // 6) A player is to the bottom-right
            if (threatPlayer.X > reactingEnemy.X && threatPlayer.Y > reactingEnemy.Y)
            {
                possibleDirectionsToRun.Add(Direction.Left);
                possibleDirectionsToRun.Add(Direction.Up);
                TryToRunSomewhere(possibleDirectionsToRun, reactingEnemy, gameState.Room);
                return;
            }

            // 7) A player is to the top-right
            if (threatPlayer.X < reactingEnemy.X && threatPlayer.Y > reactingEnemy.Y)
            {
                possibleDirectionsToRun.Add(Direction.Down);
                possibleDirectionsToRun.Add(Direction.Left);
                TryToRunSomewhere(possibleDirectionsToRun, reactingEnemy, gameState.Room);
                return;
            }

            // 8) A player is to the bottom-left
            if (threatPlayer.X > reactingEnemy.X && threatPlayer.Y < reactingEnemy.Y)
            {
                possibleDirectionsToRun.Add(Direction.Up);
                possibleDirectionsToRun.Add(Direction.Right);
                TryToRunSomewhere(possibleDirectionsToRun, reactingEnemy, gameState.Room);
                return;
            }
        }

        private void TryToRunSomewhere (List<Direction> directions, IEnemy reactingEnemy, Room room)
        {
            foreach (Direction d in directions)
            {
                if (RunInGivenDirection(reactingEnemy, room, d))
                    return;
            }
        }

        private bool RunInGivenDirection (IEnemy reactingEnemy, Room room, Direction direction)
        {
            int currX = reactingEnemy.X;
            int currY = reactingEnemy.Y;

            int newX = currX;
            int newY = currY;

            switch (direction)
            {
                case Direction.Up:
                    newX = currX - 1;
                    if (!CanRunThere(newX, newY, room))
                        return false;
                    break;
                case Direction.Down:
                    newX = currX + 1;
                    if (!CanRunThere(newX, newY, room))
                        return false;
                    break;
                case Direction.Left:
                    newY = currY - 1;
                    if (!CanRunThere(newX, newY, room))
                        return false;
                    break;
                case Direction.Right:
                    newY = currY + 1;
                    if (!CanRunThere(newX, newY, room))
                        return false;
                    break;
            }

            room.Grid[currX, currY].Enemy = null;
            room.Grid[newX, newY].AddEnemy(reactingEnemy);
            reactingEnemy.X = newX;
            reactingEnemy.Y = newY;

            return true;
        }

        private bool CanRunThere(int newX, int newY, Room room)
        {
            if (newX >= room.Height || newY >= room.Width || newX < 0 || newY < 0)
                return false;

            Cell cellToStepOn = room.GetCell(newX, newY);

            if (cellToStepOn.ContainsPlayer || cellToStepOn.Enemy != null || cellToStepOn.isWall)
                return false;

            return true;
        }    
    }
}
