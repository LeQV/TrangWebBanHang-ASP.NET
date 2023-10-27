using _19T1021302.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021302.Web.Models
{
    public class ProductSearchInput : PaginationSearchInput
    {
        public int SupplierID { get; set; }
        public int CategoryID { get; set; }
        public List<Category> ListOfCategory { get; set; }
    }
}