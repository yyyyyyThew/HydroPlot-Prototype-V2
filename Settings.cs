using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Prototype_V2;
using System.Configuration;
using System.Xml;
using System.Configuration;
using System.Collections.Specialized;
using static Org.BouncyCastle.Math.EC.ECCurve;
using OpenTK.Graphics.ES10;

namespace Prototype_V2
{
	
	internal class Settings
	{
	    public Settings() 
		{

		}
		public static string Read(string parameter)
		{
			string output = "";
	
				output = ConfigurationManager.AppSettings.Get(parameter);
				return output;

		}
		public static string[,] ReadAll()
		{
			try
			{
				// Read all the keys from the config file
				NameValueCollection sAll;
				sAll = ConfigurationManager.AppSettings;
				//Create a 2D array that holds key-value pairs
				string[,] output = new string[sAll.Count, 1];
				int i = 0;
				foreach (string s in sAll.AllKeys)
				{
					output[i,0] = "Key: " + s;
					output[i, 1] = "Value: " + sAll.Get(s);
					i++;
				}
				return output;

			}
			catch
			{
				return null;
			}
		}
		public static bool Write(string key, string value)
		{
			try
			{
				ConfigurationManager.AppSettings.Add(key, value);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public static bool OverWrite(string key, string value)
		{
			try
			{
				Configuration configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
				KeyValueConfigurationCollection settings = configFile.AppSettings.Settings;
				if (settings[key] == null)
				{
					settings.Add(key, value);
				}
				else
				{
					settings[key].Value = value;
				}
				configFile.Save(ConfigurationSaveMode.Modified);
				ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
				return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
				return false;
			}
		}
		//for changing a connectionstring to the currently used database
		public static bool AddConnection(SqlConnection _connection, string name)
		{
			try
			{
				string ConnectionString = _connection.ConnectionString;
				var csSettings = new ConnectionStringSettings(ConnectionString, name);
				ConfigurationManager.ConnectionStrings.Add(csSettings);
				return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
				return false;
			}
		}
		//for changing the connection string manually
		public static bool AddConnection(string connectionString, string name)
		{
			try
			{
				Configuration configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
				var csSettings = new ConnectionStringSettings(connectionString, name);
				configFile.ConnectionStrings.ConnectionStrings.Add(csSettings);
				return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
				return false;
			}
		}
		public static string GetConnection(string name )
		{
			try
			{
				string connectionString = ConfigurationManager.ConnectionStrings[name].ToString();
				return connectionString;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
				return "";
			}
		}
		public static void Initialise()
		{
			//loop through, applying the value in the config file to the current instance of the application
			string[,] sAll = ReadAll();
			for (int i = 0; i <= sAll.GetLength(0); i++)
			{
				if (sAll[i,0] == "Database_Enabled" && sAll[i, 1] == "True")
				{
					MessageBox.Show("Databases enabled in config");
					GetConnection("SG20");
				}
			}
			}

		}
}
