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
    public class EmployeeController : Controller
    {
        const int PAGE_SIZE = 5;
        const string EMPLOYEE_SEARCH = "EmployeeSearch";
        /// <summary>
        /// 
        /// 
        /// 
        /// </summary>
        /// <returns></returns>
        //public ActionResult Index(int page=1, string searchValue="")
        //{
        //    int rowCount = 0;
        //    var data = CommonDataService.ListOfEmployees(page, PAGE_SIZE, searchValue, out rowCount);
        //    int pageCount = rowCount / PAGE_SIZE;
        //    if (rowCount % PAGE_SIZE > 0)
        //        pageCount += 1;
        //    ViewBag.Page = page;
        //    ViewBag.RowCount = rowCount;
        //    ViewBag.PageCount = pageCount;
        //    ViewBag.SearchValue = searchValue;
        //    return View(data);
        //}
        public ActionResult Index()
        {
            Models.PaginationSearchInput condition = Session[EMPLOYEE_SEARCH] as Models.PaginationSearchInput;
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
            var data = CommonDataService.ListOfEmployees(condition.Page, condition.PageSize,
                                                                           condition.SearchValue,
                                                                           out rowCount);
            Models.EmployeeSearchOutput result = new Models.EmployeeSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data
            };

            Session[EMPLOYEE_SEARCH] = condition;
            return View(result);
        }
        public ActionResult Create()
        {
            var data = new Employee()
            {
                EmployeeID = 0
            };
            ViewBag.Title = "bổ sung nhân viên";
            return View("Edit", data);
        }
        public ActionResult Edit(int id = 0)
        {
            if (id <= 0)
                return RedirectToAction("Index");
            var data = CommonDataService.GetEmployee(id);
            if (data == null)
                return RedirectToAction("Index");
            ViewBag.Title = "Cập nhật nhân viên";
            return View(data);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Save(Employee model,string birthdate, HttpPostedFileBase uploadPhoto)/// dùng multipart-formdata de truyen file : để trong form method    
        {
            var d = Models.Converter.DMYStringToDateTime(birthdate);

            if (d == null)
                ModelState.AddModelError("BirthDate", " ");
            else
                model.BirthDate = d.Value;
            if (string.IsNullOrEmpty(model.LastName))
                ModelState.AddModelError(nameof(model.LastName), "Tên không được để trống");
            if (string.IsNullOrEmpty(model.FirstName))
                ModelState.AddModelError(nameof(model.FirstName), "Họ không được để trống");
            if (model.BirthDate > DateTime.UtcNow)
                ModelState.AddModelError(nameof(model.BirthDate), "Ngày sinh không hợp lệ");
            model.Email = model.Email ?? "";
            model.Notes = model.Notes ?? "";
            if (uploadPhoto != null)
            {
                string path = Server.MapPath("~/Images/Employees");
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                string filePath = System.IO.Path.Combine(path, fileName);
                uploadPhoto.SaveAs(filePath);
                model.Photo = $"Images/Employees/{fileName}";
            }
            if (ModelState.IsValid == false)
            {
                ViewBag.Title = model.EmployeeID == 0 ? "bổ sung nhân viên" : "Cập nhật nhân viên";
                return View("Edit", model);
            }
           

            if (model.EmployeeID == 0)
            {
                CommonDataService.AddEmployee(model);
            }
            else
            {
                CommonDataService.UpdateEmployee(model);
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
                CommonDataService.DeleteEmployee(id);
                return RedirectToAction("Index");
            }
            var data = CommonDataService.GetEmployee(id);
            if (data == null)
                return RedirectToAction("Index");
            return View(data);
        }
    }
}