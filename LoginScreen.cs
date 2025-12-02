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
		public LoginScreen()
		{
			InitializeComponent();
		}
		private void txtUserID_TextChanged(object sender, EventArgs e)
		{
			userID = txtUserID.Text;
			hashedPassword = "";
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
	}
}
