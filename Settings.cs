using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Prototype_V2
{
	internal class Settings
	{

		public static void AddConnection(string key, string value)
		{
			XDocument doc = XDocument.Load("appSettings.xml");

			XElement root = doc.Element("settings");
			XElement connectionNode = null;

			if (root != null)
			{
				connectionNode = root.Element("connectionString");
			}

			if (connectionNode == null)
			{
				connectionNode = new XElement("connectionString");

				if (root != null)
				{
					root.Add(connectionNode);
				}
			}

			// Remove existing connection with same key
			XElement existing = null;

			IEnumerable<XElement> addElements = connectionNode.Elements("add");

			foreach (XElement element in addElements)
			{
				XAttribute keyAttribute = element.Attribute("key");

				if (keyAttribute != null)
				{
					if (keyAttribute.Value == key)
					{
						existing = element;
						break;
					}
				}
			}

			if (existing != null)
			{
				existing.Remove();
			}

			// Create new add element
			XElement addElement = new XElement("add");

			XAttribute keyAttributeNew = new XAttribute("key", key);
			XAttribute valueAttributeNew = new XAttribute("value", value);

			addElement.Add(keyAttributeNew);
			addElement.Add(valueAttributeNew);

			connectionNode.Add(addElement);

			doc.Save("appSettings.xml");
		}

		///<summary>
		/// Returns the value of a connection string from the appSettings.xml file based on the provided key.
		///</summary>>
		///<param name="key">The key of the connection string to retrieve.</param>
		///<returns>The value of the connection string associated with the provided key</returns>
		public static string GetConnection(string key)
		{
			if (!File.Exists("appSettings.xml"))
			{
				return null;
			}

			XDocument doc = XDocument.Load("appSettings.xml");

			XElement settingsElement = doc.Element("settings");
			if (settingsElement == null)
			{
				return null;
			}

			XElement connectionNode = settingsElement.Element("connectionString");
			if (connectionNode == null)
			{
				return null;
			}

			XElement foundConnection = null;

			IEnumerable<XElement> addElements = connectionNode.Elements("add");

			foreach (XElement element in addElements)
			{
				XAttribute keyAttribute = element.Attribute("key");

				if (keyAttribute != null)
				{
					if (keyAttribute.Value == key)
					{
						foundConnection = element;
						break;
					}
				}
			}

			if (foundConnection == null)
			{
				return null;
			}

			XAttribute valueAttribute = foundConnection.Attribute("value");

			if (valueAttribute == null)
			{
				return null;
			}

			return valueAttribute.Value;
		}
	}
}