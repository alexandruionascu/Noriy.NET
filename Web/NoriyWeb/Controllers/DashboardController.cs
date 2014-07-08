using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Facebook;

namespace NoriyWeb.Controllers
{
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/
        public ActionResult Index()
        {
            ViewBag.UserId = User.Identity.GetUserId();
            ViewBag.UserName = User.Identity.GetUserName();
            return View();
        }

        public ActionResult Traffic()
        {

          ViewBag.UserId = User.Identity.GetUserId();
          ViewBag.UserName = User.Identity.GetUserName();
          return View();

        }

        public ActionResult MyBlacklist()
        {
            ViewBag.UserId = User.Identity.GetUserId();
            ViewBag.UserName = User.Identity.GetUserName();
            return View();
        }

        public ActionResult Categories()
        {
            ViewBag.UserId = User.Identity.GetUserId();
            ViewBag.UserName = User.Identity.GetUserName();
            return View();
        }

        public ActionResult Social()
        {
            ViewBag.UserId = User.Identity.GetUserId();
            ViewBag.UserName = User.Identity.GetUserName();

          //  FacebookClient client = new FacebookClient("CAAUmbWJBU7MBAOKAm0UZAQAjGfUQdWtZC9rUnxjMPZB9Ho6FsLZC7t8hge0tzUCPHLK74oXNhwi4t6gmDCzz5tZAnwHxUXR6LSdnuoC9j4VcG5ewT0uOtTJkXxIqEuQTWlmfxX7FYXL1U1iGBvbgNRgeVYZBLryMrkopLglRb2BCvt01hZASZBMLU9WFSaIWUTsZD");
         //   dynamic result = client.Get("me/", new { fields = "name,id" });
         //   ViewBag.id = result.ToString();
            return View();
        }

        public ActionResult Friends()
        {
            ViewBag.UserId = User.Identity.GetUserId();
            ViewBag.UserName = User.Identity.GetUserName();
            return View();
        }

	}
}