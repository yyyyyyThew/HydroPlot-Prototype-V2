using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypeV2
{
	abstract class Line
	{
		//public bool Linear;
		public string Equation;
		public abstract string Print();
		public abstract string Simplify();
		public double YIntercept { get; private set; }
		public double Gradient { get; private set; }
		public double A { get; private set; }
	}
	class LinearLine : Line
	{
		// y=mx+c
		// where (a,b) is a point on the line and m = dy/dx
		//public string Equation;
		private double YIntercept;
		private double Gradient;
		public LinearLine(double m, double c)
		{
			//Linear = linear;
			Equation = $"y={m}x+{c})";
			YIntercept = c;
			Gradient = m;
		}
		public double Interpolate(double Location1)
		{
			double Location2 = (Location1 * Gradient) + YIntercept;
			return Location2;
		}
		public override string Print()
		{
			return $"y = {Gradient}x + {YIntercept}";
		}
		public override string Simplify()
		{
			//code to simplify the gradient for readablility here
			//int.tryparse() until true on point1, point2 and gradient
			//should create lowest integer form for all 3 values
			// $"y-{Point2}={Gradient}(x-{Point2})";
			return Equation;
		}
	}

	class LogLine : Line
	{
		//public string Equation;
		private double A;
		public double YIntercept;
		public LogLine(double a, double c)
		{
			//Linear = linear;
			A = a;
			YIntercept = c;
			Equation = $"y={A}log(x)+{c}";
		}
		public override string Simplify()
		{
			//simplifies to form of a*log(b) + c
			return Equation;
		}
		public override string Print()
		{
			return $"y = {A}log(x)+ {YIntercept}";
		}
		public double Interpolate()
		{
			return 0;
		}

	}
}
