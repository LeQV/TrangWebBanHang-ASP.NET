using _19T1021302.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021302.Web.Models
{
    public class OrderSearchOutput :PaginationSearchOutput
    {
        public int Status { get; set; }
        public List<Order> Data { get; set; }
    }
}