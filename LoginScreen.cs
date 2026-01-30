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
	public partial class LoginScreen : Form
	{
		uint[] key = new uint[] { 0xA56BABCD, 0x0000FFFF, 0xABCDEF01, 0x12345678 };
		// temp value before obfuscation
		string userID = "";
		string passwordTry = "";
		string hashedPassword;
		SqlConnection Connection;
		public LoginScreen(SqlConnection conn)
		{
			Connection = conn;
			InitializeComponent();
		}
		private void txtUserID_TextChanged(object sender, EventArgs e)
		{
			userID = txtUserID.Text;
			hashedPassword = "";
			try
			{
				SqlCommand SelectPwd = new SqlCommand($"SELECT hashed_password FROM Users WHERE username = {userID}", Connection);

			}
			catch { MessageBox.Show("oops"); }
			//should be set to retrieve the hashed password from a database based on inputted userID
		}

		private void txtPassword_TextChanged(object sender, EventArgs e)
		{
			passwordTry = txtPassword.Text;
		}

		private void btnLogin_Click(object sender, EventArgs e)
		{
			if (passwordTry == null || passwordTry == "")
			{
				MessageBox.Show("Please enter a password", "Password error");
			}
			else
			{
				TinyEncryption encryption = new TinyEncryption(passwordTry, key);
				MessageBox.Show(encryption.Encrypt(), "Encrypted Password");//outputs hashed value
				if (encryption.Encrypt() == hashedPassword)
				{
					this.Close();
					Application.Run(new PrototypeV2.Home());
					//now activate home page
				}
				else
				{
					MessageBox.Show("Incorrect password, please try again", "Password error");
				}
			}

		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void LoginScreen_Load(object sender, EventArgs e)
		{

		}
	}
}
