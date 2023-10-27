using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _19T1021302.DomainModels;
namespace _19T1021302.Web.Models
{
    /// <summary>
    /// Lưu trữ kết quả tìm kiếm, phân trang đối với nhà cung cấp
    /// </summary>
    public class SupplierSearchOutput : PaginationSearchOutput
    {
        /// <summary>
        /// Dữ liệu nhà cung cấp
        /// </summary>
        public List<Supplier> Data { get; set; }
    }
}