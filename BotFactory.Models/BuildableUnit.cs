using BotFactory.Common.Interface;

namespace BotFactory.Models
{
	public abstract class BuildableUnit : IBuidableUnit
	{
        public double BuildTime { get; set; }

        public string Model { get; set; }

        public BuildableUnit(){	}
        
        public BuildableUnit(string model = "SANS NOM", double buildTime = 5.0)
        {
            Model = model;
            BuildTime = buildTime;
        }
    }
}
