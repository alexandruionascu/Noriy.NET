using Noriy_LocalDb.Models;
using NoriyWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NoriyWeb.Controllers
{
    public class RatingController : ApiController
    {
        private NoriyEntities db = new NoriyEntities();
        public IHttpActionResult Rate([FromUri]string address)
        {
            //noriy rating formula variables
            var isUrlBlacklisted = db.UrlBlacklists.Where(e => e.Url.Contains(address));
            double categoryNum = db.CategoryLinks.Where(e => e.CategoryId == isUrlBlacklisted.FirstOrDefault().CategoryId).Count();
            double blacklistNum = db.Blackurls.Where(e => e.Url.Contains(address)).Count();
            double noOfUsers = db.AspNetUsers.Count();
            //the variables of getting the score, score meaning the sum of rates, somewhere between 0 and 3
            double UrlBlacklistRate, CategoryRate, BlacklistRate;

            if (isUrlBlacklisted.Count() == 0)
            {
                UrlBlacklistRate = 0;
            }
            else
                UrlBlacklistRate = 1;

            CategoryRate = (categoryNum / noOfUsers) ;
            BlacklistRate = (blacklistNum / noOfUsers);
            //rating = score * 100 / 3
            var noriyRating = (UrlBlacklistRate + CategoryRate + BlacklistRate)*100/3;

            return Ok(100-noriyRating);
            
        }

    }
}
