using _19T1021302.Datalayers;
using _19T1021302.DomainModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021302.BusinessLayers
{
	public class CustomerAccountService
	{
		private static ICustomerAccount customerAccountDB;
		static CustomerAccountService()
		{
			string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
			customerAccountDB = new Datalayers.SQLServer.CustomerAccount(connectionString);
		}
		public static bool ExistAccountCustomer(string email)
		{
			return customerAccountDB.ExistAccount(email);
		}
		public static int RegisterAccount(CusAccount data)
		{
			return customerAccountDB.Register(data);
		}
		public static string GetHashedPassword(string email)
		{
			return customerAccountDB.GetHashedPassword(email);
		}
		public static CusAccount Authorize(string email)
		{
			CusAccount cusAccount = null;
			cusAccount= customerAccountDB.Authorize(email);
			return cusAccount;
		}
	}
}
