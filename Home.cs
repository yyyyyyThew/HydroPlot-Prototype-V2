using Microsoft.Data.SqlClient;
using Prototype_V2;
using ScottPlot;


namespace PrototypeV2
{
	public partial class Home : Form
	{
		//will eventually be fed into the Excel() constructor as the file path
		Excel CurrentData;
		//Line BestFit;
		double[] x, y;
		string a;
		private bool ConnectPoints;
		private bool RunningChart;
		public Home()
		{
			RunningChart = false;
			ConnectPoints = false;
			goPaint = false;
			InitializeComponent();
			CustomComponents();
		}
		//private Pen blackPen = new Pen(Color.FromArgb(255, 0, 0, 0));
		//private Pen redPen = new Pen(Color.FromArgb(255, 255, 0, 0));

		// Class-level declarations for the pens used
		private bool goPaint;
		private void CustomComponents()
		{
			Connect();
			//send the start timestamp to the database


			//set the filter for the file pick dialogue
			//description of filter goes in the brackets
			//after the pipe goes the actual file extensions, followed by a semi colon.
			FpkExcel.Filter = "Spreadsheet Files(*.CSV, *.XLSX)|*.CSV;*.XLSX|All files (*.*)|*.*";
			//initialises the width of each pen type
			//blackPen.Width = 5;
			//redPen.Width = 2;
			BtnRegress.Enabled = false;
			BtnSort.Enabled = false;
			BtnPrint.Enabled = false;
			BtnView.Enabled = false;
			chk_ConnectPoints.Enabled = false;
			// Subscribe to the Paint event
			//this.Paint += Home_Paint;


		}

		//private void pictureBox1_Paint(object sender, PaintEventArgs e)
		//{
		//	//(0, 0) = (72, 306)px and (m, n) = (480, 0)px
		//	//(0, 0) is in the top left, not the bottom left, so there has to be a method that turns coordinates into pixels
		//	Graphics g = e.Graphics;
		//	//maybe move EVERYTHING in by a few px
		//	//draws the axes
		//	//y (0.15*height margin)
		//	g.DrawLine(blackPen, 72, 54, 72, 306);
		//	//x (0.15*width margin)
		//	g.DrawLine(blackPen, 72, 306, 475, 306);
		//	//draws a line starting from (0, 0) that repeats every n pixels
		//	int n = 20;
		//	//sets x
		//	for (int group = 72; group < 480; group += n)
		//	{
		//		g.DrawLine(blackPen, group, 306, group, 298);
		//	}
		//	// sets y
		//	for (int group = 306; group >= 54; group -= n)
		//	{
		//		g.DrawLine(blackPen, 72, group, 80, group);
		//	}
		//	//at (0, 0)
		//	g.DrawEllipse(redPen, new Rectangle(72, 306, 1, 1));


		//}

		static public double[] toCoordinate(string alpha, double[] dataX, double[] dataY)
		{
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
				subString = alpha.Substring(plusIndex + 1, 5);
				//subString = subString.Substring(0,5);
				c = double.Parse(subString);

				//turns m and c into a straight line
				int index;
				if (dataX.Length < dataY.Length)
				{
					index = dataX.Length;
				}
				else if (dataX.Length > dataY.Length)
				{
					index = dataY.Length;
				}
				else
				{
					index = dataX.Length;
				}
				double firstXVar = dataX[0];
				double firstYVar = m * firstXVar + c;
				double lastXVar = dataX[index - 1];
				double lastYVar = m * lastXVar + c;
				return new double[] { firstXVar, firstYVar, lastXVar, lastYVar };
			}
			return new double[] { 0, 0, 0, 0 };
		}

		private void Connect()
		{
			string str;
			//string testpath = @"(localDB)\testDB";
			string finalpath = @"A240392\SQLEXPRESS";
			//eventually pull from XML settings to find this
			SqlConnection myConn = new SqlConnection($@"Server={finalpath};Database=master;TrustServerCertificate=True;Trusted_Connection=True;");
			str = File.ReadAllText("createDB.sql");

			SqlCommand createDB = new SqlCommand(str, myConn);

			try
			{
				myConn.Open();
				createDB.ExecuteNonQuery();
				MessageBox.Show("DDL has been run", "SystemTrackerDB");
				//switching to new/existing database
				SqlCommand useDB = new SqlCommand("USE SystemTrackerDB", myConn);
				useDB.ExecuteNonQuery();
				MessageBox.Show("Successfully switched to SystemTrackerDB", "SystemTrackerDB");
			}
			catch (SqlException ex)
			{
				// Handle specific exception if the database already exists
				if (ex.Message.Contains("database already exists"))
				{
					MessageBox.Show("Database already exists, proceeding with the operation.", "SystemTrackerDB");
				}
				else if (ex.Message.Contains("Database 'SystemTrackerDB' does not exist"))
				{
					MessageBox.Show("Cannot open database, running offline", "Error");
				}
				else
				{
					// Show other error messages
					MessageBox.Show("Error: " + ex.Message, "Error");
				}
			}
			finally
			{
				myConn.Close();
			}
		}
		private void pictureBox1_Click(object sender, EventArgs e)
		{
			goPaint = true;
			Refresh();
		}

