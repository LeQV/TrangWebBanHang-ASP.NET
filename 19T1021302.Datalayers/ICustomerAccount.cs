using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021302.DomainModels;

namespace _19T1021302.Datalayers
{
	public interface ICustomerAccount
	{	
		CusAccount Authorize(string email);
		string GetHashedPassword(string email);
		bool ChangePassword(string userName, string oldPassword, string newPassword);

		bool ExistAccount(string key);

		string ReturnAccountInformation(string email);

		int Register(CusAccount user);
	}
}
