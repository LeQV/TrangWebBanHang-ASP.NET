using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _19T1021302.DomainModels;
using _19T1021302.BusinessLayers;
using System.Web.Mvc;

namespace _19T1021302.Web
{
    public static class SelectListHelper
    {
        /// <summary>
        /// Trả về danh sách quốc gia
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> Countries()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value="",
                Text="--Chọn quốc gia--"
            });
            foreach(var item in CommonDataService.ListOfCountries())
            {
                list.Add(new SelectListItem()
                {
                    Value=item.CountryName,
                    Text=item.CountryName
                });
            }
            return list;
        }
        public static List<SelectListItem> Categories()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value ="",
                Text = "--Chọn loại hàng--"
            });
            foreach(var item in CommonDataService.ListOfCategories())
            {
                list.Add(new SelectListItem()
                {
                    Value= item.CategoryID.ToString(),
                    Text=item.CategoryName
                });
            }
            return list;
        }
        public static List<SelectListItem> Suppliers()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value ="",
                Text = "--Chọn nhà cung cấp--"
            });
            foreach (var item in CommonDataService.ListOfSupperliers())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.SupplierID.ToString(),
                    Text = item.SupplierName
                });
            }
            return list;
        }
        //public static List<SelectListItem> Status()
        //{
        //    List<SelectListItem> list = new List<SelectListItem>();
        //    list.Add(new SelectListItem()
        //    {
        //        Value= "-99",
        //        Text="--Trạng thái"
        //    });
        //    foreach(var item in  )
        //}
    }
}