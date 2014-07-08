using NoriyWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NoriyWeb.Controllers
{
    public class CheckController : ApiController
    {
        private NoriyEntities db = new NoriyEntities();

        public CheckController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        public void Check(Blackurl blackUrl)
        {                
            //Check in UrlBlacklist
            var result = db.CategoryLinks.Where(e => e.UserId == blackUrl.UserId);
            if (result != null)
            {
                foreach (var categLink in result)
                {
                    var count = db.UrlBlacklists.Where(e => e.CategoryId == categLink.CategoryId).Where(e => e.Url.Contains(blackUrl.Url)).Count();
                    if (count > 0)
                    {

                        //Mark url as danger
                        db.Dangers.Add(new Danger
                        {
                            Id = Guid.NewGuid(),
                            UserId = blackUrl.UserId,
                            Url = blackUrl.Url
                        });

                        db.SaveChanges();

                        return; //end of funtion

                    }

                }
            }

            //Check friends blacklists
            var friends = db.Friendships.Where(e => e.UserId1 == blackUrl.UserId);
            if(friends != null)
            {
                foreach(var friend in friends)
                {
                    var count = db.Blackurls.Where(e => e.UserId == friend.UserId2).Where(e => e.Url.Contains(blackUrl.Url)).Count();
                    if(count > 0)
                    {
                        //Mark url as danger
                        db.Dangers.Add(new Danger
                        {
                            Id = Guid.NewGuid(),
                            UserId = blackUrl.UserId,
                            Url = blackUrl.Url
                        });

                        db.SaveChanges();

                        return; //end of funtion
                    }
                }
            }



        }
    }
}
