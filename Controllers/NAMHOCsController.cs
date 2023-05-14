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
    public class NAMHOCsController : Controller
    {
        private QLHSHVTEntities db = new QLHSHVTEntities();

        // GET: NAMHOCs
        public string GetMax()
        {
            var maMax = db.NAMHOCs.ToList().Select(s => s.MaNH).Max();
            var maInt = int.Parse(maMax.Substring(2));
            var maIntNew = maInt + 1;
            return $"NH{maIntNew:D4}";
        }

        public ActionResult Index()
        {
            return View(db.NAMHOCs.ToList());
        }

        // GET: NAMHOCs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NAMHOCs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNH,NamBatDau,NamKetThuc")] NAMHOC nAMHOC)
        {
            nAMHOC.MaNH = GetMax();
            if (nAMHOC.NamBatDau < DateTime.Now.Year)
            {
                ViewBag.Error = "Lỗi: Năm học nhỏ hơn năm hiện tại";
                return View(nAMHOC);
            }
            if(db.NAMHOCs.Any(nh => nh.NamBatDau == nAMHOC.NamBatDau))
            {
                ViewBag.Error = "Lỗi: Năm học đã tồn tại";
                return View(nAMHOC);
            }
            nAMHOC.NamKetThuc = nAMHOC.NamBatDau + 1;

            if (ModelState.IsValid)
            {
                db.NAMHOCs.Add(nAMHOC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nAMHOC);
        }

        // GET: NAMHOCs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NAMHOC nAMHOC = db.NAMHOCs.Find(id);
            if (nAMHOC == null)
            {
                return HttpNotFound();
            }
            return View(nAMHOC);
        }

        // POST: NAMHOCs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NAMHOC nAMHOC = db.NAMHOCs.Find(id);
            db.NAMHOCs.Remove(nAMHOC);
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
