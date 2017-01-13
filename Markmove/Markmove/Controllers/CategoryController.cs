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

    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        // GET: Category/List
        public ActionResult List()
        {
            using (var database = new MarkmoveDbContext())
            {
                var categories = database.Categories.ToList();

                return this.View(categories);
            }
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: Category/Create
        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                using (var database = new MarkmoveDbContext())
                {
                    database.Categories.Add(category);
                    database.SaveChanges();

                    return RedirectToAction("Index");
                }
            }

            return this.View(category);
        }

        // GET: Category/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var database = new MarkmoveDbContext())
            {
                var category = database.Categories.Find(id);

                if (category == null)
                {
                    return this.HttpNotFound();
                }

                return this.View(category);
            }
        }

        // POST: Category/Edit
        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                using (var database = new MarkmoveDbContext())
                {
                    database.Entry(category).State = EntityState.Modified;
                    database.SaveChanges();

                    return RedirectToAction("Index");
                }
            }

            return this.View(category);
        }

        // GET: Category/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var database = new MarkmoveDbContext())
            {
                var category = database.Categories.Find(id);

                if (category == null)
                {
                    return this.HttpNotFound();
                }

                return this.View(category);
            }
        }

        // POST: Category/Delete
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            using (var database = new MarkmoveDbContext())
            {
                var category = database.Categories.Find(id);
                var categoryDestinations = category.Destinations.ToList();

                foreach (var destination in categoryDestinations)
                {
                    database.Destinations.Remove(destination);
                }

                database.Categories.Remove(category);
                database.SaveChanges();

                return RedirectToAction("Index");
            }
        }
    }
}