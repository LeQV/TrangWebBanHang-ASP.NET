using _19T1021302.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021302.Datalayers.SQLServer
{
    public class CustomerAccount : _BaseDAL, ICustomerAccount
    {
        public CustomerAccount(string connectionString) : base(connectionString)
        {
        }

		public CusAccount Authorize(string email)
		{
			CusAccount data = null;
			using (var connection = OpenConnection())
			{
				SqlCommand cmd = connection.CreateCommand();
				cmd.CommandText = "SELECT * FROM Customers WHERE Email = @Email";
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.AddWithValue("@Email", email);
				using(SqlDataReader dbReader = cmd.ExecuteReader())
				{
					if (dbReader.Read())
					{
						data = new CusAccount()
						{
							CustomerName = Convert.ToString(dbReader["CustomerName"]),
							ContactName = Convert.ToString(dbReader["ContactName"]),
							Address = Convert.ToString(dbReader["Address"]),
							PostalCode = Convert.ToString(dbReader["PostalCode"]),
							Country = Convert.ToString(dbReader["Country"]),
							Email = Convert.ToString(dbReader["Email"]),
							Confirmed = Convert.ToBoolean(dbReader["Confirmed"]),
						};
					}
					dbReader.Close();
				}
				connection.Close();
			}
			return data;
		}

		public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

		public bool ExistAccount(string email)
		{
			using (var connection = OpenConnection())
			{
				var cmd = connection.CreateCommand();
				cmd.CommandText = "SELECT COUNT(*) FROM Customers WHERE Email=@email";
				cmd.Parameters.AddWithValue("@email", email);
				cmd.CommandType = CommandType.Text;
				int result = (int)cmd.ExecuteScalar();
				connection.Close();
				if (result == 1)
					return true;
				return false;
			}
		}

		public string GetHashedPassword(string email)
		{
			using (var connection = OpenConnection())
			{
				string result = null;
				SqlCommand cmd = connection.CreateCommand();
				cmd.CommandText = "SELECT HashedPassword FROM Customers WHERE Email = @Email";
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.AddWithValue("@Email", email);
				result =(string) cmd.ExecuteScalar();
				
				connection.Close();
				return result;
			}
		}

		public int Register(CusAccount user)
		{
			int result = 0;
			using(var connection = OpenConnection())
			{
				SqlCommand cmd = connection.CreateCommand();
				cmd.CommandText = @"INSERT INTO Customers(CustomerName, ContactName, Address, City, PostalCode, Country,Email,HashedPassword,Confirmed)
                                    VALUES(@CustomerName, @ContactName, @Address, @City, @PostalCode, @Country, @Email, @HashedPassword, @Confirmed);
                                    SELECT SCOPE_IDENTITY()";
				cmd.CommandType = CommandType.Text;
				cmd.Parameters.AddWithValue("@CustomerName", user.CustomerName);
				cmd.Parameters.AddWithValue("@ContactName", user.ContactName);
				cmd.Parameters.AddWithValue("@Address", user.Address);
				cmd.Parameters.AddWithValue("@City", user.City);
				cmd.Parameters.AddWithValue("@PostalCode", user.PostalCode);
				cmd.Parameters.AddWithValue("@Country", user.Country);
				cmd.Parameters.AddWithValue("@Email", user.Email);
				cmd.Parameters.AddWithValue("@HashedPassword", user.HashedPassword);
				cmd.Parameters.AddWithValue("@Confirmed", false);

				result = Convert.ToInt32(cmd.ExecuteScalar());
				return result;
			}
		}


		public string ReturnAccountInformation(string email)
		{
			return null;
		}

		

		
	}
}
