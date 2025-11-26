namespace Prototype_V2
{
	public partial class Home : Form
	{
		uint[] key = new uint[] { 0xA56BABCD, 0x0000FFFF, 0xABCDEF01, 0x12345678 };
		// temp value before obfuscation
		string userID = "";
		string passwordTry = "";
		string hashedPassword;
		public Home()
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
			TinyEncryption encryption = new TinyEncryption(passwordTry, key);
			MessageBox.Show(encryption.Encrypt(), "Encrypted Password");//outputs hashed value
			if (passwordTry == "")
			{
				MessageBox.Show("Please enter a password", "Password error");
			}
			else if (encryption.Encrypt() == hashedPassword)
			{
				//now activate home page
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void Home_Load(object sender, EventArgs e)
		{

		}
	}
}
