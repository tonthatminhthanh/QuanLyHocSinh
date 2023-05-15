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
    public class CTHPsController : Controller
    {
        private QLHSHVTEntities db = new QLHSHVTEntities();

        // GET: CTHPs
        public string GetMax()
        {
            var maMax = db.CTHPs.ToList().Select(hp => hp.MaCTHP).Max();
            var maInt = int.Parse(maMax.Substring(2));
            var maIntNew = maInt + 1;
            return $"HP{maIntNew:D8}";
        }
        public ActionResult Index(string maHS, string err = "")
        {
            if(maHS == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var cTHPs = db.CTHPs.Include(c => c.GIAOVIEN).Include(c => c.HOCSINH).Include(c => c.MONHOC).Include(c => c.NAMHOC)
                .Where(c => c.HOCSINH.MaHS == maHS);
            ViewBag.MaHS = maHS;
            ViewBag.Error = err;
            return View(cTHPs.ToList());
        }

        // GET: CTHPs/Create
        public ActionResult Create(string maHS)
        {
            ViewBag.MaGV = new SelectList(db.GIAOVIENs.Select(ph => new { ph.MaGV, HoTen = ph.HoGV + " " + ph.TenGV }), "MaGV", "HoTen");
            ViewBag.MaHS = maHS;
            ViewBag.MaMH = new SelectList(db.MONHOCs, "MaMH", "TenMH");
            ViewBag.MaNH = new SelectList(db.NAMHOCs.Select(nh => new { nh.MaNH, NamHoc = (nh.NamBatDau + " - " + nh.NamKetThuc).ToString() }), "MaNH", "NamHoc");
            return View();
        }

        // POST: CTHPs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaCTHP,MaHS,MaMH,MaNH,MaGV")] CTHP cTHP)
        {
            cTHP.MaCTHP = GetMax();
            if (ModelState.IsValid)
            {
                db.CTHPs.Add(cTHP);
                db.SaveChanges();
                return RedirectToAction("Index",new {maHS = cTHP.MaHS});
            }

            ViewBag.MaGV = new SelectList(db.GIAOVIENs.Select(ph => new { ph.MaGV, HoTen = ph.HoGV + " " + ph.TenGV }), "MaGV", "HoTen", cTHP.MaGV);
            ViewBag.MaHS = cTHP.MaHS;
            ViewBag.MaMH = new SelectList(db.MONHOCs, "MaMH", "TenMH", cTHP.MaMH);
            ViewBag.MaNH = new SelectList(db.NAMHOCs.Select(nh => new { nh.MaNH, NamHoc = (nh.NamBatDau + " - " + nh.NamKetThuc).ToString()}), "MaNH", "NamHoc", cTHP.MaNH);
            return View(cTHP);
        }

        // GET: CTHPs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTHP cTHP = db.CTHPs.Find(id);
            if (cTHP == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaGV = new SelectList(db.GIAOVIENs.Select(ph => new { ph.MaGV, HoTen = ph.HoGV + " - " + ph.TenGV }), "MaGV", "HoTen", cTHP.MaGV);
            ViewBag.MaHS = cTHP.MaHS;
            ViewBag.MaMH = new SelectList(db.MONHOCs, "MaMH", "TenMH", cTHP.MaMH);
            ViewBag.MaNH = new SelectList(db.NAMHOCs.Select(nh => new { nh.MaNH, NamHoc = (nh.NamBatDau + " - " + nh.NamKetThuc).ToString() }), "MaNH", "NamHoc", cTHP.MaNH);
            return View(cTHP);
        }

        // POST: CTHPs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaCTHP,MaHS,MaMH,MaNH,MaGV")] CTHP cTHP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cTHP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { maHS = cTHP.MaHS });
            }
            ViewBag.MaGV = new SelectList(db.GIAOVIENs.Select(ph => new { ph.MaGV, HoTen = ph.HoGV + " " + ph.TenGV }), "MaGV", "HoTen", cTHP.MaGV);
            ViewBag.MaHS = cTHP.MaHS;
            ViewBag.MaMH = new SelectList(db.MONHOCs, "MaMH", "TenMH", cTHP.MaMH);
            ViewBag.MaNH = new SelectList(db.NAMHOCs.Select(nh => new { nh.MaNH, NamHoc = (nh.NamBatDau + " - " + nh.NamKetThuc).ToString() }), "MaNH", "NamHoc", cTHP.MaNH);
            return View(cTHP);
        }

        // GET: CTHPs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTHP cTHP = db.CTHPs.Find(id);
            if (cTHP == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHS = cTHP.MaHS;
            if(db.KETQUAs.Any(kq => kq.MaCTHP == cTHP.MaCTHP))
                return RedirectToAction("Index", new { maHS = cTHP.MaHS, err = "Phải xóa kết quả của học phần này trước khi xóa học phần!" });
            return View(cTHP);
        }

        // POST: CTHPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CTHP cTHP = db.CTHPs.Find(id);
            string ma = cTHP.MaHS;
            db.CTHPs.Remove(cTHP);
            db.SaveChanges();
            return RedirectToAction("Index", new {maHS = ma});
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
