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

            if (chasedPlayer.X == reactingEnemy.X && chasedPlayer.Y < reactingEnemy.Y) // 1) enemy's direction is left
            {
                Cell newCell = gameState.Room.GetCell(reactingEnemy.X, reactingEnemy.Y - 1);


                if (newCell.isWall || newCell.Enemy != null || newCell.ContainsPlayer) // if an enemy can't make a move there, it does nothing
                    return;

                gameState.Room.Grid[reactingEnemy.X, reactingEnemy.Y].Enemy = null;
                newCell.Enemy = reactingEnemy;
                reactingEnemy.Y--;

                return;
            }

            // TODO - other 7 occasions

        }
    }
}
