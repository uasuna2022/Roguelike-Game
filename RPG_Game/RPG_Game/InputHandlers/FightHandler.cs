using RPG_Game.AttackVisitors;
using RPG_Game.DefenseVisitors;
using RPG_Game.EnumClasses;
using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.InputHandlers
{
    public class FightHandler : InputHandlerBaseClass
    {
        protected override bool Process(ConsoleKeyInfo consoleKey, Game game)
        {
            if (consoleKey.Key != InputKeyConfiguration.Fight)
                return false;

            if (game.player.nearbyEnemies.Values.All(enemy => enemy == null))
            {
                GameDisplayer.Instance.AddNotification("There are no enemies nearby!");
                return true;
            }
            if (game.player.LeftHand == null && game.player.RightHand == null)
            {
                GameDisplayer.Instance.AddNotification("To fight you need to equip some weapon!");
                return true;
            }

            // there is an enemy somewhere next to the player and you equipped some weapon
            IEnemy? opponent = null;
            Direction? direction = null;
            GameDisplayer.Instance.AddNotification("Choose an enemy to attack: ←↑↓→");
            while (opponent == null)
            {
                ConsoleKeyInfo additionalConsoleKey = Console.ReadKey(true);
                switch (additionalConsoleKey.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (game.player.nearbyEnemies[EnumClasses.Direction.Down] == null)
                        {
                            GameDisplayer.Instance.AddNotification("There is no enemy in this direction. Try again ←↑↓→");
                            continue;
                        }
                        else
                        {
                            opponent = game.player.nearbyEnemies[EnumClasses.Direction.Down];
                            direction = Direction.Down;
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        if (game.player.nearbyEnemies[EnumClasses.Direction.Up] == null)
                        {
                            GameDisplayer.Instance.AddNotification("There is no enemy in this direction. Try again ←↑↓→");
                            continue;
                        }
                        else
                        {
                            opponent = game.player.nearbyEnemies[EnumClasses.Direction.Up];
                            direction = Direction.Up;
                            break;
                        }
                    case ConsoleKey.LeftArrow:
                        if (game.player.nearbyEnemies[EnumClasses.Direction.Left] == null)
                        {
                            GameDisplayer.Instance.AddNotification("There is no enemy in this direction. Try again ←↑↓→");
                            continue;
                        }
                        else
                        {
                            opponent = game.player.nearbyEnemies[EnumClasses.Direction.Left];
                            direction = Direction.Left;
                            break;
                        }
                    case ConsoleKey.RightArrow:
                        if (game.player.nearbyEnemies[EnumClasses.Direction.Right] == null)
                        {
                            GameDisplayer.Instance.AddNotification("There is no enemy in this direction. Try again ←↑↓→");
                            continue;
                        }
                        else
                        {
                            opponent = game.player.nearbyEnemies[EnumClasses.Direction.Right];
                            direction = Direction.Right;
                            break;
                        }
                    default:
                        GameDisplayer.Instance.AddNotification("Invalid input. Try again ←↑↓→");
                        continue;
                }
            }

            GameDisplayer.Instance.AddNotification("Choose an attack type: N - normal, S - stealth, M - magic");
            bool flag = false;
            int totalAttackHP = 0;
            int totalDefenseHP = 0;

            while (!flag)
            {
                char attackCharInput = char.ToUpper(Console.ReadKey(true).KeyChar);
                switch (attackCharInput)
                {
                    case 'N':
                        (totalAttackHP, totalDefenseHP) = GetTotalAttackHPAndDefenseHP(game, TypeOfAttack.Normal);
                        flag = true;
                        break;
                    case 'S':
                        (totalAttackHP, totalDefenseHP) = GetTotalAttackHPAndDefenseHP(game, TypeOfAttack.Stealth);
                        flag = true;
                        break;
                    case 'M':
                        (totalAttackHP, totalDefenseHP) = GetTotalAttackHPAndDefenseHP(game, TypeOfAttack.Magic);
                        flag = true;
                        break;
                    default:
                        GameDisplayer.Instance.AddNotification("Invalid input. Try again: N - normal, S - stealth, M - magic");
                        break;
                }
            }

            int damageToEnemy = Math.Max(0, totalAttackHP - opponent.Armor);
            opponent.Health -= damageToEnemy;
            GameDisplayer.Instance.AddNotification($"{opponent.Name}'s health reduced by {damageToEnemy} HP!" +
                $" (used armor: {Math.Min(opponent.Armor, totalAttackHP)} / {opponent.Armor})");
            GameDisplayer.Instance.DrawNearbyEnemies(game.player);

            if (opponent.Health <= 0)
            {
                GameDisplayer.Instance.AddNotification($"{opponent.Name} defeated!");
                RemoveEnemyFromRoom(game, direction);
                game.player.UpdateNearbyEnemies(game.room);
                GameDisplayer.Instance.DrawNearbyEnemies(game.player);
                GameDisplayer.Instance.UpdateMapCells(
                        game.player.X + (direction == Direction.Down ? 1 : direction == Direction.Up ? -1 : 0),
                        game.player.Y + (direction == Direction.Right ? 1 : direction == Direction.Left ? -1 : 0),
                        game.player.X + (direction == Direction.Down ? 1 : direction == Direction.Up ? -1 : 0),
                        game.player.Y + (direction == Direction.Right ? 1 : direction == Direction.Left ? -1 : 0),
                        game.room, game.player);
                return true;
            }

            int counterAttackDamage = Math.Max(0, opponent.Damage - totalDefenseHP);
            game.player.Health -= counterAttackDamage;
            GameDisplayer.Instance.AddNotification($"{opponent.Name} counterattacks you and deals {counterAttackDamage} HP! " +
                $"(blocked: {Math.Min(opponent.Damage, totalDefenseHP)} / {totalDefenseHP})");
            if (game.player.Health <= 0)
            {
                game.player.Health = 0;
                GameDisplayer.Instance.DrawPlayerStats(game.player);
                Console.ForegroundColor = ConsoleColor.DarkRed;
                GameDisplayer.Instance.AddNotification("YOU DIED! GAME OVER!");
                Console.ResetColor();
                game.gameIsRunning = false;
                GameDisplayer.Instance.AddNotification("Exiting game...");
                Console.ReadKey();
            }
            
            return true;
        }

        private (int, int) GetTotalAttackHPAndDefenseHP(Game game, TypeOfAttack typeOfAttack)
        {
            AttackVisitorBaseClass? attackVisitor = null;
            DefenseVisitorBaseClass? defenseVisitor = null;
            int totalAttackHP = 0;
            int totalDefenseHP = 0;
            if (game.player.LeftHand != null)
            {
                switch (typeOfAttack)
                {
                    case TypeOfAttack.Normal:
                        attackVisitor = new NormalAttackVisitor(game.player.LeftHand, game.player);
                        defenseVisitor = new NormalDefenseVisitor(game.player.LeftHand, game.player);
                        break;
                    case TypeOfAttack.Stealth:
                        attackVisitor = new StealthAttackVisitor(game.player.LeftHand, game.player);
                        defenseVisitor = new StealthDefenseVisitor(game.player.LeftHand, game.player);
                        break;
                    case TypeOfAttack.Magic:
                        attackVisitor = new MagicAttackVisitor(game.player.LeftHand, game.player);
                        defenseVisitor = new MagicDefenseVisitor(game.player.LeftHand, game.player);
                        break;
                    default:
                        throw new NotImplementedException(); // will never happen
                }

                totalAttackHP += game.player.LeftHand.Accept(attackVisitor);
                totalDefenseHP += game.player.LeftHand.AcceptDefense(defenseVisitor);
            }

            if (game.player.RightHand != null && game.player.RightHand!.IsTwoHanded == false)
            {
                switch (typeOfAttack)
                {
                    case TypeOfAttack.Normal:
                        attackVisitor = new NormalAttackVisitor(game.player.RightHand, game.player);
                        defenseVisitor = new NormalDefenseVisitor(game.player.RightHand, game.player);
                        break;
                    case TypeOfAttack.Stealth:
                        attackVisitor = new StealthAttackVisitor(game.player.RightHand, game.player);
                        defenseVisitor = new StealthDefenseVisitor(game.player.RightHand, game.player);
                        break;
                    case TypeOfAttack.Magic:
                        attackVisitor = new MagicAttackVisitor(game.player.RightHand, game.player);
                        defenseVisitor = new MagicDefenseVisitor(game.player.RightHand, game.player);
                        break;
                    default:
                        throw new NotImplementedException();
                }

                totalAttackHP += game.player.RightHand.Accept(attackVisitor);
                totalDefenseHP += game.player.RightHand.AcceptDefense(defenseVisitor);
            }

            return (totalAttackHP, totalDefenseHP);
        }

        private void RemoveEnemyFromRoom(Game game, Direction? direction)
        {
            int newX = game.player.X; 
            int newY = game.player.Y;
            switch (direction)
            {
                case Direction.Up: newX--; break;
                case Direction.Down: newX++; break;
                case Direction.Left: newY--; break;
                case Direction.Right: newY++; break;
            }
            if (newX >= 0 && newY >= 0 && newX < game.room.Height && newY < game.room.Width)
                game.room.Grid[newX, newY].Enemy = null;
        }
    }
}
