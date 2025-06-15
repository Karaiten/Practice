using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SoftwareStore.Models;

namespace SoftwareStore.Controllers
{
    public class SoftwareController : Controller
    {
        private SoftwareContext db = new SoftwareContext();

        public ActionResult Index(int? categoryId, string licenseType, int page = 1)
        {
            int pageSize = 5;
            IQueryable<Software> softwares = db.Softwares.Include(s => s.Category);

            if (categoryId != null && categoryId != 0)
            {
                softwares = softwares.Where(s => s.CategoryId == categoryId);
            }

            if (!string.IsNullOrEmpty(licenseType) && licenseType != "Всі")
            {
                softwares = softwares.Where(s => s.LicenseType == licenseType);
            }

            var totalItems = softwares.Count();

            var softwaresOnPage = softwares
                .OrderBy(s => s.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var categories = db.Categories.ToList();
            categories.Insert(0, new Category { Id = 0, Name = "Всі" });

            var licenseTypes = db.Softwares
                                 .Select(s => s.LicenseType)
                                 .Distinct()
                                 .ToList();
            licenseTypes.Insert(0, "Всі");

            var model = new SoftwareListViewModel
            {
                Softwares = softwaresOnPage,
                Categories = new SelectList(categories, "Id", "Name", categoryId),
                LicenseTypes = new SelectList(licenseTypes, licenseType),
                PageInfo = new PageInfo
                {
                    PageNumber = page,
                    PageSize = pageSize,
                    TotalItems = totalItems
                }
            };

            return View(model);
        }




        // GET: /Software/Details/5
        public ActionResult Details(int id = 0)
        {
            Software software = db.Softwares.Find(id);
            if (software == null)
            {
                return HttpNotFound();
            }
            return View(software);
        }

        // GET: /Software/Create
        public ActionResult Create()
        {
            var categories = db.Categories.ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }

        // POST: /Software/Create
        [HttpPost]
        public ActionResult Create(Software software)
        {
            if (ModelState.IsValid)
            {
                db.Softwares.Add(software);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var categories = db.Categories.ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", software.CategoryId);
            return View(software);
        }


        // GET: /Software/Edit/5
        public ActionResult Edit(int id = 0)
        {
            Software software = db.Softwares.Find(id);
            if (software == null)
            {
                return HttpNotFound();
            }

            var categories = db.Categories.ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", software.CategoryId);

            return View(software);
        }


        // POST: /Software/Edit/5
        [HttpPost]
        public ActionResult Edit(Software software)
        {
            if (ModelState.IsValid)
            {
                db.Entry(software).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(software);
        }

        // GET: /Software/Delete/5
        public ActionResult Delete(int id = 0)
        {
            Software software = db.Softwares.Find(id);
            if (software == null)
            {
                return HttpNotFound();
            }
            return View(software);
        }

        // POST: /Software/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Software software = db.Softwares.Find(id);
            db.Softwares.Remove(software);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
