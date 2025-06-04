using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_Game.AttackVisitors;
using RPG_Game.DefenseVisitors;
using RPG_Game.EnumClasses;
using RPG_Game.InputHandlers;
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
        public virtual void ReactOnMove(GameState gameState) => this.CurrentStrategy.React(this, gameState);
        public virtual bool AttackPlayer(Player player) // false -> a player died, true -> a player survivedthe attack
        {
            DefenseVisitorBaseClass? defenseVisitor = null;
            int totalDefenseHP = 0;
            if (player.LeftHand != null)
            { 
                defenseVisitor = new NormalDefenseVisitor(player.LeftHand, player);
                totalDefenseHP += player.LeftHand.AcceptDefense(defenseVisitor);
            }

            if (player.RightHand != null && player.RightHand!.IsTwoHanded == false)
            {
                defenseVisitor = new NormalDefenseVisitor(player.RightHand, player);
                totalDefenseHP += player.RightHand.AcceptDefense(defenseVisitor);
            }

            int totalDamage = Math.Max(0, this.Damage - totalDefenseHP);
            player.Health = (player.Health - totalDamage >= 0) ? player.Health - totalDamage : 0;
            player.Notify($"Offensive enemy {this.Name} attacks you and deals {totalDamage} HP! " +
                $"(blocked: {Math.Min(this.Damage, totalDefenseHP)} / {totalDefenseHP})");
            player.Refresh();
            if (player.Health == 0)
            {
                return false;
            }

            else return true;
        }
    }
}
