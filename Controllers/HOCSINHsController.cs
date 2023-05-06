using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLHocSinhHVT.Models;

namespace QLHocSinhHVT.Controllers
{
    [Authorize]
    public class HOCSINHsController : Controller
    {
        private QLHSHVTEntities db = new QLHSHVTEntities();

        public string GetMax()
        {
            var maMax = db.HOCSINHs.ToList().Select(s => s.MaHS).Max();
            var maInt = int.Parse(maMax.Substring(2));
            var maIntNew = maInt + 1;
            return $"HS{maIntNew:D6}";
        }

        // GET: HOCSINHs
        public ActionResult Index()
        {
            var hOCSINHs = db.HOCSINHs.Include(h => h.LOP).Include(h => h.PHUHUYNH);
            return View(hOCSINHs.ToList());
        }

        // GET: HOCSINHs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOCSINH hOCSINH = db.HOCSINHs.Find(id);
            if (hOCSINH == null)
            {
                return HttpNotFound();
            }
            return View(hOCSINH);
        }

        // GET: HOCSINHs/Create
        [Authorize(Roles = "User")]
        public ActionResult Create()
        {
            ViewBag.MaLop = new SelectList(db.LOPs, "MaLop", "MaLop");
            ViewBag.MaPH = new SelectList(db.PHUHUYNHs.Select(ph => new { ph.MaPH, HoTen = ph.HoPH + " " + ph.TenPH }), "MaPH", "HoTen");
            return View();
        }

        // POST: HOCSINHs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHS,HoHS,TenHS,AnhHS,GioiTinh,MaLop,MaPH,NgaySinh,SDT,Email,DiaChi,QueQuan,TonGiao,QuocTich")] HOCSINH hOCSINH)
        {
            string maHS = GetMax();
            hOCSINH.MaHS = maHS;
            var anh = Request.Files["Avatar"];
            string fileName = System.IO.Path.GetFileName(anh.FileName);
            string savePath = Server.MapPath("/Images/" + fileName);
            anh.SaveAs(savePath);
            hOCSINH.AnhHS = fileName;

            if (ModelState.IsValid)
            {
                db.HOCSINHs.Add(hOCSINH);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLop = new SelectList(db.LOPs, "MaLop", "MaLop", hOCSINH.MaLop);
            ViewBag.MaPH = new SelectList(db.PHUHUYNHs.Select(ph => new { ph.MaPH, HoTen = ph.HoPH + " " + ph.TenPH }), "MaPH", "HoTen", hOCSINH.MaPH);
            return View(hOCSINH);
        }

        // GET: HOCSINHs/Edit/5
        [Authorize(Roles = "User")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOCSINH hOCSINH = db.HOCSINHs.Find(id);
            if (hOCSINH == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLop = new SelectList(db.LOPs, "MaLop", "MaLop", hOCSINH.MaLop);
            ViewBag.MaPH = new SelectList(db.PHUHUYNHs.Select(ph => new { ph.MaPH, HoTen = ph.HoPH + " " + ph.TenPH }), "MaPH", "HoTen", hOCSINH.MaPH);

            return View(hOCSINH);
        }

        // POST: HOCSINHs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public ActionResult Edit([Bind(Include = "MaHS,HoHS,TenHS,AnhHS,GioiTinh,MaLop,MaPH,NgaySinh,SDT,Email,DiaChi,QueQuan,TonGiao,QuocTich")] HOCSINH hOCSINH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hOCSINH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLop = new SelectList(db.LOPs, "MaLop", "MaNganh", hOCSINH.MaLop);
            ViewBag.MaPH = new SelectList(db.PHUHUYNHs.Select(ph => new { ph.MaPH, HoTen = ph.HoPH + " " + ph.TenPH }), "MaPH", "HoTen", hOCSINH.MaPH);
            return View(hOCSINH);
        }

        // GET: HOCSINHs/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOCSINH hOCSINH = db.HOCSINHs.Find(id);
            if (hOCSINH == null)
            {
                return HttpNotFound();
            }
            return View(hOCSINH);
        }

        // POST: HOCSINHs/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HOCSINH hOCSINH = db.HOCSINHs.Find(id);
            db.HOCSINHs.Remove(hOCSINH);
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
