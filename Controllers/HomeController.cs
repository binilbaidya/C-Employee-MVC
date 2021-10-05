using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUDEmployeeMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Brief information about this website.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "You can meet us at:";

            return View();
        }
    }
}