using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoriyWeb.Controllers
{
    public class WelcomeController : Controller
    {
        //
        // GET: /Welcome/
        public ActionResult Index()
        {
            ViewBag.Title = "Noriy";
            return File("index.html","text/html");
        }
	}
}