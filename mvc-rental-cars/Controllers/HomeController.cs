using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvc_rental_cars.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "MVC Rental Cars App";
            return View();
        }

        public ActionResult Contact()
        {
           ViewBag.Message = "Información de Contacto";
            return View();
        }
    }
}