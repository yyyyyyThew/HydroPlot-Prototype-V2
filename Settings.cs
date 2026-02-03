using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Configuration;
using System.Collections.Specialized;

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
		public static bool Write(string parameter, string value)
		{
			try
			{
				
				//write "value" to "parameter" in the xml doc
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
	}
