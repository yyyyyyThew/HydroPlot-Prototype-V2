using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype_V2
{
	class TinyEncryption
	{
		uint delta = 0x9e3779b9;
		int n = 32 ; //the number of rounds to perform#
		private string Source;
		private string Hash;
		private string Key;

		public TinyEncryption(string source, string key)
		{
			delta = 0x9e3779b9;
			Source = source;
			Key = key;
			if (Source.Length % 2 != 0)
			{
				Source += " ";
			}
			Hash = Encrypt(FromString(Source), Key);
		}
		public TinyEncryption(string source, uint deltaOverride)
		{
			delta = deltaOverride;
		}
		//v is the data and k is the key
		//the value for delta is taken from the documentation for tiny encryption algorithm
		public void Encrypt(uint[] v, uint[] k)
		{
			uint y = v[0];
			uint z = v[1];
			uint sum = 0;
			//uint delta = 0x9e3779b9;
			uint n = 32;

			while (n-- > 0)
			{
				y += (z << 4 ^ z >> 5) + z ^ sum + k[sum & 3];
				sum += delta;
				z += (y << 4 ^ y >> 5) + y ^ sum + k[sum >> 11 & 3];
			}

			v[0] = y;
			v[1] = z;
		}
		public void Decrypt(uint[] v, uint[] k)
		{
			uint n = 32;
			uint sum;
			uint y = v[0];
			uint z = v[1];
			uint delta = 0x9e3779b9;

			sum = delta << 5;

			while (n-- > 0)
			{
				z -= (y << 4 ^ y >> 5) + y ^ sum + k[sum >> 11 & 3];
				sum -= delta;
				y -= (z << 4 ^ z >> 5) + z ^ sum + k[sum & 3];
			}

			v[0] = y;
			v[1] = z;
		}
		public string ToString(uint In)
		{
			System.Text.StringBuilder output = new System.Text.StringBuilder();
			output.Append((char)((In & 0xFF)));
			output.Append((char)((In >> 8) & 0xFF));
			output.Append((char)((In >> 16) & 0xFF));
			output.Append((char)((In >> 24) & 0xFF));
			return output.ToString();
		}
		public uint FromString(string In)
		{
			uint output;
			output = ((uint)In[0]);
			output += ((uint)In[1] << 8);
			output += ((uint)In[2] << 16);
			output += ((uint)In[3] << 24);
			return output;
		}
	}
}

