using Microsoft.Data.SqlClient;
using Prototype_V2;
using ScottPlot;
namespace PrototypeV2
{
	public partial class Home : Form
	{
		Excel CurrentData;
		Spreadsheet CSVCurrentData;
		Table UserData;

		//Line BestFit;
		double[] x, y;
		string XLabel, YLabel;
		string a;
		private bool ConnectPoints;
		private bool UseExcel;
		private bool UseCSV;
		private bool RunningChart;
		string fileStyle;
		SqlConnection _connection;
		public Home()
		{
			InitializeComponent();
			string connectionString = @Settings.GetConnection("SG20");
			MessageBox.Show(connectionString);
			_connection = Connect(connectionString);
			//set the filter for the file pick dialogue
			//description of filter goes in the brackets
			//after the pipe goes the actual file extensions, followed by a semi colon.
			FpkExcel.Filter = "Spreadsheet Files(*.CSV, *.XLSX)|*.CSV;*.XLSX|All files (*.*)|*.*";
			//Disable buttons that will try to access data that isn't loaded
			BtnRegress.Enabled = false;
			BtnSort.Enabled = false;
			BtnPrint.Enabled = false;
			BtnView.Enabled = false;
			chk_ConnectPoints.Enabled = false;
			//sets class variable default values
			RunningChart = false;
			ConnectPoints = false;
			UseExcel = true;
			UseCSV = true;
		}
		private bool goPaint;
		
		static public double[] toCoordinate(string alpha, double[] dataX, double[] dataY)
		{
		try
			{
				//turns a string formatted regression line into coordinates
				//returns the limits where interpolation is possible
				Coordinate max;
				Coordinate min;
				string subString;
				int eqIndex = alpha.IndexOf('=');//index where '=' appears
				int multIndex = alpha.IndexOf('x');//and 'x'
				double m;
				if (eqIndex == -1 || multIndex == -1)
				{
					MessageBox.Show("Not in correct Format", "Error");
					//throw new Exception("Invalid format");
				}
				else
				{

					subString = alpha.Substring(eqIndex + 1, multIndex - (eqIndex + 1)).Trim();
					m = double.Parse(subString);
					int plusIndex = alpha.IndexOf("+");//index where '+' appears

					double c;
					if (plusIndex == -1)
					{
						//throw new Exception("Invalid format");
					}
					else
					{
						c = double.Parse(subString);
					}
					subString = alpha.Substring(plusIndex);
					c = double.Parse(subString);

					//turns m and c into a straight line
					int index;
					if (dataX.Length <= dataY.Length)
					{
						index = dataX.Length;
					}
					else if (dataX.Length >= dataY.Length)
					{
						index = dataY.Length;
					}
					else
					{
						index = dataX.Length;
					}

					List<Coordinate> list = new List<Coordinate>();
					for (int i = 0; i < index - 1; i++)
					{
						list.Add(new Coordinate(dataX[i], dataY[i]));
					}
					list = Table.Sort(list);

					double firstXVar = list.First().X;
					double firstYVar = m * firstXVar + c;
					double lastXVar = list.Last().X;
					double lastYVar = m * lastXVar + c;
					return new double[] { firstXVar, firstYVar, lastXVar, lastYVar };
				}
			}
			catch
			{
				//returns the origin twice if the equation is not in correct format
				MessageBox.Show("Some input values were not real numbers");
			}
			return new double[] { 0, 0, 0, 0 };

		}
		static private SqlConnection Connect(string path)
		{
			try
			{
				// $@"Server={path};TrustServerCertificate=True;Trusted_Connection=True;Initial Catalog=SystemTrackerDB;"
				SqlConnection myConn = new SqlConnection(path);
				myConn.Open();
				MessageBox.Show("Database Connnection Established");
				myConn.Close();
				return myConn;

			}
			catch (SqlException ex)
			{
				try
				{
					SqlConnection myConn = new SqlConnection(path);
					myConn.Open();
					var createDB = myConn.CreateCommand();
					createDB.CommandText = "CREATE DATABASE [SystemTrackerDB]";
					createDB.ExecuteNonQuery();
					var UseDB = myConn.CreateCommand();
					UseDB.CommandText = "USE [SystemTrackerDB]";
					UseDB.ExecuteNonQuery();
					var RunDDL = myConn.CreateCommand();
					RunDDL.CommandText = File.ReadAllText("createDB.sql");
					RunDDL.ExecuteNonQuery();

					createDB.ExecuteNonQuery();
					UseDB.ExecuteNonQuery();
					RunDDL.ExecuteNonQuery();
					return myConn;
				}

				catch (Exception finalex)
				{
					MessageBox.Show(finalex.Message);
					return null;
				}
			}
		}
		private void pictureBox1_Click(object sender, EventArgs e)
		{
			Refresh();
		}
		private void Home_Load(object sender, EventArgs e)
		{

		}

