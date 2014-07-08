using NoriyWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NoriyWeb.Controllers
{
    [Authorize]
    public class StatisticsController : ApiController
    {
        private NoriyEntities db = new NoriyEntities();
        public IHttpActionResult GetStatistics([FromUri]string userId)
        {
            //Todo: do not forget lazy dumbass: http://localhost:8942/api/statistics?userid=0183277d-27c2-4401-abad-f98944c09cb9
            //TODO: one single function for all
            Statistics myStatistics = new Statistics {
                Blocked = db.Traffic.Where(e => e.UserId == userId).Where(e => e.Accepted == false).Count().ToString(),
                Blacklist = db.Blackurls.Where(e => e.UserId == userId).Count().ToString(),
                Traffic = db.Traffic.Where(e => e.UserId == userId).Count().ToString(),
                Categories = db.CategoryLinks.Where(e => e.UserId == userId).Count().ToString(),
                Alerts = db.Dangers.Where(e => e.UserId == userId).Count().ToString()

            };
            return Ok(myStatistics);

        }


    }
}
