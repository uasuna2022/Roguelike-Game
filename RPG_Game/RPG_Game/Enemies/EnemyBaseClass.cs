using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_Game.Interfaces;
using RPG_Game.MVC_Pattern.Model;

namespace RPG_Game.Enemies
{
    public abstract class EnemyBaseClass: IEnemy
    {
        public abstract string Name { get; }
        public abstract char Symbol { get; }
        public virtual ConsoleColor Color => ConsoleColor.Red;
        public abstract int Health { get; set; }
        public abstract int MaxHealth { get; }
        public abstract int Damage { get; }
        public abstract int Armor { get; }

        public int X { get; set; }
        public int Y { get; set; }
        public IStrategy CurrentStrategy { get; set; }
        protected EnemyBaseClass(int x, int y, IStrategy strategy)
        {
            X = x;
            Y = y;
            CurrentStrategy = strategy;
        } 
        public virtual (Player, int) FindNearestPlayer(GameState gameState)
        {
            Player nearestPlayer = gameState.Players[0];
            int distance = Math.Abs(Y - nearestPlayer.Y) + Math.Abs(X - nearestPlayer.X);
            foreach (Player player in gameState.Players)
            {
                if (distance > Math.Abs(Y - player.Y) + Math.Abs(X - player.X))
                {
                    nearestPlayer = player;
                    distance = Math.Abs(Y - player.Y) + Math.Abs(X - player.X);
                }
            }

            return (nearestPlayer, distance);
        }
    }
}
