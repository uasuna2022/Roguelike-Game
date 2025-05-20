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
using RPG_Game.MVC_Pattern.Controller;

namespace RPG_Game.InputHandlers
{
    public class FightHandler : InputHandlerBaseClass
    {
        protected override bool Process(ConsoleKeyInfo consoleKey, GameController controller)
        {
            if (consoleKey.Key != InputKeyConfiguration.Fight)
                return false;

            Player player = controller.GameState.Players[controller.LocalPlayerIdx];
            Room room = controller.GameState.Room;

            if (player.nearbyEnemies.Values.All(enemy => enemy == null))
            {
                //GameDisplayer.Instance.AddNotification("There are no enemies nearby!");
                player.Notify("There are no enemies nearby!");
                return true;
            }
            if (player.LeftHand == null && player.RightHand == null)
            {
                //GameDisplayer.Instance.AddNotification("To fight you need to equip some weapon!");
                player.Notify("To fight you need to equip some weapon!");
                return true;
            }

            // there is an enemy somewhere next to the player and you equipped some weapon
            IEnemy? opponent = null;
            Direction? direction = null;
            //GameDisplayer.Instance.AddNotification("Choose an enemy to attack: ←↑↓→");
            player.Notify("Choose an enemy to attack: ←↑↓→");
            while (opponent == null)
            {
                ConsoleKeyInfo additionalConsoleKey = Console.ReadKey(true);
                switch (additionalConsoleKey.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (player.nearbyEnemies[EnumClasses.Direction.Down] == null)
                        {
                            //GameDisplayer.Instance.AddNotification("There is no enemy in this direction. Try again ←↑↓→");
                            player.Notify("There is no enemy in this direction. Try again ←↑↓→");
                            continue;
                        }
                        else
                        {
                            opponent = player.nearbyEnemies[EnumClasses.Direction.Down];
                            direction = Direction.Down;
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        if (player.nearbyEnemies[EnumClasses.Direction.Up] == null)
                        {
                            //GameDisplayer.Instance.AddNotification("There is no enemy in this direction. Try again ←↑↓→");
                            player.Notify("There is no enemy in this direction. Try again ←↑↓→");
                            continue;
                        }
                        else
                        {
                            opponent = player.nearbyEnemies[EnumClasses.Direction.Up];
                            direction = Direction.Up;
                            break;
                        }
                    case ConsoleKey.LeftArrow:
                        if (player.nearbyEnemies[EnumClasses.Direction.Left] == null)
                        {
                            //GameDisplayer.Instance.AddNotification("There is no enemy in this direction. Try again ←↑↓→");
                            player.Notify("There is no enemy in this direction. Try again ←↑↓→");
                            continue;
                        }
                        else
                        {
                            opponent = player.nearbyEnemies[EnumClasses.Direction.Left];
                            direction = Direction.Left;
                            break;
                        }
                    case ConsoleKey.RightArrow:
                        if (player.nearbyEnemies[EnumClasses.Direction.Right] == null)
                        {
                            //GameDisplayer.Instance.AddNotification("There is no enemy in this direction. Try again ←↑↓→");
                            player.Notify("There is no enemy in this direction. Try again ←↑↓→");
                            continue;
                        }
                        else
                        {
                            opponent = player.nearbyEnemies[EnumClasses.Direction.Right];
                            direction = Direction.Right;
                            break;
                        }
                    default:
                        //GameDisplayer.Instance.AddNotification("Invalid input. Try again ←↑↓→");
                        player.Notify("Invalid input. Try again ←↑↓→");
                        continue;
                }
            }

            //GameDisplayer.Instance.AddNotification("Choose an attack type: N - normal, S - stealth, M - magic");
            player.Notify("Choose an attack type: N - normal, S - stealth, M - magic");
            bool flag = false;
            int totalAttackHP = 0;
            int totalDefenseHP = 0;

            while (!flag)
            {
                char attackCharInput = char.ToUpper(Console.ReadKey(true).KeyChar);
                switch (attackCharInput)
                {
                    case 'N':
                        (totalAttackHP, totalDefenseHP) = GetTotalAttackHPAndDefenseHP(player, TypeOfAttack.Normal);
                        flag = true;
                        break;
                    case 'S':
                        (totalAttackHP, totalDefenseHP) = GetTotalAttackHPAndDefenseHP(player, TypeOfAttack.Stealth);
                        flag = true;
                        break;
                    case 'M':
                        (totalAttackHP, totalDefenseHP) = GetTotalAttackHPAndDefenseHP(player, TypeOfAttack.Magic);
                        flag = true;
                        break;
                    default:
                        //GameDisplayer.Instance.AddNotification("Invalid input. Try again: N - normal, S - stealth, M - magic");
                        player.Notify("Invalid input. Try again: N - normal, S - stealth, M - magic");
                        break;
                }
            }

            int damageToEnemy = Math.Max(0, totalAttackHP - opponent.Armor);
            opponent.Health -= damageToEnemy;
            //GameDisplayer.Instance.AddNotification($"{opponent.Name}'s health reduced by {damageToEnemy} HP!" +
            //$" (used armor: {Math.Min(opponent.Armor, totalAttackHP)} / {opponent.Armor})");
            player.Notify($"{opponent.Name}'s health reduced by {damageToEnemy} HP!" 
                + $" (used armor: {Math.Min(opponent.Armor, totalAttackHP)} / {opponent.Armor})");
            //GameDisplayer.Instance.DrawNearbyEnemies(player);
            player.Refresh();

            if (opponent.Health <= 0)
            {
                //GameDisplayer.Instance.AddNotification($"{opponent.Name} defeated!");
                player.Notify($"{opponent.Name} defeated!");
                RemoveEnemyFromRoom(room, player, direction);
                //player.UpdateNearbyEnemies(game.room);
                /*
                GameDisplayer.Instance.DrawNearbyEnemies(game.player);
                GameDisplayer.Instance.UpdateMapCells(
                        game.player.X + (direction == Direction.Down ? 1 : direction == Direction.Up ? -1 : 0),
                        game.player.Y + (direction == Direction.Right ? 1 : direction == Direction.Left ? -1 : 0),
                        game.player.X + (direction == Direction.Down ? 1 : direction == Direction.Up ? -1 : 0),
                        game.player.Y + (direction == Direction.Right ? 1 : direction == Direction.Left ? -1 : 0),
                        game.room, game.player);
                */
                player.Refresh();
                return true;
            }

            int counterAttackDamage = Math.Max(0, opponent.Damage - totalDefenseHP);
            player.Health -= counterAttackDamage;
            GameDisplayer.Instance.AddNotification($"{opponent.Name} counterattacks you and deals {counterAttackDamage} HP! " +
                $"(blocked: {Math.Min(opponent.Damage, totalDefenseHP)} / {totalDefenseHP})");
            player.Notify($"{opponent.Name} counterattacks you and deals {counterAttackDamage} HP! " +
                $"(blocked: {Math.Min(opponent.Damage, totalDefenseHP)} / {totalDefenseHP})");
            if (player.Health <= 0)
            {
                player.Health = 0;
                player.Refresh();
                player.Notify("YOU DIED! GAME OVER!");
                controller.RequestQuit();
                /*
                GameDisplayer.Instance.DrawPlayerStats(player);
                Console.ForegroundColor = ConsoleColor.DarkRed;
                GameDisplayer.Instance.AddNotification("YOU DIED! GAME OVER!");
                Console.ResetColor();
                game.gameIsRunning = false;
                GameDisplayer.Instance.AddNotification("Exiting game...");
                */
                Console.ReadKey();
            }
            
            return true;
        }

        private (int, int) GetTotalAttackHPAndDefenseHP(Player player, TypeOfAttack typeOfAttack)
        {
            AttackVisitorBaseClass? attackVisitor = null;
            DefenseVisitorBaseClass? defenseVisitor = null;
            int totalAttackHP = 0;
            int totalDefenseHP = 0;
            if (player.LeftHand != null)
            {
                switch (typeOfAttack)
                {
                    case TypeOfAttack.Normal:
                        attackVisitor = new NormalAttackVisitor(player.LeftHand, player);
                        defenseVisitor = new NormalDefenseVisitor(player.LeftHand, player);
                        break;
                    case TypeOfAttack.Stealth:
                        attackVisitor = new StealthAttackVisitor(player.LeftHand, player);
                        defenseVisitor = new StealthDefenseVisitor(player.LeftHand, player);
                        break;
                    case TypeOfAttack.Magic:
                        attackVisitor = new MagicAttackVisitor(player.LeftHand, player);
                        defenseVisitor = new MagicDefenseVisitor(player.LeftHand, player);
                        break;
                    default:
                        throw new NotImplementedException(); // will never happen
                }

                totalAttackHP += player.LeftHand.Accept(attackVisitor);
                totalDefenseHP += player.LeftHand.AcceptDefense(defenseVisitor);
            }

            if (player.RightHand != null && player.RightHand!.IsTwoHanded == false)
            {
                switch (typeOfAttack)
                {
                    case TypeOfAttack.Normal:
                        attackVisitor = new NormalAttackVisitor(player.RightHand, player);
                        defenseVisitor = new NormalDefenseVisitor(player.RightHand, player);
                        break;
                    case TypeOfAttack.Stealth:
                        attackVisitor = new StealthAttackVisitor(player.RightHand, player);
                        defenseVisitor = new StealthDefenseVisitor(player.RightHand, player);
                        break;
                    case TypeOfAttack.Magic:
                        attackVisitor = new MagicAttackVisitor(player.RightHand, player);
                        defenseVisitor = new MagicDefenseVisitor(player.RightHand, player);
                        break;
                    default:
                        throw new NotImplementedException();
                }

                totalAttackHP += player.RightHand.Accept(attackVisitor);
                totalDefenseHP += player.RightHand.AcceptDefense(defenseVisitor);
            }

            return (totalAttackHP, totalDefenseHP);
        }

        private void RemoveEnemyFromRoom(Room room, Player player, Direction? direction)
        {
            int newX = player.X; 
            int newY = player.Y;
            switch (direction)
            {
                case Direction.Up: newX--; break;
                case Direction.Down: newX++; break;
                case Direction.Left: newY--; break;
                case Direction.Right: newY++; break;
            }
            if (newX >= 0 && newY >= 0 && newX < room.Height && newY < room.Width)
                room.Grid[newX, newY].Enemy = null;
        }
    }
}
