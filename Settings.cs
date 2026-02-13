using System.Xml;
namespace Prototype_V2
{

	internal class Settings
	{
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
								currentNode += " " + reader.Name + "='" + reader.Value + "'";
							currentNode += "/";
							currentNode += ">\n";
							o.AddLast(currentNode);
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
							currentNode += ">" + "/n";
							o.AddLast(currentNode);
							break;
						}
				}
			}
			return o;
		}
	}
}
