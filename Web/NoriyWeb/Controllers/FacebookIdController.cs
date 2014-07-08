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

namespace NoriyWeb.Controllers
{
    [Authorize]
    public class FacebookIdController : ApiController
    {
        private NoriyEntities db = new NoriyEntities();

        // GET api/FacebookId
        public IQueryable<FacebookId> GetFacebookIds()
        {
            return db.FacebookIds;
        }

        // GET api/FacebookId/5
        [ResponseType(typeof(FacebookId))]
        public IHttpActionResult GetFacebookId(Guid id)
        {
            FacebookId facebookid = db.FacebookIds.Find(id);
            if (facebookid == null)
            {
                return NotFound();
            }

            return Ok(facebookid);
        }

        // PUT api/FacebookId/5
        public IHttpActionResult PutFacebookId(Guid id, FacebookId facebookid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != facebookid.Id)
            {
                return BadRequest();
            }

            db.Entry(facebookid).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacebookIdExists(facebookid.UserId))
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

        // POST api/FacebookId
        [ResponseType(typeof(FacebookId))]
        public IHttpActionResult PostFacebookId(FacebookId facebookid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            var result = db.FacebookIds.Where(e => e.UserId == facebookid.UserId).Count();
            if(result == 0)
            {
                facebookid.Id = Guid.NewGuid();
                db.FacebookIds.Add(facebookid);
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (FacebookIdExists(facebookid.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = facebookid.Id }, facebookid);
        }

        // DELETE api/FacebookId/5
        [ResponseType(typeof(FacebookId))]
        public IHttpActionResult DeleteFacebookId(Guid id)
        {
            FacebookId facebookid = db.FacebookIds.Find(id);
            if (facebookid == null)
            {
                return NotFound();
            }

            db.FacebookIds.Remove(facebookid);
            db.SaveChanges();

            return Ok(facebookid);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FacebookIdExists(string id)
        {
            return db.FacebookIds.Count(e => e.UserId == id) > 0;
        }
    }
}