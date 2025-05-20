using RPG_Game.EnumClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_Game.MVC_Pattern.Controller;

namespace RPG_Game.InputHandlers
{
    public class MoveHandler: InputHandlerBaseClass
    {
        protected override bool Process(ConsoleKeyInfo consoleKey, GameController controller)
        {
            Direction? direction = DirectionFromKey(consoleKey);
            if (direction == null)
                return false;

            Player player = controller.GameState.Players[controller.LocalPlayerIdx];
            Room room = controller.GameState.Room;

            if (player.newIsValidMove(direction, room))
            {
                //int oldX = player.X;
                //int oldY = player.Y;
                player.newMove(direction, room);
                player.NotifyObservers();
                controller.GameState.IncrementStepCounter(); 
                player.UpdateNearbyEnemies(room);
                /*
                GameDisplayer.Instance.DrawCellStats(room.GetCell(player.X, player.Y));
                GameDisplayer.Instance.UpdateMapCells(oldX, oldY, player.X, player.Y, room, player);
                GameDisplayer.Instance.DrawGameStats(player);
                GameDisplayer.Instance.DrawActivePotionEffects(player);
                GameDisplayer.Instance.DrawNearbyEnemies(player);
                */
                player.Refresh();
                return true;
            }

            else
            {
                //GameDisplayer.Instance.AddNotification("You can't go that way!");
                player.Notify("You can't go that way!");
                return true;
            } 
        }
        private Direction? DirectionFromKey(ConsoleKeyInfo consoleKey)
        {
            if (consoleKey.Key == InputKeyConfiguration.MoveUp)
                return Direction.Up;
            if (consoleKey.Key == InputKeyConfiguration.MoveDown)
                return Direction.Down;
            if (consoleKey.Key == InputKeyConfiguration.MoveLeft)
                return Direction.Left;
            if (consoleKey.Key == InputKeyConfiguration.MoveRight)
                return Direction.Right;
            return null;
        }
    }
}
