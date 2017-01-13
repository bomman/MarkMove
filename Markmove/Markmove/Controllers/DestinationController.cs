using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Markmove.Controllers
{
    using System.Data.Entity;
    using System.Net;
    using Models;

    public class DestinationController : Controller
    {
        // GET: Destination
        public ActionResult Index()
        {
            return this.RedirectToAction("List");
        }

        // GET: Destination/List
        public ActionResult List()
        {
            using (var database = new MarkmoveDbContext())
            {
                var destinations = database.Destinations
                    .ToList();

                return this.View(destinations);
            }
        }

        // GET: Destination/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var database = new MarkmoveDbContext())
            {
                var article = database.Destinations
                    .Where(d => d.Id == id)
                    .Include(d => d.Tags)
                    .First();

                if (article == null)
                {
                    return this.HttpNotFound();
                }

                return this.View(article);
            }
        }

        // GET: Destination/Create
        [Authorize]
        public ActionResult Create()
        {
            using (var database = new MarkmoveDbContext())
            {
                var model = new DestinationViewModel();

                ViewBag.AvailableCategories = database.Categories.ToList();

                return this.View(model);
            }
        }

        // POST: Destination/Create
        [HttpPost]
        [Authorize]
        public ActionResult Create(DestinationViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                using (var database = new MarkmoveDbContext())
                {
                    var destination = new Destination(model.Name, model.Country, model.Description, model.CategoryId);
                    
                    this.SetDestinationTags(destination, model, database);

                    database.Destinations.Add(destination);
                    database.SaveChanges();

                    return this.RedirectToAction("Index");
                }   
            }

            return this.View(model);
        }

        // GET: Destination/Delete
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var database = new MarkmoveDbContext())
            {
                var destination = database.Destinations
                    .First(d => d.Id == id);

                if (!this.IsUserAuthorizedToEdit(destination))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }

                if (destination == null)
                {
                    return this.HttpNotFound();
                }

                ViewBag.TagsString = string.Join(", ", destination.Tags.Select(t => t.Name));

                var viewModel = new DestinationViewModel()
                {
                    Id = destination.Id,
                    Name = destination.Name,
                    Description = destination.Description,
                    Country = destination.Country,
                    CategoryId = destination.CategoryId,
                    CategoryName = destination.Category.Name
                };

                return this.View(viewModel);
            }
        }

        //POST: Destination/Delete
        [HttpPost]
        [ActionName("Delete")]
        [Authorize]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var database = new MarkmoveDbContext())
            {
                var destination = database.Destinations.Find(id);

                if (destination == null)
                {
                    return this.HttpNotFound();
                }

                database.Destinations.Remove(destination);
                database.SaveChanges();

                return this.RedirectToAction("Index");
            }
        }

        // GET: Destination/Edit
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var database = new MarkmoveDbContext())
            {
                var destination = database.Destinations.Find(id);

                if (!this.IsUserAuthorizedToEdit(destination))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }

                if (destination == null)
                {
                    return this.HttpNotFound();
                }

                var viewModel = new DestinationViewModel()
                {
                    Id = destination.Id,
                    Name = destination.Name,
                    Description = destination.Description,
                    Country = destination.Country,
                    CategoryId = destination.CategoryId
                };

                ViewBag.AvailableCategories = database.Categories.ToList();

                viewModel.Tags = string.Join(", ", destination.Tags.Select(t => t.Name));

                return this.View(viewModel);
            }
        }

        // POST: Destination/Edit
        [HttpPost]
        [Authorize]
        public ActionResult Edit(DestinationViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                using (var database = new MarkmoveDbContext())
                {
                    var destination = database.Destinations.FirstOrDefault(d => d.Id == model.Id);

                    destination.Name = model.Name;
                    destination.Description = model.Description;
                    destination.CategoryId = model.CategoryId;

                    this.SetDestinationTags(destination, model, database);

                    database.Entry(destination).State = EntityState.Modified;
                    database.SaveChanges();
                }
            }

            return this.RedirectToAction("Index");
        }

        private void SetDestinationTags(Destination destination, DestinationViewModel model, MarkmoveDbContext database)
        {
            var tagsStrings = model.Tags
                .Split(new char[] {',', ' '}, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.ToLower())
                .Distinct();

            destination.Tags.Clear();

            foreach (var tagsString in tagsStrings)
            {
                Tag tag = database.Tags.FirstOrDefault(t => t.Name.Equals(tagsString));

                if (tag == null)
                {
                    tag = new Tag() { Name = tagsString };
                    database.Tags.Add(tag);
                }

                destination.Tags.Add(tag);
            }
        }

        private bool IsUserAuthorizedToEdit(Destination destination)
        {
            bool isAdmin = this.User.IsInRole("Admin");

            // bool isAuthor = destination.IsAuthor(this.User.Indentity.Name);

            return isAdmin;
        }
    }
}