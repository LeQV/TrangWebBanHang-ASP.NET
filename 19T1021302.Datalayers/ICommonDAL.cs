using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _19T1021302.Datalayers
{
    /// <summary>
    /// Định nghĩa các phép xư lí dữ liệu chung
    /// </summary>
    public interface ICommonDAL<T> where  T : class
    {
        /// <summary>
        /// Tìm kiếm và lấy danh sách dữ liệu dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần hiển thị</param>
        /// <param name="pageSize">Số dòng hiển thị trên trang(bằng 0 nếu không phân trang)</param>
        /// <param name="searchValue">Giá trị cần tìm (chuỗi tỗn nếu không tìm kiếm, tức là truy vấn toàn bộ dữ liệu) </param>
        /// <returns></returns>
        IList<T> List(int page = 1, int pageSize =0, string searchValue="");
        /// <summary>
        /// Đếm số dòng dữ liệu tìm được
        /// </summary>
        /// <param name="searchValue">Giá trị cần tìm (chuỗi tỗn nếu không tìm kiếm, tức là truy vấn toàn bộ dữ liệu)</param>
        /// <returns></returns>
        int Count(string searchValue = "");
        /// <summary>
        /// Lấy một dòng dữ liệu dựa vào id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(int id);
        /// <summary>
        /// Bổ sung dữ liệu vào CSDL
        /// </summary>
        /// <param name="data"></param>
        /// <returns>ID của kiểu dữ liệu vừa bổ sung</returns>
        int Add(T data);
        /// <summary>
        /// Cập nhật dữ liệu
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(T data);
        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>

        bool Delete(int id);

        bool InUsed(int id);
    }
}
