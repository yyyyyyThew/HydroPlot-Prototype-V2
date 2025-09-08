using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Interpolation;
using NPOI;

namespace PrototypeV2
{
	class Table
	{
		public LogLine LogBestFit;
		public LinearLine LinearBestFit;
		private string DataLabel;
		public string XLabel { get; private set; }
		public string YLabel { get; private set; }
		public double R2 { get; private set; }
		public List<Coordinate> Data = new List<Coordinate>();
		//private List<Coordinate> LocalColumn = new List<Coordinate>();
		public bool isLinear;
		//public Line BestFit;
		public string Regress(List<Coordinate> DataIn)
		{
			//defaults to a flat line that passes through (0,0)
			double Gradient = 0;
			double a = 0;
			double b = 0;
			double sumOfX = 0;
			double sumOfY = 0;
			double sumOfXSq = 0;
			double sumOfYSq = 0;
			double coDeviateSum = 0;
			double rsquared;
			double YIntercept;
			//calculate line of best fit
			//if there is an error, just use default values and throw an error

			for (var i = 0; i < DataIn.Count; i++)
			{
				var x = Data[i].X;
				var y = Data[i].Y;
				coDeviateSum += x * y;
				sumOfX += x;
				sumOfY += y;
				sumOfXSq += x * x;
				sumOfYSq += y * y;
			}
			int count = Data.Count;
			double ssX = sumOfXSq - ((sumOfX * sumOfX) / count);
			double ssY = sumOfYSq - ((sumOfY * sumOfY) / count);

			double rNumerator = (count * coDeviateSum) - (sumOfX * sumOfY);
			double rDenominator = (count * sumOfXSq - (sumOfX * sumOfX)) * (count * sumOfYSq - (sumOfY * sumOfY));
			double sCo = coDeviateSum - ((sumOfX * sumOfY) / count);

			double meanX = sumOfX / count;
			double meanY = sumOfY / count;
			double r = rNumerator / Math.Sqrt(rDenominator);

			rsquared = r * r;
			YIntercept = meanY - ((sCo / ssX) * meanX);
			Gradient = sCo / ssX;
			//if less than ~95, call LogLinear()
			if (rsquared > 0.5)
			{
				LinearLine BestFit = new LinearLine(Gradient, YIntercept);
				return BestFit.Equation;
			}
			else
			{
				RegressLog(this.Data);
				return "";
			}
		}
		public string RegressLog(List<Coordinate> DataIn)
		{
			// Logarithmic Regression
			double sumLogX = 0, sumLogXSq = 0, sumLogXY = 0;
			double sumY = 0;
			int count = Data.Count;

			foreach (var point in Data)
			{
				var x = point.X;
				var y = point.Y;
				var logX = Math.Log(x);  // Taking the logarithm of x

				sumLogX += logX;
				sumLogXSq += logX * logX;
				sumLogXY += logX * y;
				sumY += y;
			}

			double aNumerator = count * sumLogXY - sumLogX * sumY;
			double aDenominator = count * sumLogXSq - sumLogX * sumLogX;
			double a = aNumerator / aDenominator;

			double b = (sumY - a * sumLogX) / count;  // Intercept
			LogLine BestFit = new LogLine(a, b);
			return BestFit.Equation;
		}
		public List<Coordinate> LogLinear()
		{
			//x -> log(x) and checks if the data is linearised.
			//if the data is close enough to linear (r~=0.95) then it creates a LogLine object
			//else, it throws a fun little warning
			List<Coordinate> LinearisedData = DeepCopy(this.Data).Data;
			for (int items = 0; items < LinearisedData.Count; items++)
			{
				LinearisedData[items].X = Math.Log10(LinearisedData[items].X);
			}
			isLinear = false;
			return LinearisedData;
			//Regress(LinearisedData);
		}

		public string Print(int Index)
		{
			if (Data[Index] is null)
			{
				throw new Exception("Index refers to a null coordinate");
			}
			else
			{
				return Convert.ToString($"({Data[Index].X}, {Data[Index].Y})");
			}
		}
		//allows for everything in an object of Table class to be copied to a new object of a subclass
		//prevents issues where using List = oldList causes oldList to be edited as well as List
		public Table DeepCopy(List<Coordinate> oldData)
		{
			return new Table(oldData, this.DataLabel, this.XLabel, this.YLabel);
		}

		// Method to merge sort data (has 2 overloads, required to do the whole thang ong)
		public List<Coordinate> Sort(List<Coordinate> Input)
		{
			if (Input.Count <= 1)
				return Input; // Base case: return the list if it has only one element

			// Divide the unsorted list into two halves
			List<Coordinate> left = new List<Coordinate>();
			List<Coordinate> right = new List<Coordinate>();

			int middle = Input.Count / 2;
			for (int i = 0; i < middle; i++) // Split the unsorted list into left
			{
				left.Add(Input[i]);
			}
			for (int i = middle; i < Input.Count; i++) // Split the unsorted list into right
			{
				right.Add(Input[i]);
			}

			// Recursively perform Merge Sort on the divided lists
			left = Sort(left);
			right = Sort(right);

			// Merge the sorted halves
			return Sort(left, right);
			//return Input;
		}
		public List<Coordinate> Sort(List<Coordinate> left, List<Coordinate> right)
		{
			List<Coordinate> result = new List<Coordinate>();

			// Compares elements from both lists and merge them in sorted order
			while (left.Count > 0 || right.Count > 0)
			{
				if (left.Count > 0 && right.Count > 0)
				{
					if (left.First().X <= right.First().X) // Compare the first elements of both lists
					{
						result.Add(left.First());
						left.Remove(left.First()); // Remove the added element from the list
					}
					else
					{
						result.Add(right.First());
						right.Remove(right.First()); // Remove the added element from the list
					}
				}
				else if (left.Count > 0)
				{
					result.Add(left.First());
					left.Remove(left.First());
				}
				else if (right.Count > 0)
				{
					result.Add(right.First());
					right.Remove(right.First());
				}
			}
			return result;
		}

		public Table(List<Coordinate> data, string datalabel, string xlabel, string ylabel)
		{
			Data = data;
			DataLabel = datalabel;
			XLabel = xlabel;
			YLabel = ylabel;
		}
	}
}
