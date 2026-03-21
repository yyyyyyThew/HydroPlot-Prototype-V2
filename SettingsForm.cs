using Microsoft.Data.SqlClient;
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
	public partial class SettingsForm : Form
	{
		private string UserInput;
		private string ServerName;
		public SettingsForm()
		{
			InitializeComponent();
			UserInput = "";
			ServerName = "";
			XMLContents.Text = Settings.ReadAll();
		}

		private void XMLContents_TextChanged(object sender, EventArgs e)
		{

		}

		private void btnInsert_Click(object sender, EventArgs e)
		{
			try
			{
				//if the string can be added, then closed, input is accepted
				Settings.AddConnection(UserInput, ServerName);
			}

			catch { MessageBox.Show("Error"); }
		}

		private void txtUserInput_TextChanged(object sender, EventArgs e)
		{
			UserInput = txtUserInput.Text;
		}

		private void txtConnectionName_TextChanged(object sender, EventArgs e)
		{
			ServerName = txtUserInput.Text;

		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{

		}
	}
}