		private void BtnFile_Click(object sender, EventArgs e)
		{
			try
			{
				string FilePath;
				//MessageBox.Show("ok");
				if (FpkExcel.ShowDialog() == DialogResult.OK)
				{
					if (new FileInfo(FpkExcel.FileName).Extension == ".csv")
					{
						FilePath = FpkExcel.FileName;

						MessageBox.Show(FilePath, "Input File");
						CSVCurrentData = new Spreadsheet(FilePath);
						CSVCurrentData.ReadSpreadsheet();
						//MessageBox.Show(Convert.ToString(CurrentData.DataOut[0].X), "Data in 1st row, 1st column");
						DgvCurrentData.ColumnCount = 2;
						DgvCurrentData.Rows.Add(CSVCurrentData.xtitle, CSVCurrentData.ytitle);
						for (int items = 0; items < CSVCurrentData.DataOut.Count(); items++)
						{
							DgvCurrentData.Rows.Add(CSVCurrentData.LocalColumn.Data[items].X, CSVCurrentData.LocalColumn.Data[items].Y);
						}
						UseCSV = true;
						UseExcel = false;
						fileStyle = "CSV";
						DgvCurrentData.Refresh();
						BtnRegress.Enabled = true;
						BtnSort.Enabled = true;
						BtnPrint.Enabled = true;
						BtnView.Enabled = true;
						chk_ConnectPoints.Enabled = true;
					}
					else if (new FileInfo(FpkExcel.FileName).Extension == ".xlsx")
					{
						MessageBox.Show(FpkExcel.FileName, "Input File");
						FilePath = FpkExcel.FileName;
						CurrentData = new Excel(FilePath);
						CurrentData.ReadWorkbook();
						//MessageBox.Show(Convert.ToString(CurrentData.DataOut[0].X), "Data in 1st row, 1st column");
						DgvCurrentData.ColumnCount = 2;
						DgvCurrentData.Rows.Add(CurrentData.xtitle, CurrentData.ytitle);
						XLabel = CurrentData.xtitle;
						YLabel = CurrentData.ytitle;
						for (int items = 0; items < CurrentData.DataOut.Count(); items++)
						{
							DgvCurrentData.Rows.Add(CurrentData.LocalColumn.Data[items].X, CurrentData.LocalColumn.Data[items].Y);
						}
						UseExcel = true;
						UseCSV = false;
						fileStyle = "Excel";
						DgvCurrentData.Refresh();
						BtnRegress.Enabled = true;
						BtnSort.Enabled = true;
						BtnPrint.Enabled = true;
						BtnView.Enabled = true;
						chk_ConnectPoints.Enabled = true;
					}
				}
				else
				{
					MessageBox.Show("No file selected", "Input File");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Input document not in correct format", "error");
			}

		}

		private void FpkExcel_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
		{

		}

		private void BtnFile_MouseHover(object sender, EventArgs e)
		{
			TtiOpen.SetToolTip(BtnFile, "Select a file to open");
		}

		private void BtnSort_Click(object sender, EventArgs e)
		{
			//uses data from file if UseExel is true, or data in the table otherwise
			Table Data;
			if (UseExcel == true)
			{
				Data = CurrentData.LocalColumn;
			}
			else if (UseCSV == true)
			{
				Data = CSVCurrentData.LocalColumn;
			}
			else
			{
				Data = UserData;
			}
			List<Coordinate> SortedData = Data.Data;
			try
			{
				SortedData = Table.Sort(Data.Data);
				Data.Data = SortedData;
				MessageBox.Show("Data Sorted", "Task Complete");
			}
			catch (Exception ex)
			{
				MessageBox.Show(Convert.ToString(ex), "Error Encountered");
			}
			//resets the table and adds the sorted list
			DgvCurrentData.Rows.Clear();
			DgvCurrentData.Rows.Add(Data.XLabel, Data.YLabel);
			for (int items = 0; items < SortedData.Count(); items++)
			{
				DgvCurrentData.Rows.Add(SortedData[items].X, SortedData[items].Y);
			}
			DgvCurrentData.Refresh();
		}

		private void BtnRegress_Click(object sender, EventArgs e)
		{
			Table Data;
			if (fileStyle == "Excel")
			{
				Data = CurrentData.LocalColumn;
			}
			else if (fileStyle == "CSV")
			{
				Data = CSVCurrentData.LocalColumn;
			}
			else if (fileStyle == "User")
			{
				Data = UserData;
			}
			else
			{
				Coordinate first = new Coordinate(0, 0);
				Coordinate second = new Coordinate(1, 1);
				List<Coordinate> list = new List<Coordinate>();
				list.Add(first);
				list.Add(second);
				Data = new Table(list, "error", "x", "y");
			}
			RunningChart = true;

			//turns data into values usable for analysis.
			//as correlation means nothing without a line of best fit, the two datapoints are superglued together in a tuple
			//a  correlation of 0 means there is no LINEAR correlation
			(string Expression, double RSquared) regressedData = Table.Regress(Data.Data);
			//standard deviation = variance squared, so theres no need for a second method here
			double SDev = Math.Sqrt(Table.Variance(Data.Data));
			double median = Table.Median(Table.Sort(Data.Data));

			MessageBox.Show("Expression: " + regressedData.Expression + "\nR squared: " + Convert.ToString(regressedData.RSquared + "\nStandard Deviation: " + SDev.ToString() + $"\nMedian flow for {Data.XLabel}: " + median));
			List<Coordinate> coordList = Data.Data;
			double[] xValues = new double[coordList.Count];
			double[] yValues = new double[coordList.Count];
			int i = 0;
			foreach (Coordinate location in coordList)
			{
				xValues[i] = location.X;
				yValues[i] = location.Y;
				i = i + 1;
			}
			x = xValues;
			y = yValues;
			a = regressedData.Expression;
			//uses the override that allows axis labels
			pltHome_Paint(xValues, yValues, regressedData.Expression, Data.XLabel, Data.YLabel, "RiverFlow");

		}
		//base method can be used if no axis labels are included
		private void pltHome_Paint(double[] dataX, double[] dataY, string alpha)
		{
			pltHome.Plot.Clear();
			double[] points = toCoordinate(alpha, dataX, dataY);
			Coordinates pt1 = new Coordinates(points[0], points[1]);
			Coordinates pt2 = new Coordinates(points[2], points[3]);
			var line = pltHome.Plot.Add.Line(pt1, pt2);
			pltHome.Plot.XLabel("Horizonal Axis");
			pltHome.Plot.YLabel("Vertical Axis");
			pltHome.Plot.Title("Plot Title");

			if (ConnectPoints == true)
			{
				pltHome.Plot.Add.Scatter(dataX, dataY);
			}
			else
			{
				pltHome.Plot.Add.ScatterPoints(dataX, dataY);
			}
			pltHome.Plot.Axes.AutoScale();
			pltHome.Refresh();
		}
		private void pltHome_Paint(double[] dataX, double[] dataY, string alpha, string xLabel, string yLabel, string dataType)
		{
			pltHome.Plot.Clear();
			double[] points = toCoordinate(alpha, dataX, dataY);
			Coordinates pt1 = new Coordinates(points[0], points[1]);
			Coordinates pt2 = new Coordinates(points[2], points[3]);
			var line = pltHome.Plot.Add.Line(pt1, pt2);
			pltHome.Plot.XLabel(xLabel);
			pltHome.Plot.YLabel(yLabel);
			pltHome.Plot.Title($"{dataType} at {xLabel} against {yLabel}");

			if (ConnectPoints == true)
			{
				pltHome.Plot.Add.Scatter(dataX, dataY);
			}
			else
			{
				pltHome.Plot.Add.ScatterPoints(dataX, dataY);
			}
			pltHome.Plot.Axes.AutoScale();
			pltHome.Refresh();
		}
		private void pltHome_Load(object sender, EventArgs e)
		{

		}

		private void btnPaintPlot_Click(object sender, EventArgs e)
		{
		}

		private void btnLogIn_Click(object sender, EventArgs e)
		{
			new LoginScreen(_connection).Show();
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{

		}

		private void chk_ConnectPoints_CheckedChanged(object sender, EventArgs e)
		{
			if (ConnectPoints == false)
				ConnectPoints = true;
			else
				ConnectPoints = false;
		}

		private void BtnView_Click(object sender, EventArgs e)
		{
			if (RunningChart)
			{
				GraphView Viewer = new GraphView(x, y, a, ConnectPoints, XLabel, YLabel);
				Viewer.Show();
			}
		}

		private void BtnCreate_Click(object sender, EventArgs e)
		{
			try
			{
				double x;
				double y;
				UseExcel = false;
				UseCSV = false;
				fileStyle = "User";
				BtnRegress.Enabled = true;
				BtnSort.Enabled = true;
				BtnPrint.Enabled = true;
				BtnView.Enabled = true;
				chk_ConnectPoints.Enabled = true;
				//UserData = DgvCurrentData[];
				List<Coordinate> tempData = new List<Coordinate>();
				foreach (DataGridViewRow row in DgvCurrentData.Rows)
				{
					x = Convert.ToDouble(row.Cells[0].Value);
					y = Convert.ToDouble(row.Cells[1].Value);
					tempData.Add(new Coordinate(x, y));
					UserData = new Table(tempData, "User Data", "", "");
				}
			}
			catch
			{
				MessageBox.Show("Invalid input data");
			}
		}

		private void DgvCurrentData_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}

		private void DgvCurrentData_Click(object sender, EventArgs e)
		{
			if (DgvCurrentData.Rows.Count == 0)
			{
				DgvCurrentData.ColumnCount = 2;
				DgvCurrentData.Columns[0].ValueType = typeof(double);
				DgvCurrentData.Columns[1].ValueType = typeof(double);
			}
		}
	}
}
