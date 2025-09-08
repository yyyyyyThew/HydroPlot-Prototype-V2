using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype_V2
{
	class Coordinate
	{
		public double X { get; set; }
		public double Y { get; set; }
		public Coordinate(double x, double y, double frequency = 1)
		{
			X = x;
			Y = y;
		}
	}
}
