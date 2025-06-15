using SoftwareStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace SoftwareStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly SoftwareContext db = new SoftwareContext();

        // Головна сторінка зі списком ПЗ
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





        // GET: Покупка
        [HttpGet]
        public ActionResult Buy(int id)
        {
            ViewBag.SoftwareId = id;
            return View();
        }

        // POST: Покупка
        [HttpPost]
        public ActionResult Buy(Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                purchase.Date = DateTime.Now;
                db.Purchases.Add(purchase);
                db.SaveChanges();
                return Content($"Дякуємо, {purchase.Person}, за покупку!");
            }
            return View(purchase);
        }

        // Перегляд деталей ПЗ
        public ActionResult SoftwareView(int? id)
        {
            if (id == null) return RedirectToAction("Index");

            var software = db.Softwares.Find(id);
            if (software == null) return RedirectToAction("Index");

            return View(software);
        }

        // GET: Редагування ПЗ
        [HttpGet]
        public ActionResult EditSoftware(int? id)
        {
            if (id == null) return HttpNotFound();

            var software = db.Softwares.Find(id);
            if (software == null) return HttpNotFound();

            ViewBag.Categories = new SelectList(db.Categories, "Id", "Name", software.CategoryId);
            return View(software);
        }

        // POST: Редагування ПЗ
        [HttpPost]
        public ActionResult EditSoftware(Software software)
        {
            if (ModelState.IsValid)
            {
                db.Entry(software).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Categories = new SelectList(db.Categories, "Id", "Name", software.CategoryId);
            return View(software);
        }

        // GET: Підтвердження видалення
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null) return HttpNotFound();

            var software = db.Softwares.Find(id);
            if (software == null) return HttpNotFound();

            return View(software);
        }

        // POST: Видалення підтверджене
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null) return HttpNotFound();

            var software = db.Softwares.Find(id);
            if (software == null) return HttpNotFound();

            db.Softwares.Remove(software);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Створення нового ПЗ
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Categories = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        // POST: Створення нового ПЗ
        [HttpPost]
        public ActionResult Create(Software software)
        {
            if (ModelState.IsValid)
            {
                db.Softwares.Add(software);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Categories = new SelectList(db.Categories, "Id", "Name", software.CategoryId);
            return View(software);
        }

        // Звільнення ресурсів
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
