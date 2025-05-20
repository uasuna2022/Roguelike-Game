using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game.MVC_Pattern.Model
{
    public class GameState
    {
        public List<Player> Players { get; }
        public Room Room { get; }
        public int Version { get; }
        public int StepCounter { get; set; }
        public GameState(List<Player> players, Room room, int version)
        {
            Players = players;
            Room = room;
            Version = version;
        }

        public event EventHandler? StateChanged;
        public event Action<string>? NotificationAdded;
        public void InvokeNotificationAdded(string message)
        {
            NotificationAdded?.Invoke(message);
        }
        public void InvokeStateChanged()
        {
            StateChanged?.Invoke(this, EventArgs.Empty);
        }
        public void IncrementStepCounter()
        {
            StepCounter++;
            InvokeStateChanged(); // zmienię to na inny event później
        }
    }
}
