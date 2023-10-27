using _19T1021302.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021302.Datalayers.SQLServer
{
    public class EmployeeAccount : _BaseDAL, IUserAccountDAL
    {
        public EmployeeAccount(string connectionString) : base(connectionString)
        {
        }

        public UserAccount Acthorize(string userName, string password)
        {
            UserAccount data = null;
            using(var connection = OpenConnection())
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Employees WHERE Email=@Email AND Password=@Password";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Email", userName);
                cmd.Parameters.AddWithValue("@Password", password);

                using (var dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new UserAccount()
                        {
                            UserID = Convert.ToString(dbReader["EmployeeID"]),
                            UserName = Convert.ToString(dbReader["Email"]),
                            FullName = $"{dbReader["FirstName"]} {dbReader["LastName"]}",
                            Email = Convert.ToString(dbReader["Email"]),
                            Password = "",
                            RoleNames = "",
                            Photos=Convert.ToString(dbReader["Photo"])
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
            using (var connection = OpenConnection())
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = "UPDATE Employees SET Password=@NewPassword WHERE Email=@Email AND Password=@OldPassword ";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Email", userName);
                cmd.Parameters.AddWithValue("@NewPassword", newPassword);
                cmd.Parameters.AddWithValue("@OldPassword", oldPassword);
                var result= cmd.ExecuteNonQuery();
                connection.Close();
                if (result == 1)
                    return true;
                return false;
            }           
        }

		public bool ExistAccount(string key)
		{
			using(var connection = OpenConnection())
			{
                var cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM Employees WHERE Email=@key";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@key", key);
				int result = (int)cmd.ExecuteScalar();
                connection.Close();
                if (result == 1)
                    return true;
                return false;
            }
		}

		public bool Register(UserAccount user)
		{
			throw new NotImplementedException();
		}

		public string ReturnAccountInformation(string email)
        {
            UserAccount data = null;
            using (var connection = OpenConnection())
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Employees WHERE Email=@email";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@email", email);
                using (var dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new UserAccount()
                        {
                            Email = Convert.ToString(dbReader["Email"]),
                            Password = Convert.ToString(dbReader["Password"])
                        };                  
                    }
                    dbReader.Close();
                }
                connection.Close();
                string information = data.Email + " " + data.Password;
                return information;
            }
        }      
	}
}
