using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _19T1021302.Web.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        // GET: CustomerAccount
       
        public ActionResult Index()
        {
            return View();
        }
        
    }
}