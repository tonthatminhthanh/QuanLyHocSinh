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
    [Authorize(Roles = "Admin")]
    public class THAMSOesController : Controller
    {
        private QLHSHVTEntities db = new QLHSHVTEntities();

        // GET: THAMSOes
        public ActionResult Index()
        {
            return View(db.THAMSOes.ToList());
        }

        // GET: THAMSOes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THAMSO tHAMSO = db.THAMSOes.Find(id);
            if (tHAMSO == null)
            {
                return HttpNotFound();
            }
            return View(tHAMSO);
        }

        // POST: THAMSOes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaThamSo,GiaTriToiThieu,GiaTriToiDa")] THAMSO tHAMSO)
        {
            if(tHAMSO.GiaTriToiThieu > tHAMSO.GiaTriToiDa)
            {
                ViewBag.Error = "Giá trị tối thiểu phải lớn hơn giá trị tối đa";
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(tHAMSO).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(tHAMSO);
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
