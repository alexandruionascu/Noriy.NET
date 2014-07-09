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
using Microsoft.AspNet.Identity;
using NoriyWeb.Models;
using Noriy_LocalDb.Models;

namespace NoriyWeb.Controllers
{
    [Authorize]
    public class TrafficController : ApiController
    {
        private NoriyEntities db = new NoriyEntities();

        
        // GET api/Traffic
       
        public IQueryable<Traffic> GetTraffic()
        {
            return db.Traffic;
        }

        // GET api/Traffic/5
        [ResponseType(typeof(IQueryable<Traffic>))]
        public IHttpActionResult GetTraffic(Guid id)
        {
            var userId = id.ToString();
            var traffic = db.Traffic.Where(e => e.UserId == userId);
            if (traffic == null)
            {
                return NotFound();
            }

            return Ok(traffic);
        }

        // PUT api/Traffic/5
        public IHttpActionResult PutTraffic(Guid id, Traffic traffic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != traffic.Id)
            {
                return BadRequest();
            }

            db.Entry(traffic).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrafficExists(id))
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

        // POST api/Traffic
        [ResponseType(typeof(Traffic))]
        public IHttpActionResult PostTraffic(Traffic traffic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Traffic.Add(traffic);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TrafficExists(traffic.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = traffic.Id }, traffic);
        }

        // DELETE api/Traffic/5
        [ResponseType(typeof(Traffic))]
        public IHttpActionResult DeleteTraffic(Guid id)
        {
            Traffic traffic = db.Traffic.Find(id);
            if (traffic == null)
            {
                return NotFound();
            }

            db.Traffic.Remove(traffic);
            db.SaveChanges();

            return Ok(traffic);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TrafficExists(Guid id)
        {
            return db.Traffic.Count(e => e.Id == id) > 0;
        }
    }
}