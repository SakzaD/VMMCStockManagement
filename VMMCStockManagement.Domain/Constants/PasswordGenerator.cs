using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMMCStockManagement.Domain.Constants
{
	public static class PasswordGenerator
	{
		const string LOWER_CASE = "abcdefghijklmnopqrstuvwxyz";
		const string UPPER_CASE = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		const string NUMBERS = "123456789";
		const string SPECIALS = @"!@£$%^&*()#€";

		public static string GeneratePassword(bool useLowercase,  bool useUppercase, bool useNumbers, bool useSpecial, int passwordSize)
		{
			char[] _password = new char[passwordSize];
			string charSet = "";
			Random _random = new Random();
			int counter;

			if(useLowercase) charSet += LOWER_CASE;

			if (useUppercase) charSet += UPPER_CASE;

			if (useNumbers) charSet += NUMBERS;

			if (useSpecial) charSet += SPECIALS;

			for (counter = 0; counter < passwordSize; counter ++)
			{
				_password[counter] = charSet[_random.Next(charSet.Length - 1)];
			}

			if (useSpecial)
			{
				bool hasSpecial = false;

				foreach (char sp in SPECIALS)
				{ 
					foreach(char c in _password)
					{
						if (c == sp) hasSpecial = true; break;
					}
					
					if (hasSpecial) {
						break; 
					}
				}
				if (!hasSpecial)
				{
					var newPassword = GeneratePassword(useLowercase, useUppercase, useNumbers, useSpecial, passwordSize);
					return newPassword;
				}
			}

			return String.Join(null, _password);

		}

	}
}
