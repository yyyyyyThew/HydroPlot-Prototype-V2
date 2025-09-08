using System.Windows.Forms;
using System.Threading;

namespace PrototypeV2
{
	public partial class Home : Form
	{
		string FilePath;
		double Gradient;
		double YIntercept;
		//will eventually be fed into the Excel() constructor as the file path
		Excel CurrentData;
		Line BestFit;
		public Home()
		{
			InitializeComponent();
			CustomComponents();
		}
		private Pen blackPen = new Pen(Color.FromArgb(255, 0, 0, 0));
		private Pen redPen = new Pen(Color.FromArgb(255, 255, 0, 0));

		// Class-level declarations for the pens used
		private bool goPaint = false;
		private void CustomComponents()
		{
			//set the filter for the file pick dialogue
			//description of filter goes in the brackets
			//after the pipe goes the actual file extensions, followed by a semi colon.
			FpkExcel.Filter = "Spreadsheet Files(*.CSV, *.XLSX)|*.CSV;*.XLSX|All files (*.*)|*.*";
			//initialises the width of each pen type
			blackPen.Width = 5;
			redPen.Width = 2;
			BtnRegress.Enabled = false;
			// Subscribe to the Paint event
			//this.Paint += Home_Paint;


		}

		private void pictureBox1_Paint(object sender, PaintEventArgs e)
		{
			//(0, 0) = (72, 306)px and (m, n) = (480, 0)px
			//(0, 0) is in the top left, not the bottom left, so there has to be a method that turns coordinates into pixels
			Graphics g = e.Graphics;
			//maybe move EVERYTHING in by a few px
			//draws the axes
			//y (0.15*height margin)
			g.DrawLine(blackPen, 72, 54, 72, 306);
			//x (0.15*width margin)
			g.DrawLine(blackPen, 72, 306, 475, 306);
			//draws a line starting from (0, 0) that repeats every n pixels
			int n = 20;
			//sets x
			for (int group = 72; group < 480; group += n)
			{
				g.DrawLine(blackPen, group, 306, group, 298);
			}
			// sets y
			for (int group = 306; group >= 54; group -= n)
			{
				g.DrawLine(blackPen, 72, group, 80, group);
			}
			//at (0, 0)
			g.DrawEllipse(redPen, new Rectangle(72, 306, 1, 1));


		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{
			goPaint = true;
			Refresh();
		}

		private void pictureBox1_DoubleClick(object sender, EventArgs e)
		{
			goPaint = true;
			Graphics g = pictureBox1.CreateGraphics();
			//Rectangle rect = new Rectangle(5, 4, 5, 5);
			g.DrawEllipse(blackPen, new Rectangle(0, 0, 2, 2));
			g.DrawEllipse(blackPen, new Rectangle(460, 340, 2, 2));
		}

		private void Home_Load(object sender, EventArgs e)
		{

		}

		private void BtnFile_Click(object sender, EventArgs e)
		{
			MessageBox.Show("ok");
			if (FpkExcel.ShowDialog() == DialogResult.OK)
			{
				MessageBox.Show(FpkExcel.FileName, "Input File");
			}
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
			List<Coordinate> SortedData = CurrentData.LocalColumn.Sort(CurrentData.LocalColumn.Data);
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
			if (CurrentData.LocalColumn.Regress(CurrentData.LocalColumn.Data) == "")
			{
				MessageBox.Show(CurrentData.LocalColumn.RegressLog(CurrentData.LocalColumn.Data));
				//YIntercept = CurrentData.LocalColumn.LogBestFit.YIntercept;
				//THIS DOES'NT WORK
				//PLS FIX
				//Gradient = CurrentData.LocalColumn.LogBestFit.A;
			}
			else
			{
				try
				{
					MessageBox.Show(CurrentData.LocalColumn.Regress(CurrentData.LocalColumn.Data));
					YIntercept = CurrentData.LocalColumn.LinearBestFit.YIntercept;
					Gradient = CurrentData.LocalColumn.LinearBestFit.Gradient;
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
			//(old) for some reason this always returns 0 for vars that should have different data
			//(old)sometimes values for A and YIntercept are NaN for some reason
			//MessageBox.Show(Convert.ToString(BestFit.Print()));

			//time to paint oh yeahhhh
			Graphics g = pictureBox1.CreateGraphics();
			//from 0,0 to 70% along the x-axis
			//Program.GetPoints(this.pictureBox1);
			g.DrawLine(redPen, 72, 306, 475, Convert.ToInt16(Math.Floor(306 * 0.7)));
			pictureBox1.Refresh();
		}
	}
}
