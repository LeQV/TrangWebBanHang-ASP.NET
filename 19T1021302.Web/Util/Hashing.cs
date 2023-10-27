using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BCrypt.Net;
namespace _19T1021302.Web.Util
{
	public class Hashing
	{
		public static string GetRandomSalt()
		{
			return BCrypt.Net.BCrypt.GenerateSalt(12);
		}
		public static string HashPassword(string password)
		{
			return BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());
		}
		public static bool ValidatePassword(string password, string correctHash)
		{
			return BCrypt.Net.BCrypt.Verify(password,correctHash);
		}
	}
}