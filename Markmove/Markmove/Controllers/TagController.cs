using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Markmove.Models
{
    using System.Data.Entity;
    using System.Net;

    public class TagController : Controller
    {
        // GET: Tag
        public ActionResult List(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var database = new MarkmoveDbContext())
            {
                var destinations = database.Tags
                    .Include(t => t.Destinations.Select(d => d.Tags))
                    .FirstOrDefault(t => t.Id == id)
                    .Destinations
                    .ToList();

                return View(destinations);
            }
        }
    }
}