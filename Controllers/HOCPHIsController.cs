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
    public class HOCPHIsController : Controller
    {
        private QLHSHVTEntities db = new QLHSHVTEntities();
        public string GetMax()
        {
            var maMax = db.HOCPHIs.ToList().Select(hp => hp.MaHocPhi).Max();
            var maInt = int.Parse(maMax.Substring(2));
            var maIntNew = maInt + 1;
            return $"FE{maIntNew:D8}";
        }

        // GET: HOCPHIs
        public ActionResult Index(string id, string maNH)
        {
            if (id == null || id == "")
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var hOCPHIs = db.HOCPHIs.Include(h => h.HOCSINH).Include(h => h.NAMHOC).Include(h => h.LOAIHOCPHI).Where(h => h.MaHS == id);
            ViewBag.MaHS = id;
            return View(hOCPHIs.ToList());
        }

        // GET: HOCPHIs/Create
        public ActionResult Create(string id)
        {
            ViewBag.MaHS = id;
            var existingNAMHOCs = db.HOCPHIs.Where(h => h.MaHS == id).Select(h => h.MaNH).ToList();

            if (db.HOCPHIs.Any(h => h.MaHS == id))
            {
                ViewBag.MaNH = new SelectList(db.NAMHOCs
                .Where(n => !existingNAMHOCs.Contains(n.MaNH))
                .Select(n => new { n.MaNH, NamHoc = n.NamBatDau + " - " + n.NamKetThuc }), "MaNH", "NamHoc");
            }
            else
            {
                ViewBag.MaNH = new SelectList(db.NAMHOCs
                .Select(n => new { n.MaNH, NamHoc = n.NamBatDau + " - " + n.NamKetThuc }), "MaNH", "NamHoc");
            }
            ViewBag.MaLHP = new SelectList(db.LOAIHOCPHIs.Select(lp => new { lp.MaLHP, hocPhi = lp.TenLHP + " - " + lp.DONGIA + " VND" }), "MaLHP", "hocPhi");
            ViewBag.MaNamHoc = db.NAMHOCs.FirstOrDefault().MaNH; //dùng để đưa mã năm học vào route values
            return View();
        }

        // POST: HOCPHIs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHocPhi,MaNH,MaHS,DONGIA,NgayThongBao,MaLHP,NgayDongTien")] HOCPHI hOCPHI)
        {
            hOCPHI.MaHocPhi = GetMax();
            string lhp = hOCPHI.MaLHP;
            hOCPHI.DONGIA = db.LOAIHOCPHIs.SingleOrDefault(lh => lh.MaLHP == lhp).DONGIA;
            hOCPHI.NgayThongBao = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.HOCPHIs.Add(hOCPHI);
                db.SaveChanges();
                return RedirectToAction("Index", new {id = hOCPHI.MaHS, maNH = hOCPHI.MaNH});
            }

            ViewBag.MaHS = hOCPHI.MaHS;
            ViewBag.MaNH = new SelectList(db.NAMHOCs.Select(n => new { n.MaNH, NamHoc = n.NamBatDau + " - " + n.NamKetThuc }).Where(n => !db.HOCPHIs.Any(h => h.MaNH == n.MaNH)), "MaNH", "NamHoc", hOCPHI.MaNH);
            ViewBag.MaLHP = new SelectList(db.LOAIHOCPHIs.Select(lp => new { lp.MaLHP, hocPhi = lp.TenLHP + " - " + lp.DONGIA + " VND"}), "MaLHP", "hocPhi", hOCPHI.MaLHP);
            ViewBag.MaNamHoc = db.NAMHOCs.FirstOrDefault().MaNH; //dùng để đưa mã năm học vào route values
            return View(hOCPHI);
        }

        // GET: HOCPHIs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOCPHI hOCPHI = db.HOCPHIs.Find(id);
            if (hOCPHI == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHS = hOCPHI.MaHS;
            ViewBag.MaNamHoc = hOCPHI.MaNH;
            return View(hOCPHI);
        }

        // POST: HOCPHIs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HOCPHI hOCPHI = db.HOCPHIs.Find(id);
            string maNamHoc = hOCPHI.MaNH, maHS = hOCPHI.MaHS;
            db.HOCPHIs.Remove(hOCPHI);
            db.SaveChanges();
            return RedirectToAction("Index", new {id = maHS, maNH = maNamHoc});
        }

        public ActionResult DongHocPhi(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOCPHI hOCPHI = db.HOCPHIs.Find(id);
            if (hOCPHI == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHS = hOCPHI.MaHS;
            ViewBag.MaNamHoc = hOCPHI.MaNH;
            return View(hOCPHI);
        }

        [HttpPost, ActionName("DongHocPhi")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmedDHP(string id)
        {
            HOCPHI hOCPHI = db.HOCPHIs.Find(id);
            string maNamHoc = hOCPHI.MaNH, maHS = hOCPHI.MaHS;
            hOCPHI.NgayDongTien = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("Index", new { id = maHS, maNH = maNamHoc });
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
