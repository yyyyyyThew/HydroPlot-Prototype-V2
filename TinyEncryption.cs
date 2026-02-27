using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Prototype_V2
{
	class TinyEncryption
	{
		uint delta = 0x9e3779b9;
		int n = 32; // the number of rounds to perform
		private uint[] Value;
		private uint[] Hash;
		private uint[] Key;

		public TinyEncryption(string source, uint[] key)
		{
			Value = FromString(source);//Convert password string to unsigned integer array
			Key = key;
			//Hash = Encrypt(Source, Key);//Encrypt the password and store the cipertext in Hash
		}
		public TinyEncryption(string source, uint deltaOverride, uint[] key)
		{
			delta = deltaOverride;
			Value = FromString(source);
			Key = key;
			//Hash = Encrypt(Source, Key);
		}
		//v is the data and k is the key
		//the value for delta is taken from the documentation for tiny encryption algorithm
		public string Encrypt()
		{
			uint[] v = Value;
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
		//turns a key into 
		public string ToString(uint[] Unsigned)
		{
			string Result = "";
			foreach (uint value in Unsigned)//works for long strings, but methods only address the first 128 bits as a key
			{
				byte[] byteArray = BitConverter.GetBytes(value);
				foreach (byte b in byteArray)//loops through
				{
					Result = Result + ((char)b); // Only works for ascii characters - throw error if unicode is used
				}
			}
			return Result;

		}
		//used to turn a user password into a key that can be accepted by the algorithm
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
		// GemerateSalt() creates an 128 bit encryption salt
		public uint[] GenerateSalt()
		{		
			//used to create a pseudo random seed that can be used to add randomness to user keys
			//stored SECURELY per-user - they need BOTH the key AND the salt to login
			uint[] result = new uint[4]; //filled with random bytes -> uints 
			result[0] = Convert.ToUInt32(RandomNumberGenerator.GetBytes(4));
			result[1] = Convert.ToUInt32(RandomNumberGenerator.GetBytes(4));
			result[2] = Convert.ToUInt32(RandomNumberGenerator.GetBytes(4));
			result[3] = Convert.ToUInt32(RandomNumberGenerator.GetBytes(4));
			return result;
		}
		//the user provides a key and a salt, to which xor is applied to create a more entropic key
		public uint[] ApplySalt(string userkey, uint[] salt)
		{
			//bitwise xor on each 8 bit uint 
			uint[] key = FromString(userkey);
			uint[] saltedKey = new uint[4];
			int i = 0;
			foreach (uint b in key)
			{
				foreach (uint c in salt)
				{
					saltedKey[i] = c ^ b;
					i++;
				}
			}
			return saltedKey;
		}
		// reversible quality of xor - as A = B XOR C implies B = A XOR C
		//this means the salted key and user key (the password) can be used to find the original salt
		public uint[] UndoSalt(uint[] key, uint[] saltedKey)
		{
			uint[] salt = new uint[4];
			int i = 0;
			foreach (uint b in key)
			{
				foreach (uint c in saltedKey)
				{
					salt[i] = c ^ b;
					i++;
				}
			}
			return salt;
		}
		public uint[] GetSalt(string User)
		{
			string Salt = "";
			uint[] UserKey = new uint[4];
			//use a SELECT statement to retrieve the stored per-user salt
			uint[] SaltedKey = UndoSalt(FromString(Salt), UserKey);
			return SaltedKey;
		}
		static void StoreSalt(SqlConnection conn)
		{
			//use an INSERT INTO statement to write to the User table in the DB
			//the DDL needs to be updated to include this feature
		}
	}
}

