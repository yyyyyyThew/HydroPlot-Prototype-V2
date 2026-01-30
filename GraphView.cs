using NPOI.HPSF;
using NPOI.Util;
using PrototypeV2;
using ScottPlot;
using ScottPlot.Plottables;
using ScottPlot.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prototype_V2
{
	public partial class GraphView : Form
	{
		private double[] x;
		private double[] y;
		private string a;
		bool connectpoints;
		public GraphView(double[] dataX, double[] dataY, string alpha, bool ConnectPoints)
		{
			x = dataX;
			y = dataY;
			a = alpha;
			connectpoints = ConnectPoints;
			InitializeComponent();
			pltView_Paint(x, y, a, connectpoints);
		}

		private void GV_FullscreenView_Load(object sender, EventArgs e)
		{


		}

		private void btn_SavePng_Click(object sender, EventArgs e)
		{
			if (SAV_SaveFile.ShowDialog() == DialogResult.OK)
			{
				try
				{
					string filepath = SAV_SaveFile.FileName;

					Bitmap bmp = GV_FullscreenView.Plot.GetImage(Width, Height).GetBitmap();
					bmp.Save(filepath, System.Drawing.Imaging.ImageFormat.Png);

					MessageBox.Show("Saved to: " + filepath);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString(), "Error");
				}
			}
			else
			{
				MessageBox.Show("No file selected", "Input File");
			}
		}
		private void pltView_Paint(double[] dataX, double[] dataY, String alpha, bool ConnectPoints)
		{
			//GV_FullscreenView.Plot.Clear();
			double[] points = Home.toCoordinate(alpha, dataX, dataY);
			Coordinates pt1 = new Coordinates(points[0], points[1]);
			Coordinates pt2 = new Coordinates(points[2], points[3]);
			var line = GV_FullscreenView.Plot.Add.Line(pt1, pt2);

			if (ConnectPoints == true)
			{
				GV_FullscreenView.Plot.Add.Scatter(dataX, dataY);
			}
			else
			{
				GV_FullscreenView.Plot.Add.ScatterPoints(dataX, dataY);
			}
			GV_FullscreenView.Plot.Axes.AutoScale();
			GV_FullscreenView.Refresh();
		}
		private void btn_Leave_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void SAV_SaveFile_FileOk(object sender, CancelEventArgs e)
		{

		}

		private void GraphView_Load(object sender, EventArgs e)
		{

		}
	}
}
