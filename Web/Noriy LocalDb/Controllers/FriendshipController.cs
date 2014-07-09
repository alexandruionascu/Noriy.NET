using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using NoriyWeb.Models;
using Noriy_LocalDb.Models;

namespace NoriyWeb.Controllers
{
    
    public class FriendshipController : ApiController
    {
        private NoriyEntities db = new NoriyEntities();

        // GET api/Friendship

        /*
        public IQueryable<Friendship> GetFriendships()
        {
            return db.Friendships;
        }
         */

        // GET api/Friendship/5
        [ResponseType(typeof(Friendship))]
        public IHttpActionResult GetFriendship(string id)
        {
            List<string> friendsData = new List<string>();
            var friends = db.Friendships.Where(e => e.UserId1 == id);
            if (friends == null)
            {
                return NotFound();
            }
            else
            {
                foreach (var friend in friends)
                {
                    var data = db.AspNetUsers.Where(e => e.Id == friend.UserId2);
                    if(data != null)
                    {
                        var friendData = data.FirstOrDefault();
                        if (friendData != null)
                        {
                            friendsData.Add(friendData.UserName);
                        }
                    }           
                                  
                }
            }

            return Ok(friendsData);
        }

        // PUT api/Friendship/5
        public IHttpActionResult PutFriendship(Guid id, Friendship friendship)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != friendship.Id)
            {
                return BadRequest();
            }

            db.Entry(friendship).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FriendshipExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Friendship
        [ResponseType(typeof(Friendship))]
        public IHttpActionResult PostFriendship(Friendship friendship)
        {
            

            var friendId = db.AspNetUsers.Where(e => e.UserName == friendship.UserId2);
            if(friendId != null)
            {
                var id2 = friendId.SingleOrDefault();
                if(id2 != null)
                {
                    friendship.UserId2 = id2.Id;
                    friendship.Id = Guid.NewGuid();
                    db.Friendships.Add(friendship);
                }
              

            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (FriendshipExists(friendship.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = friendship.Id }, friendship);
        }

        // DELETE api/Friendship/
        [ResponseType(typeof(Friendship))]
        public IHttpActionResult DeleteFriendship(RemoveFriendship friendship)
        {
            //Get Friend ID
            var friendData = db.AspNetUsers.FirstOrDefault(e => e.UserName == friendship.FriendName);
            if (friendData != null)
            {
                var removeFriendship = db.Friendships.Where(e => e.UserId1 == friendship.UserId).FirstOrDefault(e => e.UserId2 == friendData.Id);
                db.Friendships.Remove(removeFriendship);
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    return NotFound();
                }
                
            }
            else return NotFound();




            //

            return Ok(friendship);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FriendshipExists(Guid id)
        {
            return db.Friendships.Count(e => e.Id == id) > 0;
        }
    }
}