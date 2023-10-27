using _19T1021302.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace _19T1021302.Datalayers.SQLServer
{
    /// <summary>
    /// 
    /// </summary>
    public class CountryDAL : _BaseDAL, ICountryDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public CountryDAL(string connectionString) : base(connectionString)
        {
        }
 
        /// <summary>
        /// Lấy về danh sách tất cả quốc gia
        /// </summary>
        /// <returns></returns>
        public IList<Country> List()
        {
            List<Country> data = new List<Country>();
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT * FROM Countries";
                cmd.CommandType = CommandType.Text;

                SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dbReader.Read())
                {
                    data.Add(new Country()
                    {
                        CountryName = Convert.ToString(dbReader["CountryName"])
                    }) ;
                }
                dbReader.Close();
                cn.Close();
            }
            return data;
        }
    }
}
