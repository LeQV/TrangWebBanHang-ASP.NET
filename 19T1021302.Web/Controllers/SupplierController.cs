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
    public class SupplierController : Controller
    {
        private const int PAGE_SIZE = 5;
        private const string SUPPLIER_SEARCH = "SupplierSearch";
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //public actionresult index(int page=1, string searchvalue="")
        //{
        //    int rowcount = 0;
        //    var data = commondataservice.listofsupperliers(page,page_size,searchvalue, out rowcount);
        //    int pagecount = rowcount / page_size;
        //    if (rowcount % page_size > 0)
        //        pagecount += 1;
        //    viewbag.page = page;
        //    viewbag.rowcount = rowcount;
        //    viewbag.pagecount = pagecount;
        //    viewbag.searchvalue = searchvalue;
        //    return view(data);// truyền dữ liệu bằng model
        //}

        public ActionResult Index()
        {
            Models.PaginationSearchInput condition = Session[SUPPLIER_SEARCH] as Models.PaginationSearchInput;
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
            var data = CommonDataService.ListOfSupperliers(condition.Page, condition.PageSize,
                                                                           condition.SearchValue,
                                                                           out rowCount);
            Models.SupplierSearchOutput result = new Models.SupplierSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data
            };

            Session[SUPPLIER_SEARCH] = condition;
            return View(result);
        }
        public ActionResult Create()
        {
            var data = new Supplier()
            {
                SupplierID = 0
            };
            ViewBag.Title = "bổ sung nhà cung cấp";
            return View("Edit", data);
        }
        //public bool Update(int id, string supplierName, string contactName, string address, string phone, string country, string city, string postalCode)
        //{
        //    var data = new Supplier
        //    {
        //        SupplierID = id,
        //        SupplierName = supplierName,
        //        ContactName = contactName,
        //        Address = address,
        //        Phone = phone,
        //        Country = country,
        //        City = city,
        //        PostalCode = postalCode
        //    };
        //    return CommonDataService.UpdateSupplier(data);
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id = 0)
        {
            if (id <= 0)
                return RedirectToAction("Index");
            var data = CommonDataService.GetSupplier(id);
            if (data == null)
                return RedirectToAction("Index");
            ViewBag.Title = "Cập nhật nhà cung cấp";
            return View(data);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Save(Supplier data)
        {
            if (string.IsNullOrEmpty(data.SupplierName))
                ModelState.AddModelError(nameof(data.SupplierName), "Tên không được để trống");
            if (string.IsNullOrEmpty(data.ContactName))
                ModelState.AddModelError(nameof(data.ContactName), "Tên giao dịch không được để trống");
            if (string.IsNullOrWhiteSpace(data.Country))
                ModelState.AddModelError(nameof(data.Country), "Vui lòng chọn quốc gia");
            data.Address = data.Address ?? "";
            data.Phone = data.Phone ?? "";
            data.City = data.City ?? "";
            data.PostalCode = data.PostalCode ?? "";

            if (ModelState.IsValid == false)
            {
                ViewBag.Title = data.SupplierID == 0 ? "bổ sung nhà cung cấp" : "Cập nhật nhà cung cấp";
                return View("Edit",data);
            }
            if (data.SupplierID == 0)
            {
                CommonDataService.AddSupplier(data);
            }
            else
            {
                CommonDataService.UpdateSupplier(data);
            }
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
                CommonDataService.DeleteSupplier(id);
                return RedirectToAction("Index");
            }
            var data = CommonDataService.GetSupplier(id);
            if (data == null)
                return RedirectToAction("Index");
            return View(data);
        }
    }
}