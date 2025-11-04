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
		int n = 32; // the number of rounds to perform
		private uint[] Source;
		private uint[] Hash;
		private uint[] Key;

		public TinyEncryption(string source, uint[] key)
		{
			Source = FromString(source);//Convert password string to unsigned integer array
			Key = key;
			//Hash = Encrypt(Source, Key);//Encrypt the password and store the cipertext in Hash
		}
		public TinyEncryption(string source, uint deltaOverride, uint[] key)
		{
			delta = deltaOverride;
			Source = FromString(source);
			Key = key;
			//Hash = Encrypt(Source, Key);
		}
		//v is the data and k is the key
		//the value for delta is taken from the documentation for tiny encryption algorithm
		public string Encrypt()
		{
			uint[] v = Source;
			uint[] k = Key;
			uint y = v[0];
			uint z = v[1];
			uint sum = 0;
			//uint delta = 0x9e3779b9;
			//uint n = 32;

			while (n-- > 0)
			{
				y += (z << 4 ^ z >> 5) + z ^ sum + k[sum & 3];
				sum += delta;
				z += (y << 4 ^ y >> 5) + y ^ sum + k[sum >> 11 & 3];
			}

			v[0] = y;
			v[1] = z;
			Hash = v;
			return $"{y}.{z}";
		}
		public string Decrypt()
		{
			//uint n = 32;
			//uint delta = 0x9e3779b9;
			uint[] v = Hash;
			uint[] k = Key;
			uint sum;
			uint y = v[0];
			uint z = v[1];

			sum = delta << 5;

			while (n-- > 0)
			{
				z -= (y << 4 ^ y >> 5) + y ^ sum + k[sum >> 11 & 3];
				sum -= delta;
				y -= (z << 4 ^ z >> 5) + z ^ sum + k[sum & 3];
			}

			v[0] = y;
			v[1] = z;
			return ToString(v);
		}
		public string ToString(uint[] Unsigned)
		{
			string Result = "";
			foreach (uint value in Unsigned)
			{
				byte[] byteArray = BitConverter.GetBytes(value);
				foreach (byte b in byteArray)
				{
					Result = Result + ((char)b); // Only works for ASCII characters
				}
			}
			return Result;

		}
		public uint[] FromString(string Source)
		{
			Encoding ascii = Encoding.ASCII;
			byte[] bytes = ascii.GetBytes(Source);
			uint[] Unsigned = new uint[bytes.Length];
			// Convert each byte to a uint
			for (int i = 0; i < bytes.Length; i++)
			{
				Unsigned[i] = Convert.ToUInt32(bytes[i]);
			}
			return Unsigned;
		}
	}
}

