﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace NoriyWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Noriy";
            return View();
        }
        
    }
}
