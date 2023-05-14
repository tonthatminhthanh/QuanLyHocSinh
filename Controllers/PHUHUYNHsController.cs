using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyHocSinh.Models;

namespace QuanLyHocSinh.Controllers
{
    public class PHUHUYNHsController : Controller
    {
        private QLHSHVTEntities db = new QLHSHVTEntities();

        // GET: PHUHUYNHs
        public string GetMax()
        {
            var maMax = db.PHUHUYNHs.ToList().Select(ph => ph.MaPH).Max();
            var maInt = int.Parse(maMax.Substring(2));
            var maIntNew = maInt + 1;
            return $"PH{maIntNew:D4}";
        }
        [Authorize(Roles = "User")]
        public ActionResult Index()
        {
            return View(db.PHUHUYNHs.ToList());
        }

        // GET: PHUHUYNHs/Details/5
        [Authorize(Roles = "User")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHUHUYNH pHUHUYNH = db.PHUHUYNHs.Find(id);
            if (pHUHUYNH == null)
            {
                return HttpNotFound();
            }
            return View(pHUHUYNH);
        }

        // GET: PHUHUYNHs/Create
        [Authorize(Roles = "User")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: PHUHUYNHs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public ActionResult Create([Bind(Include = "MaPH,HoPH,TenPH,SDT,Email,NgheNghiep")] PHUHUYNH pHUHUYNH)
        {
            pHUHUYNH.MaPH = GetMax();
            if (ModelState.IsValid)
            {
                db.PHUHUYNHs.Add(pHUHUYNH);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pHUHUYNH);
        }

        // GET: PHUHUYNHs/Edit/5
        [Authorize(Roles = "User")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHUHUYNH pHUHUYNH = db.PHUHUYNHs.Find(id);
            if (pHUHUYNH == null)
            {
                return HttpNotFound();
            }
            return View(pHUHUYNH);
        }

        // POST: PHUHUYNHs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public ActionResult Edit([Bind(Include = "MaPH,HoPH,TenPH,SDT,Email,NgheNghiep")] PHUHUYNH pHUHUYNH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pHUHUYNH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pHUHUYNH);
        }

        // GET: PHUHUYNHs/Delete/5
        [Authorize(Roles = "User")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHUHUYNH pHUHUYNH = db.PHUHUYNHs.Find(id);
            if (pHUHUYNH == null)
            {
                return HttpNotFound();
            }
            return View(pHUHUYNH);
        }

        // POST: PHUHUYNHs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public ActionResult DeleteConfirmed(string id)
        {
            PHUHUYNH pHUHUYNH = db.PHUHUYNHs.Find(id);
            db.PHUHUYNHs.Remove(pHUHUYNH);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
