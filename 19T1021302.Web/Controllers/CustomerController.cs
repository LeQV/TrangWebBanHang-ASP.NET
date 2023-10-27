using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19T1021302.DomainModels;
using _19T1021302.Datalayers;
using _19T1021302.BusinessLayers;
namespace _19T1021302.Web.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private const int PAGE_SIZE = 5;
        private const string CUSTOMER_SEARCH = "CustomerSearch";

        //public ActionResult Index(int page=1,string searchValue="")
        //{
        //    int rowCount = 0;
        //    var data = CommonDataService.ListOfCustomers(page,PAGE_SIZE,searchValue, out rowCount);
        //    int pageCount = rowCount / PAGE_SIZE;
        //    if (rowCount % PAGE_SIZE > 0)
        //        pageCount += 1;
        //    ViewBag.Page = page;
        //    ViewBag.PageCount = pageCount;
        //    ViewBag.RowCount = rowCount;
        //    ViewBag.SearchValue = searchValue;
        //    return View(data);
        //}

        public ActionResult Index()
        {
            Models.PaginationSearchInput condition = Session[CUSTOMER_SEARCH] as Models.PaginationSearchInput;
            if (condition == null)
            {
                condition = new Models.PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = ""
                };
            }
            return View(condition);
        }
        public ActionResult Search(Models.PaginationSearchInput condition)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfCustomers(condition.Page, condition.PageSize, condition.SearchValue, out rowCount);
            var result = new Models.CustomerSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                RowCount = rowCount,
                SearchValue = condition.SearchValue,
                Data = data
            };
            Session[CUSTOMER_SEARCH] = condition;
            return View(result);
        }
        public ActionResult Create()
        {
            var data = new Customer()
            {
                CustomerID = 0
            };
            ViewBag.Title = "bổ sung khách hàng";
            return View("Edit",data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id = 0)
        {
            if (id <= 0)
                return RedirectToAction("Index");
            var data = CommonDataService.GetCustomer(id);
            if (data == null) 
                return RedirectToAction("Index");
            ViewBag.Title = "Cập nhật khách hàng";
            return View(data);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Save(Customer data)
        {
            if (string.IsNullOrEmpty(data.CustomerName))
                ModelState.AddModelError(nameof(data.CustomerName), "Tên không được để trống");
            if (string.IsNullOrEmpty(data.ContactName))
                ModelState.AddModelError(nameof(data.ContactName), "Tên giao dịch không được để trống");
            if (string.IsNullOrWhiteSpace(data.Country))
                ModelState.AddModelError(nameof(data.Country), "Vui lòng chọn quốc gia");
            data.PostalCode = data.PostalCode ?? "";
            data.Email = data.Email ?? "";
            data.Address = data.Address ?? "";
            data.City = data.City ?? "";
            if (!ModelState.IsValid)
            { 
                ViewBag.Title = data.CustomerID == 0 ? "bổ sung khách hàng" : "Cập nhật khách hàng";
                return View("Edit", data);
            }
            if (data.CustomerID == 0)            
                CommonDataService.AddCustomer(data);            
            else           
                CommonDataService.UpdateCustomer(data);           
            return RedirectToAction("Index");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int id = 0)
        {
            if (id <= 0)
                return RedirectToAction("Index");
            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteCustomer(id);
                return RedirectToAction("Index");
            }
            var data = CommonDataService.GetCustomer(id);
            if (data == null)
                return RedirectToAction("Index");
            return View(data);
        }
    }
}