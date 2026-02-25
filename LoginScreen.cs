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
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime.InteropServices;

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
			//connect to the known good connection from Program.Home
			Connection = conn;
			
			InitializeComponent();
		}
		private void txtUserID_TextChanged(object sender, EventArgs e)
		{
		//UserID here means the username field in the database
			userID = txtUserID.Text;
		}

		private void txtPassword_TextChanged(object sender, EventArgs e)
		{
			passwordTry = txtPassword.Text;
		}

		private void btnLogin_Click(object sender, EventArgs e)
		{
			Connection.Open();
			hashedPassword = "";
			int usernumber;
			int DeviceID;
			try
			{
				SqlCommand SelectPwd = new SqlCommand($"SELECT hashed_password FROM Users WHERE username = '{userID}'", Connection);
				SqlDataReader sr = SelectPwd.ExecuteReader();
				//retrieve first record that meets the criteria
				if (sr.Read())
				{
					hashedPassword = sr.GetString(0);
					sr.Close();
				}
				//should be set to retrieve the hashed password from a database based on inputted userID
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

						//find device name and id
						string deviceName = Environment.MachineName.ToString();
						using (sr = new SqlCommand($"SELECT device_id FROM Devices WHERE device_name = '{deviceName}'", Connection).ExecuteReader())
						{
							sr.Read();
							DeviceID = sr.GetInt32(0);
							sr.Close();
						}

						//find user id
						using (sr = new SqlCommand($"SELECT user_id FROM Users WHERE username = '{userID}'", Connection).ExecuteReader())
						{
							sr.Read();
							usernumber = sr.GetInt32(0);
							sr.Close();
						}

						//find time
						string timeStamp = DateTime.Now.ToString();
						
						//add device info
						SqlCommand cmd = new SqlCommand($"INSERT INTO Sessions (session_timestamp, user_id, device_id) VALUES ('{timeStamp}', '{usernumber}' , '{DeviceID}')", Connection);
						cmd.ExecuteNonQuery();
						Connection.Close();
						this.Close();
					}
					else
					{
						MessageBox.Show("Incorrect password, please try again", "Password error");
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}


		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void LoginScreen_Load(object sender, EventArgs e)
		{

		}

		private void btn_SignUp_Click(object sender, EventArgs e)
		{
			Connection.Open();
			string testUser = "SuperUser";
			string testPass = "password";
			TinyEncryption encr = new TinyEncryption(testPass, key);
			string hashedPass = encr.Encrypt();
			string Query = $"INSERT INTO Users VALUES ('{testUser}', '{hashedPass}', 'Admin');";
			SqlCommand CreateAccount = new SqlCommand(Query,Connection);
			CreateAccount.ExecuteNonQuery();

			string testDevice = Environment.MachineName.ToString();
			Query = $"INSERT INTO Devices (device_name) VALUES ('{testDevice}')";
			SqlCommand CreateDevice = new SqlCommand(Query,Connection);
			CreateDevice.ExecuteNonQuery();
			Connection.Close();
		}
	}
}
