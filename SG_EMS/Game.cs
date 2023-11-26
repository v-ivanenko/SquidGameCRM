using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_EMS
{
    public abstract class Game
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime Start { get; private set; }

        public DateTime End { get; private set; }

        protected Game(string name, string description)
        {
            Name = name;
            Description = description;

        }

        // This method will be overridden to provide specific start game behaviors
        public virtual string StartGame()
        {
            Start = DateTime.Now;
            return $"Starting the game: {Name}.";

        }

        public virtual bool isGameOver()
        {
            return true;
        }
    }

    public class RedLightGreenLight : Game
    {
        public RedLightGreenLight() : base("Red Light, Green Light", "Stop when it's red, run when it's green.")
        {
        }

        public override string StartGame()
        {
            return $"{base.StartGame()} Remember, don't move when it's red light!";
        }
    }

    public class TugOfWar : Game
    {
        public TugOfWar() : base("Tug of War", "Pull the rope to your side to win.")
        {
        }

        public override string StartGame()
        {
            return $"{base.StartGame()} Use all your strength to pull the rope!";
        }
    }



}
