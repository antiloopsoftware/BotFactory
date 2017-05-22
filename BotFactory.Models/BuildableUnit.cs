using BotFactory.Common.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotFactory.Models
{
	public abstract class BuildableUnit : IBuidableUnit
	{
		
		public BuildableUnit(){	}
        
        public BuildableUnit(string model = "Sans nom", double buildTime = 5.0)
        {
            Model = model;
            BuildTime = buildTime;
        }

        public double BuildTime { get; set; }
	
		public string Model { get; set; }
    }
}
