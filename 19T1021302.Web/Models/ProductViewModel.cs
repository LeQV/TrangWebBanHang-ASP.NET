using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _19T1021302.DomainModels;
namespace _19T1021302.Web.Models
{
    public class ProductViewModel : Product
    {
        public  List<ProductAttribute> Attributes { get; set; }
        public List<ProductPhoto> Photos { get; set; }
    }
}