		//private void pictureBox1_DoubleClick(object sender, EventArgs e)
		//{
		//	goPaint = true;
		//	Graphics g = pictureBox1.CreateGraphics();
		//	Rectangle rect = new Rectangle(5, 4, 5, 5);
		//	g.DrawEllipse(blackPen, new Rectangle(0, 0, 2, 2));
		//	g.DrawEllipse(blackPen, new Rectangle(460, 340, 2, 2));
		//}

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
					MessageBox.Show(FpkExcel.FileName, "Input File");
					FilePath = FpkExcel.FileName;
					CurrentData = new Excel(FilePath);
					CurrentData.ReadWorkbook();
					//MessageBox.Show(Convert.ToString(CurrentData.DataOut[0].X), "Data in 1st row, 1st column");
					DgvCurrentData.ColumnCount = 2;
					DgvCurrentData.Rows.Add(CurrentData.xtitle, CurrentData.ytitle);
					for (int items = 0; items < CurrentData.DataOut.Count(); items++)
					{
						DgvCurrentData.Rows.Add(CurrentData.LocalColumn.Data[items].X, CurrentData.LocalColumn.Data[items].Y);
					}
					DgvCurrentData.Refresh();
					BtnRegress.Enabled = true;
					BtnSort.Enabled = true;
					BtnPrint.Enabled = true;
					BtnView.Enabled = true;
					chk_ConnectPoints.Enabled = true;
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
			List<Coordinate> SortedData = CurrentData.LocalColumn.Data;
			try
			{
				SortedData = CurrentData.LocalColumn.Sort(CurrentData.LocalColumn.Data);
				CurrentData.LocalColumn.Data = SortedData;
				MessageBox.Show("Data Sorted", "Task Complete");
			}
			catch (Exception ex)
			{
				MessageBox.Show(Convert.ToString(ex), "Error Encountered");
			}
			DgvCurrentData.Rows.Clear();
			DgvCurrentData.Rows.Add(CurrentData.xtitle, CurrentData.ytitle);
			for (int items = 0; items < SortedData.Count(); items++)
			{
				DgvCurrentData.Rows.Add(SortedData[items].X, SortedData[items].Y);
			}
			DgvCurrentData.Refresh();
		}

		private void BtnRegress_Click(object sender, EventArgs e)
		{
			//CurrentData.LocalColumn.Regress(CurrentData.LocalColumn.Data);
			//Line Regression = CurrentData.LocalColumn.BestFit;

			//LinearLine BestFit = new LinearLine(Regression.Gradient, Regression.YIntercept);
			//LogLine BestFit = new LogLine(Regression.A, Regression.YIntercept);

			//(old) for some reason this always returns 0 for vars that should have different data
			//(old)sometimes values for A and YIntercept are NaN for some reason
			//MessageBox.Show(Convert.ToString(BestFit.Print()));
			RunningChart = true;
			string alpha = CurrentData.LocalColumn.Regress(CurrentData.LocalColumn.Data);
			MessageBox.Show(alpha, "equation");
			List<Coordinate> coordList = CurrentData.LocalColumn.Data;
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
			a = alpha;
			pltHome_Paint(xValues, yValues, alpha);

			//time to paint oh yeahhhh
			//Graphics g = pictureBox1.CreateGraphics();
			//from 0,0 to 70% along the x-axis
			//Program.GetPoints(this.pictureBox1);
			//g.DrawLine(redPen, 72, 306, 475, Convert.ToInt16(Math.Floor(306 * 0.7)));
			//pictureBox1.Refresh();
		}
		//private void pltHome_Paint(double[] dataX, double[] dataY)
		//{
		//	//double[] dataX = { 1, 2, 3, 4, 5 };
		//	//double[] dataY = { 1, 4, 9, 16, 25 };
		//	//Coordinates pt1;
		//	//Coordinates pt2;
		//	pltHome.Plot.Add.Scatter(dataX, dataY);
		//	//var line = pltHome.Plot.Add.Line(pt1, pt2);
		//	pltHome.Plot.Axes.AutoScale();
		//	pltHome.Refresh();
		//}
		private void pltHome_Paint(double[] dataX, double[] dataY, String alpha)
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
		private void pltHome_Paint(double[] dataX, double[] dataY, String alpha, string xLabel, string yLabel, string dataType)
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
			//pltHome_Paint();
		}

		private void btnLogIn_Click(object sender, EventArgs e)
		{
			new LoginScreen().Show();
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
				GraphView Viewer = new GraphView(x, y, a, ConnectPoints);
				Viewer.Show();
			}
		}
	}
}
