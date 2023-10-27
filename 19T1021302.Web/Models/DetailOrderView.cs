using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _19T1021302.DomainModels;
namespace _19T1021302.Web.Models
{
    public class DetailOrderView : Order
    {
        public List<OrderDetail> ListProducts { get; set; }

    }
}