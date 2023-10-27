using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021302.DomainModels;
using System.Configuration;
using _19T1021302.Datalayers;

namespace _19T1021302.BusinessLayers
{
    /// <summary>
    /// Cung cấp các chức năng nghiệp vụ xử lí dữ liệu chung liên quan đến :
    /// Quốc gia, Nhà cung cấp, Khách hàng, Người giao hàng, Nhân viên, Loại hàng.
    /// </summary>
    public static class CommonDataService
    {
        private static readonly ICountryDAL countryDB;
        private static ICommonDAL<Supplier> supplierDB;
        private static ICommonDAL<Customer> customerDB;
        private static ICommonDAL<Shipper> shipperDB;
        private static ICommonDAL<Employee> employeeDB;
        private static ICommonDAL<Category> categoryDB;


        /// <summary>
        /// Ctor
        /// </summary>
        static CommonDataService()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            categoryDB = new Datalayers.SQLServer.CategoryDAL(connectionString);
            supplierDB = new Datalayers.SQLServer.SupplierDAL(connectionString);
            countryDB = new Datalayers.SQLServer.CountryDAL(connectionString);
            customerDB = new Datalayers.SQLServer.CustomerDAL(connectionString);
            shipperDB = new Datalayers.SQLServer.ShipperDAL(connectionString);
            employeeDB = new Datalayers.SQLServer.EmployeeDAL(connectionString);
        }
        #region Xử lí liên quan đến quốc gia
        /// <summary>
        /// Lấy danh sách quốc gia
        /// </summary>
        /// <returns></returns>
        public static List<Country> ListOfCountries()
        {
            return countryDB.List().ToList();
        }
        #endregion

