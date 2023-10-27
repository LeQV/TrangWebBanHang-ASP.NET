using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021302.DomainModels;

namespace _19T1021302.Datalayers
{
    /// <summary>
    /// Định nghĩa phép xử lí dữ liệu liên quan đến quốc gia
    /// </summary>
    public interface ICountryDAL
    {
        /// <summary>
        /// Lấy danh sách quốc gia 
        /// </summary>
        /// <returns></returns>
        IList<Country> List();
    }
}
