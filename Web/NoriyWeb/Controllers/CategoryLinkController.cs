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
    public class CategoryLinkController : ApiController
    {
        private NoriyEntities db = new NoriyEntities();

        public CategoryLinkController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET api/CategoryLink
        public IQueryable<CategoryLink> GetCategoryLinks()
        {
            return db.CategoryLinks;
        }

        // GET api/CategoryLink/5
        [ResponseType(typeof(CategoryLink))]
        public IHttpActionResult GetCategoryLink(string id)
        {
            var categorylinks = db.CategoryLinks.Where(e => e.UserId == id);
            if (categorylinks == null)
            {
                return NotFound();
            }

            return Ok(categorylinks);
        }

        // PUT api/CategoryLink/5
      /*  public IHttpActionResult PutCategoryLink(RemoveCategory remove)
        {

            var result = db.CategoryLinks.Where(e => e.UserId == remove.UserId);
            result = result.Where(e => e.CategoryId == remove.CategoryId);

            db.CategoryLinks.RemoveRange(result);
            /*

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categorylink.Id)
            {
                return BadRequest();
            }

            db.Entry(categorylink).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryLinkExists(id))
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
    */

        // POST api/CategoryLink
        [ResponseType(typeof(CategoryLink))]
        public IHttpActionResult PostCategoryLink(CategoryLink categorylink)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            categorylink.Id = Guid.NewGuid();

            db.CategoryLinks.Add(categorylink);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CategoryLinkExists(categorylink.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = categorylink.Id }, categorylink);
        }

        // DELETE api/CategoryLink/5
        [ResponseType(typeof(CategoryLink))]
        public IHttpActionResult DeleteCategoryLink(RemoveCategory remove)
        {
            var result = db.CategoryLinks.Where(e => e.UserId == remove.UserId);
            result = result.Where(e => e.CategoryId == remove.CategoryId);

            db.CategoryLinks.RemoveRange(result);
            /*

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categorylink.Id)
            {
                return BadRequest();
            }
            */
            db.SaveChanges();

            return Ok(remove);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryLinkExists(Guid id)
        {
            return db.CategoryLinks.Count(e => e.Id == id) > 0;
        }
    }
}