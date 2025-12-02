using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using PrototypeV2;

namespace Prototype_V2
{
	class Spreadsheet
	{
		public PrototypeV2.Table LocalColumn;
		public string xtitle;
		public string ytitle;
		public List<Coordinate> DataOut = new List<Coordinate>();
		StreamReader reader;
		public Spreadsheet(string path)
		{
			try
			{
				reader = new StreamReader(@"path");

			}
			catch (Exception)
			{
				MessageBox.Show("Can't open spreadsheet", "Error");
			}
		}
		public void ReadSpreadsheet()
		{
			while (!reader.EndOfStream)
			{
				var line = reader.ReadLine();
				var values = line.Split(';');
				if (double.TryParse(values[0], out double i) == true && double.TryParse(values[0], out double j))
				{
					double xvalue = Convert.ToDouble(values[0]);
					double yvalue = Convert.ToDouble(values[1]);
					this.DataOut.Add(new Coordinate(xvalue, yvalue));

				}
			}
			LocalColumn = new PrototypeV2.Table(DataOut, $"Spreadsheet Data For {xtitle} and {ytitle}", xtitle, ytitle);
		}
	}
}
