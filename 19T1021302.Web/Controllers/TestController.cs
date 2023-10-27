using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _19T1021302.Web.Controllers
{
    [RoutePrefix("thu-nghiem")]
    public class TestController : Controller
    {
      //[Route("xin-chao/{name?}/{age?}")]
        public string Hello(string name="", int age=-1)
        {
            return $"Hello {name}, {age} years old";
        }


        public ActionResult Testngay(string str)
        {
            var date = Models.Converter.DMYStringToDateTime(str);
            ViewBag.date = date;
            return View();
        }
    }
}