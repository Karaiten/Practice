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

        // GET: /Software/
        public ActionResult Index()
        {
            return View(db.Softwares.ToList());
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
