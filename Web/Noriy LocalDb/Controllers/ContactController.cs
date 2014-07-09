using Noriy_LocalDb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Noriy_LocalDb.Controllers
{
    public class ContactController : ApiController
    {
        NoriyEntities db = new NoriyEntities();

        public IHttpActionResult Send(Contact form)
        {
            try
            {
                form.Id = Guid.NewGuid();
                db.Contacts.Add(form);
                db.SaveChanges();
                return Ok();
            }
            catch(DbUpdateException)
            {
                return NotFound();
            }

        }
    }
}
