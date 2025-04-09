using RPG_Game.EnumClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.InputHandlers
{
    public class MoveHandler: InputHandlerBaseClass
    {
        protected override bool Process(ConsoleKeyInfo consoleKey, Game game)
        {
            Direction? direction = DirectionFromKey(consoleKey);
            if (direction == null)
                return false;
            if (game.player.newIsValidMove(direction, game.room))
            {
                int oldX = game.player.X;
                int oldY = game.player.Y;
                game.player.newMove(direction, game.room);
                game.player.NotifyObservers();
                GameDisplayer.Instance.DrawCellStats(game.room.GetCell(game.player.X, game.player.Y));
                GameDisplayer.Instance.UpdateMapCells(oldX, oldY, game.player.X, game.player.Y, game.room, game.player);
                GameDisplayer.Instance.DrawGameStats(game.player);
                GameDisplayer.Instance.DrawActivePotionEffects(game.player);
                return true;
            }

            else
            {
                GameDisplayer.Instance.AddNotification("You can't go that way!");
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
