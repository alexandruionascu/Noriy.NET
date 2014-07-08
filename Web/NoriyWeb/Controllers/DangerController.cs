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
    public class DangerController : ApiController
    {
        private NoriyEntities db = new NoriyEntities();

        // GET api/Danger
        public IQueryable<Danger> GetDangers()
        {
            return db.Dangers;
        }

        // GET api/Danger/5
        [ResponseType(typeof(Danger))]
        public IHttpActionResult GetDanger(Guid id)
        {
            Danger danger = db.Dangers.Find(id);
            if (danger == null)
            {
                return NotFound();
            }

            return Ok(danger);
        }

        // PUT api/Danger/5
        public IHttpActionResult PutDanger(Guid id, Danger danger)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != danger.Id)
            {
                return BadRequest();
            }

            db.Entry(danger).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DangerExists(id))
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

        // POST api/Danger
        [ResponseType(typeof(Danger))]
        public IHttpActionResult PostDanger(Danger danger)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Dangers.Add(danger);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DangerExists(danger.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = danger.Id }, danger);
        }

        // DELETE api/Danger/5
        [ResponseType(typeof(Danger))]
        public IHttpActionResult DeleteDanger(Guid id)
        {
            Danger danger = db.Dangers.Find(id);
            if (danger == null)
            {
                return NotFound();
            }

            db.Dangers.Remove(danger);
            db.SaveChanges();

            return Ok(danger);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DangerExists(Guid id)
        {
            return db.Dangers.Count(e => e.Id == id) > 0;
        }
    }
}