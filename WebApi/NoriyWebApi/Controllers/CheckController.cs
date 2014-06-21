using NoriyWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace NoriyWebApi.Controllers
{
    public class CheckController : ApiController
    {
        private DbModelContainer db = new DbModelContainer();

        public CheckController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        public int Check(Blackurl blackUrl)
        {
            var result = db.Blackurls.Where(e => e.Url.Contains(blackUrl.Url));

            return result.Where(e => e.UserId == blackUrl.UserId).Count();

        }
	}
}