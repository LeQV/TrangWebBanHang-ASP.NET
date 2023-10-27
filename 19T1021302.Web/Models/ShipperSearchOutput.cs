using _19T1021302.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021302.Web.Models
{
    public class ShipperSearchOutput : PaginationSearchOutput
    {
        /// <summary>
        /// Dữ liệu về Shipper
        /// </summary>
        public List<Shipper> Data { get; set; }
    }
}