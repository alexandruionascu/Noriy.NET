﻿using System;
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
    [Authorize]
    public class BlackurlController : ApiController
    {
        private NoriyEntities db = new NoriyEntities();

        // GET api/Blackurl
    
        //public IQueryable<Blackurl> GetBlackurls()
        //{
        //    return db.Blackurls;
        //}
     

        // GET api/Blackurl/5
        [ResponseType(typeof(Blackurl))]
        public IHttpActionResult GetBlackurl(string id)
        {
            var blackUrls = db.Blackurls.Where(e => e.UserId == id);
            if (blackUrls == null)
            {
                return NotFound();
            }

            return Ok(blackUrls);
        }

        // PUT api/Blackurl/5
        public IHttpActionResult PutBlackurl(Guid id, Blackurl blackurl)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != blackurl.Id)
            {
                return BadRequest();
            }

            db.Entry(blackurl).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlackurlExists(id))
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

        // POST api/Blackurl
        [ResponseType(typeof(Blackurl))]
        public IHttpActionResult PostBlackurl(Blackurl blackurl)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            blackurl.Id = Guid.NewGuid();

            db.Blackurls.Add(blackurl);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (BlackurlExists(blackurl.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = blackurl.Id }, blackurl);
        }

        // DELETE api/Blackurl/5
        [ResponseType(typeof(Blackurl))]
        public IHttpActionResult DeleteBlackurl(Guid id)
        {
            Blackurl blackurl = db.Blackurls.Find(id);
            if (blackurl == null)
            {
                return NotFound();
            }

            db.Blackurls.Remove(blackurl);
            db.SaveChanges();

            return Ok(blackurl);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BlackurlExists(Guid id)
        {
            return db.Blackurls.Count(e => e.Id == id) > 0;
        }
    }
}