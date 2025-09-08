using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrototypeV2
{
	public partial class SheetPicker : Form
	{
		public string PickedSheet;
		public string[] options;
		public SheetPicker(string[] Options)
		{
			options = Options;
			PickedSheet = "";
			InitializeComponent();
			for (int items = 0; items < Options.Length; items++)
			{
				LstSelect.Items.Add(options[items]);
			}
		}
		//public void LstSelect_Click()
		//{
		//	try
		//	{
		//		PickedSheet = LstSelect.SelectedItem.ToString();
		//	}
		//	catch (Exception)
		//	{
		//		throw new Exception("Sheet name is null");
		//	}
		//	if (PickedSheet != null)
		//	{


		//	}
		//}

		private void BtnConfirmSheet_Click(object sender, EventArgs e)
		{
			//PickedSheet = LstSelect.SelectedItem.ToString();
			//MessageBox.Show(PickedSheet);
			this.Close();
		}

		private void LstSelect_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			//PickedSheet = LstSelect.SelectedItem.ToString();
			PickedSheet = LstSelect.SelectedItem.ToString();
			MessageBox.Show(PickedSheet);
		}

		private void LstSelect_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
	}
}
