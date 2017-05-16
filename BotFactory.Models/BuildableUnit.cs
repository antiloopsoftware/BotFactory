using BotFactory.Common.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotFactory.Models
{
	public abstract class BuildableUnit : IBuidableUnit
	{
		private double _builTime = 5;
        private string _model;

		public BuildableUnit()
		{
			
		}

		public BuildableUnit(double buildTime)
		{
			
		}

		public double BuildTime
		{
			get
			{
                return _builTime;
			}
			set
			{
                _builTime = value;
			}
		}

		public string Model
		{
			get
			{
                return _model;
			}
			set
			{
                _model = value;
			}
		}
	}
}
