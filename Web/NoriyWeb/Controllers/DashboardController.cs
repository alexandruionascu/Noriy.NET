using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace NoriyWeb.Controllers
{
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Traffic()
        {

          ViewBag.UserId = User.Identity.GetUserId();
          ViewBag.UserName = User.Identity.GetUserName();
          return View();

        }
	}
}