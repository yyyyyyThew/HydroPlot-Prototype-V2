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
		public string ToString(uint[] Unsigned)
		{
			string Result = "";
			for (int i = 0; i < Unsigned.Length; i++)
			{
				Result = Result + Convert.ToChar(Unsigned[i]);
			}
			return Source;
		}
		public uint[] FromString(string Source)
		{
			Encoding ascii = Encoding.ASCII;
			byte[] bytes = ascii.GetBytes(Source);

			// Initialize the array with the correct size
			uint[] Unsigned = new uint[bytes.Length];

			// Convert each byte to a uint and store it in the array
			for (int i = 0; i < bytes.Length; i++)
			{
				Unsigned[i] = Convert.ToUInt32(bytes[i]);
			}
			return Unsigned;
		}
	}
}

