using System.Configuration;
using System.Xml;
namespace Prototype_V2
{

	internal class Settings
	{
		public static void addConnection(string connString)
		{
			LinkedList<string> list = Read();
			string command = "<add key='{key}' value='{value}' />\n";
			LinkedListNode<string> newNode = new LinkedListNode<string>(command);
			list.AddAfter(list.Find("<connectionString>\n"), newNode);
			Write(list);
		}
		public static string getConnections()
		{
			XmlTextReader reader = new XmlTextReader("appSettings.xml");
			string output = "";
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.Element && reader.Name == "connectionString")
				{
					while (reader.MoveToNextAttribute())
					{
						output = reader.Value;
					}
				}
			}
			reader.Close();
			return output;
				//switch (reader.NodeType)
				//{
				//	case (XmlNodeType.Element): // node is an element tag
				//		{
				//			currentNode += "<" + reader.Name;
				//			while (reader.MoveToNextAttribute()) // Read the attributes.
				//				currentNode += " " + reader.Name + "='" + reader.Value + "'";
				//			currentNode += "/";
				//			currentNode += ">\n";
				//			o.AddLast(currentNode);
				//			break;
				//		}
				//	case (XmlNodeType.Text): // node is text
				//		{
				//			currentNode += reader.Value + "\n";
				//			o.AddLast(currentNode);
				//			break;
				//		}
				//	case (XmlNodeType.EndElement): // node is element end tag
				//		{
				//			currentNode += "</" + reader.Name;
				//			currentNode += ">" + "/n";
				//			o.AddLast(currentNode);
				//			break;
				//		}
				//}
			
		}
		//returns entire XML file as a list of strings, with 1 index being 1 attribute
		public static LinkedList<string> Read()
		{
			XmlTextReader reader = new XmlTextReader("appSettings.xml");
			LinkedList<string> o = new LinkedList<string>();
			string currentNode;
			while (reader.Read())
			{
				currentNode = "";
				switch (reader.NodeType)
				{
					case (XmlNodeType.Element): // node is an element tag
						{
							currentNode += "<" + reader.Name;
							while (reader.MoveToNextAttribute()) // Read the attributes.
							{
								currentNode += " " + reader.Name + "='" + reader.Value + "'";
								currentNode += ">\n";
								o.AddLast(currentNode);
							}
							break;
							
						}
					case (XmlNodeType.Text): // node is text
						{
							currentNode += reader.Value + "\n";
							o.AddLast(currentNode);
							break;
						}
					case (XmlNodeType.EndElement): // node is element end tag
						{
							currentNode += "</" + reader.Name;
							currentNode += ">" + "\n";
							o.AddLast(currentNode);
							break;
						}
				}
			}
			reader.Close();
			return o;
		}
		public static void Write(LinkedList<string> o)
		{
			string FinalXML = "";
			foreach (string s in o)
			{
				FinalXML += s;
			}
			File.WriteAllText("appSettings.xml", FinalXML);
		}
	}
}
