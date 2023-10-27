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
    public class CategoryController : Controller
    {
        const int PAGE_SIZE = 5;
        const string CATEGORY_SEARCH = "CategorySearch";
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //public ActionResult Index(int page=1, string searchValue="")
        //{
        //    int rowCount = 0;
        //    var data = CommonDataService.ListOfCategories(page, PAGE_SIZE, searchValue, out rowCount);
        //    int pageCount = rowCount / PAGE_SIZE;
        //    if (rowCount % PAGE_SIZE > 0)
        //    {
        //        pageCount += 1;
        //    }
        //    ViewBag.SearchValue = searchValue;
        //    ViewBag.Page = page;
        //    ViewBag.PageCount = pageCount;
        //    ViewBag.RowCount = rowCount;
        //    return View(data);
        //}
        public ActionResult Index()
        {
            Models.PaginationSearchInput condition = Session[CATEGORY_SEARCH] as Models.PaginationSearchInput;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ActionResult Search(Models.PaginationSearchInput condition)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfCategories(condition.Page, condition.PageSize,
                                                                           condition.SearchValue,
                                                                           out rowCount);
            Models.CategorySearchOutput result = new Models.CategorySearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data
            };

            Session[CATEGORY_SEARCH] = condition;
            return View(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Title = "bổ sung loại hàng";
            var data = new Category()
            {
                CategoryID = 0
            };
            return View("Edit",data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Save(Category data)
        {
            if (string.IsNullOrEmpty(data.CategoryName))
                ModelState.AddModelError(nameof(data.CategoryName), "Tên không được để trống");
            if (string.IsNullOrEmpty(data.Description))
                ModelState.AddModelError(nameof(data.Description), "Mô tả không được để trống");
            if(!ModelState.IsValid)
            {
                ViewBag.Title = data.CategoryID == 0 ? "bổ sung loại hàng" : "Cập nhật loại hàng";
                return View("Edit", data);
            }
            if (data.CategoryID == 0)
            {
                CommonDataService.AddCategory(data);
            }
            else
            {
                CommonDataService.UpdateCategory(data);
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id=0)
        {
            if (id <= 0)
                return RedirectToAction("Index");
            var data = CommonDataService.GetCategory(id);
            if (data == null)
                return RedirectToAction("Index");
            ViewBag.Title = "Cập nhật loại hàng";
            return View("Edit", data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int id=0)
        {
            if (id <= 0)
                return RedirectToAction("Index");
            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteCategory(id);
                return RedirectToAction("Index");
            }
            var data = CommonDataService.GetCategory(id);
            if (data == null)
                return RedirectToAction("Index");
            return View(data);
        }
    }
}