        #region Xử lí liên quan đến Nhà cung cấp
        /// <summary>
        /// Tìm kiếm, lấy danh sách và phân trang Nhà cung cấp
        /// </summary>
        /// <param name="page">Trang cần hiển thị</param>
        /// <param name="pageSize">Số dòng trên mỗi trang (0 nếu </param>
        /// <param name="searchValue">Giá trị tìm kiếm</param>
        /// <param name="rowCount">Tham số đầu ra: Số dòng dữ lệu tìm kiếm được</param>
        /// <returns></returns>
        public static List<Supplier> ListOfSupperliers(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = supplierDB.Count(searchValue);
            return supplierDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Tìm kiếm, lấy danh sách Nhà cung cấp
        /// </summary>
        /// <param name="searchValue">Giá trị tìm kiếm (rỗng sẽ lấy tất cả dữ liệu về Nhà cung cấp)</param>
        /// <returns></returns>     
        public static List<Supplier> ListOfSupperliers(string searchValue = "")
        {
            return supplierDB.List(1, 0, searchValue).ToList();
        }
        /// <summary>
        /// Lấy thông tin của 1 Nhà cung cấp
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static Supplier GetSupplier(int supplierID)
        {
            return supplierDB.Get(supplierID);
        }

        /// <summary>
        /// Thêm Nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddSupplier(Supplier data)
        {
            return supplierDB.Add(data);
        }
        /// <summary>
        /// Cập nhật Nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateSupplier(Supplier data)
        {
            return supplierDB.Update(data);
        }

        /// <summary>
        /// Xóa Nhà cung cấp
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static bool DeleteSupplier(int supplierID)
        {
            return supplierDB.Delete(supplierID);
        }

        /// <summary>
        /// Kiểm tra xem thông tin của 1 Nhà cung cấp hiện có dữ liệu liên quan hay không
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static bool InUsedSupplier(int supplierID)
        {
            return supplierDB.InUsed(supplierID);
        }
        #endregion

        #region Xử lí liên quan đến Khách hàng
        /// <summary>
        /// Tìm kiếm, lấy danh sách và phân trang Khách hàng
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Customer> ListOfCustomers(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = customerDB.Count(searchValue);
            return customerDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Tìm kiếm, lấy danh sách Nhà cung cấp
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Customer> ListOfCustomers(string searchValue = "")
        {
            return customerDB.List(1, 0, searchValue).ToList();
        }
        /// <summary>
        /// Lấy thông tin của 1 Khách hàng
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static Customer GetCustomer(int customerID)
        {
            return customerDB.Get(customerID);
        }
        /// <summary>
        /// Thêm thông tin 1 Khách hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddCustomer(Customer data)
        {
            return customerDB.Add(data);
        }
        /// <summary>
        /// Cập nhật Khách hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateCustomer(Customer data)
        {
            return customerDB.Update(data);
        }
        /// <summary>
        /// Xóa Khách hàng
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static bool DeleteCustomer(int customerID)
        {
            return customerDB.Delete(customerID);
        }
        /// <summary>
        /// Kiểm tra xem 1 Khách hàng hiện có dữ liệu liên quan hay không
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static bool InUsedCustomer(int customerID)
        {
            return customerDB.InUsed(customerID);
        }
        #endregion

        #region Xử lí liên quan đến loại hàng

        /// <summary>
        /// Tìm kiếm, lấy danh sách và phân trang Loại hàng
        /// </summary>
        /// <param name="page">Trang cần hiển thị</param>
        /// <param name="pageSize">Số dòng trên mỗi trang (0 nếu </param>
        /// <param name="searchValue">Giá trị tìm kiếm</param>
        /// <param name="rowCount">Tham số đầu ra: Số dòng dữ lệu tìm kiếm được</param>
        /// <returns></returns>
        public static List<Category> ListOfCategories(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = categoryDB.Count(searchValue);
            return categoryDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Tìm kiếm, lấy danh sách Loại hàng
        /// </summary>
        /// <param name="searchValue">Giá trị tìm kiếm (rỗng sẽ lấy tất cả dữ liệu về Nhà cung cấp)</param>
        /// <returns></returns>     
        public static List<Category> ListOfCategories(string searchValue = "")
        {
            return categoryDB.List(1, 0, searchValue).ToList();
        }
        /// <summary>
        /// Lấy Loại hàng
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public static Category GetCategory(int categoryID)
        {
            return categoryDB.Get(categoryID);
        }

        /// <summary>
        /// Thêm Loại hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddCategory(Category data)
        {
            return categoryDB.Add(data);
        }
        /// <summary>
        /// Cập nhật Loại hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateCategory(Category data)
        {
            return categoryDB.Update(data);
        }

        /// <summary>
        /// Xóa Loại hàng
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public static bool DeleteCategory(int categoryID)
        {
            return categoryDB.Delete(categoryID);
        }

        /// <summary>
        /// Kiểm tra Loại hàng có đang được sử dụng không
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public static bool InUsedCategory(int categoryID)
        {
            return categoryDB.InUsed(categoryID);
        }
        #endregion

        #region Xử lí liên quan đến nhân viên
        /// <summary>
        /// Tìm kiếm, lấy danh sách và phân trang Nhân viên
        /// </summary>
        /// <param name="page">Trang cần hiển thị</param>
        /// <param name="pageSize">Số dòng trên mỗi trang (0 nếu </param>
        /// <param name="searchValue">Giá trị tìm kiếm</param>
        /// <param name="rowCount">Tham số đầu ra: Số dòng dữ lệu tìm kiếm được</param>
        /// <returns></returns>
        public static List<Employee> ListOfEmployees(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = employeeDB.Count(searchValue);
            return employeeDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Tìm kiếm, lấy danh sách Nhân viên
        /// </summary>
        /// <param name="searchValue">Giá trị tìm kiếm (rỗng sẽ lấy tất cả dữ liệu về Nhà cung cấp)</param>
        /// <returns></returns>     
        public static List<Employee> ListOfEmployees(string searchValue = "")
        {
            return employeeDB.List(1, 0, searchValue).ToList();
        }
        /// <summary>
        /// Lấy thông tin của 1 Nhân viên
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public static Employee GetEmployee(int employeeID)
        {
            return employeeDB.Get(employeeID);
        }

        /// <summary>
        /// Thêm Nhân viên
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddEmployee(Employee data)
        {
            return employeeDB.Add(data);
        }
        /// <summary>
        /// Cập nhật Nhân viên
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateEmployee(Employee data)
        {
            return employeeDB.Update(data);
        }

        /// <summary>
        /// Xóa Nhân viên
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public static bool DeleteEmployee(int employeeID)
        {
            return employeeDB.Delete(employeeID);
        }

        /// <summary>
        /// Kiểm tra xem thông tin của 1 Nhân viên có hiện có dữ liệu liên quan hay không
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public static bool InUsedEmployye(int employeeID)
        {
            return employeeDB.InUsed(employeeID);
        }

        #endregion

        #region Xử lí liên quan đến Shipper
        /// <summary>
        /// Tìm kiếm, lấy danh sách và phân trang Shipper
        /// </summary>
        /// <param name="page">Trang cần hiển thị</param>
        /// <param name="pageSize">Số dòng trên mỗi trang (0 nếu </param>
        /// <param name="searchValue">Giá trị tìm kiếm</param>
        /// <param name="rowCount">Tham số đầu ra: Số dòng dữ lệu tìm kiếm được</param>
        /// <returns></returns>
        public static List<Shipper> ListOfShippers(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = shipperDB.Count(searchValue);
            return shipperDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Tìm kiếm, lấy danh sách Shipper
        /// </summary>
        /// <param name="searchValue">Giá trị tìm kiếm (rỗng sẽ lấy tất cả dữ liệu về Nhà cung cấp)</param>
        /// <returns>Danh sách Shipper không phân trang</returns>     
        public static List<Shipper> ListOfShippers(string searchValue = "")
        {
            return shipperDB.List(1, 0, searchValue).ToList();
        }
        /// <summary>
        /// Lấy thông tin của 1 Shipper
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        public static Shipper GetShipper(int shipperID)
        {
            return shipperDB.Get(shipperID);
        }

        /// <summary>
        /// Thêm Shipper
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddShipper(Shipper data)
        {
            return shipperDB.Add(data);
        }
        /// <summary>
        /// Cập nhật Shipper
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateShipper(Shipper data)
        {
            return shipperDB.Update(data);
        }

        /// <summary>
        /// Xóa Shipper
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        public static bool DeleteShipper(int shipperID)
        {
            return shipperDB.Delete(shipperID);
        }

        /// <summary>
        /// Kiểm tra xem thông tin Shipper có dữ liệu liên quan hay không
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        public static bool InUsedShipper(int shipperID)
        {
            return shipperDB.InUsed(shipperID);
        }
        #endregion
    }
}
