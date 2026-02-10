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
using NPOI.Util;

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

			// Read all the keys from the config file
			NameValueCollection sAll;
			sAll = ConfigurationManager.AppSettings;
			//Create a 2D array that holds key-value pairs
			string[,] output = new string[sAll.Count, 1];
			int i = 0;
			foreach (string s in sAll.AllKeys)
			{
				output[i, 0] = s;
				output[i, 1] = sAll.Get(s);
				i++;
			}
			return output;
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
		public static string GetConnection(string name)
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

			string DatabaseEnabled = ConfigurationManager.AppSettings["Database_Enabled"];

			var _configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoaming);
			ConnectionStringSettings NewString = new ConnectionStringSettings();
			NewString.Name = "SG20";
			NewString.ConnectionString = @"Server=A240392\\SQLEXPRESS;TrustServerCertificate=True;Trusted_Connection=True;Initial Catalog=SystemTrackerDB;\";
			_configuration.ConnectionStrings.ConnectionStrings.Add(NewString);
			ConnectionStringSettingsCollection connections = _configuration.ConnectionStrings.ConnectionStrings;
			if (connections.Count != 0 && DatabaseEnabled == "True")
			{
				foreach (ConnectionStringSettings css in connections)
				{
					//reading the Connection Strings
					string conString = _configuration.ConnectionStrings.ConnectionStrings[css.Name].ConnectionString;
					MessageBox.Show(conString);
				}
			}

			string Theme = ConfigurationManager.AppSettings["Theme"];
		}
	}
}
