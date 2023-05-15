using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;
using Microsoft.Ajax.Utilities;
using QuanLyHocSinh.Models;

namespace QuanLyHocSinh.Controllers
{ 
    public class HOCSINHsController : Controller
    {
        private QLHSHVTEntities db = new QLHSHVTEntities();

        // GET: HOCSINHs
        public string GetMax()
        {
            var maMax = db.HOCSINHs.ToList().Select(s => s.MaHS).Max();
            var maInt = int.Parse(maMax.Substring(2));
            var maIntNew = maInt + 1;
            return $"HS{maIntNew:D6}";
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public ActionResult ExportListHocSinhToExcel(List<HOCSINH> hOCSINHs)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Danh sách học sinh");
            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 2].Value = "Họ học sinh";
            worksheet.Cells[1, 3].Value = "Tên học sinh";
            worksheet.Cells[1, 4].Value = "Giới tính";
            worksheet.Cells[1, 5].Value = "Lớp";
            worksheet.Cells[1, 6].Value = "Phụ huynh";
            worksheet.Cells[1, 7].Value = "Ngày sinh";
            worksheet.Cells[1, 8].Value = "SĐT";
            worksheet.Cells[1, 9].Value = "Email";
            worksheet.Cells[1, 10].Value = "Địa chỉ";
            worksheet.Cells[1, 11].Value = "Quê quán";
            worksheet.Cells[1, 12].Value = "Tôn giáo";
            worksheet.Cells[1, 13].Value = "Quốc tịch";
            int stt = 1, rowNum = 2;
            foreach(var hs in hOCSINHs)
            {
                var pHUHUYNH = db.PHUHUYNHs.Where(ph => ph.MaPH == hs.MaPH).Single();
                var tONGIAO = db.TONGIAOs.Where(tg => tg.MaTonGiao == hs.MaTonGiao).Single();
                var qUOCTICH = db.QUOCTICHes.Where(qt => qt.MaQuocTich == hs.MaQuocTich).Single();
                worksheet.Cells[rowNum, 1].Value = stt;
                worksheet.Cells[rowNum, 2].Value = hs.HoHS;
                worksheet.Cells[rowNum, 3].Value = hs.TenHS;
                if(hs.GioiTinh)
                    worksheet.Cells[rowNum, 4].Value = "Nam";
                else
                    worksheet.Cells[rowNum, 4].Value = "Nữ";
                worksheet.Cells[rowNum, 5].Value = hs.MaLop;
                worksheet.Cells[rowNum, 6].Value = pHUHUYNH.HoPH + " " + pHUHUYNH.TenPH;
                worksheet.Cells[rowNum, 7].Value = hs.NgaySinh.Day + "/" + hs.NgaySinh.Month + "/" + hs.NgaySinh.Year;
                worksheet.Cells[rowNum, 8].Value = hs.SDT;
                worksheet.Cells[rowNum, 9].Value = hs.Email;
                worksheet.Cells[rowNum, 10].Value = hs.DiaChi;
                worksheet.Cells[rowNum, 11].Value = hs.QueQuan;
                worksheet.Cells[rowNum, 12].Value = tONGIAO.TenTonGiao;
                worksheet.Cells[rowNum, 13].Value = qUOCTICH.TenQuocTich;
                stt++;
                rowNum++;
            }

            byte[] data = excelPackage.GetAsByteArray();

            return File(data, "\"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet\"", "DanhSachHocSinh.xls");
        }

        public JsonResult GetPhuHuynh(string term)
        {
            var phuHuynhList = db.PHUHUYNHs
    .Where(p => p.TenPH.Contains(term) || p.HoPH.Contains(term))
    .Select(p => new { label = p.HoPH + " " + p.TenPH, value = p.MaPH })
    .ToList();
            return Json(phuHuynhList, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "User")]
        public ActionResult Index()
        {
            var hOCSINHs = db.HOCSINHs.Include(h => h.LOP).Include(h => h.PHUHUYNH).Include(h => h.QUOCTICH).Include(h => h.TONGIAO);

            ViewBag.MaLop = new SelectList(db.LOPs, "MaLop", "MaLop");
            ViewBag.MaNH = db.NAMHOCs.FirstOrDefault().MaNH;
            return View(hOCSINHs.ToList());
        }
        [HttpPost]
        [Authorize(Roles = "User")]
        public ActionResult Index(string id = "", string hoTenHS = "", string maLop = "", string maPH = "")
        {
            var hocSinhs = db.HOCSINHs.Where(s => s.TenHS.Contains(""));
            //|| (hs.HoHS.ToLower() + " " + hs.TenHS.ToLower()).Contains(hoTenHS.ToLower())
            //|| hs.MaLop.Contains(maLop) || hs.MaPH.Contains(maPH)
            if(id != "")
            {
                hocSinhs = hocSinhs.Where(hs => hs.MaHS == id);
                ViewBag.MaHS = id;
            }
            if(hoTenHS != "")
            {
                hocSinhs = hocSinhs.Where(hs => (hs.HoHS.ToLower() + " " + hs.TenHS.ToLower()).Contains(hoTenHS.ToLower()));
                ViewBag.HoTenHS = hoTenHS;
            }
            if(maLop != "")
            {
                hocSinhs = hocSinhs.Where(hs => hs.MaLop == maLop);
            }
            if(maPH != "")
            {
                hocSinhs = hocSinhs.Where(hs => hs.MaPH == maPH);
                var pHUHUYNH = db.PHUHUYNHs.Where(ph => ph.MaPH == maPH).ToList();
                ViewBag.MaPH = pHUHUYNH[0].HoPH + " " + pHUHUYNH[0].TenPH;
            }
            ViewBag.MaLop = new SelectList(db.LOPs, "MaLop", "MaLop");
            ViewBag.MaNH = db.NAMHOCs.FirstOrDefault().MaNH;
            return View(hocSinhs.ToList());
        }

        // GET: HOCSINHs/Details/5
        [Authorize(Roles = "User")]
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
            ViewBag.MaQuocTich = new SelectList(db.QUOCTICHes, "MaQuocTich", "TenQuocTich");
            ViewBag.MaTonGiao = new SelectList(db.TONGIAOs, "MaTonGiao", "TenTonGiao");
            return View();
        }

        // POST: HOCSINHs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public ActionResult Create([Bind(Include = "MaHS,HoHS,TenHS,AnhHS,GioiTinh,MaLop,MaPH,NgaySinh,SDT,Email,DiaChi,QueQuan,MaTonGiao,MaQuocTich")] HOCSINH hOCSINH)
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
            ViewBag.MaQuocTich = new SelectList(db.QUOCTICHes, "MaQuocTich", "TenQuocTich", hOCSINH.MaQuocTich);
            ViewBag.MaTonGiao = new SelectList(db.TONGIAOs, "MaTonGiao", "TenTonGiao", hOCSINH.MaTonGiao);
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
            ViewBag.MaQuocTich = new SelectList(db.QUOCTICHes, "MaQuocTich", "TenQuocTich", hOCSINH.MaQuocTich);
            ViewBag.MaTonGiao = new SelectList(db.TONGIAOs, "MaTonGiao", "TenTonGiao", hOCSINH.MaTonGiao);
            return View(hOCSINH);
        }

        // POST: HOCSINHs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public ActionResult Edit([Bind(Include = "MaHS,HoHS,TenHS,AnhHS,GioiTinh,MaLop,MaPH,NgaySinh,SDT,Email,DiaChi,QueQuan,MaTonGiao,MaQuocTich")] HOCSINH hOCSINH)
        {
            var anh = Request.Files["Avatar"];
            if(anh.ContentLength != 0)
            {
                string fileName = System.IO.Path.GetFileName(anh.FileName);
                string savePath = Server.MapPath("/Images/" + fileName);
                anh.SaveAs(savePath);
                hOCSINH.AnhHS = fileName;
            }    
            else
            {
                hOCSINH.AnhHS = db.HOCSINHs.AsNoTracking().Where(s => s.MaHS == hOCSINH.MaHS).Single().AnhHS;
            }

            if (ModelState.IsValid)
            {
                db.Entry(hOCSINH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLop = new SelectList(db.LOPs, "MaLop", "MaLop", hOCSINH.MaLop);
            ViewBag.MaPH = new SelectList(db.PHUHUYNHs.Select(ph => new { ph.MaPH, HoTen = ph.HoPH + " " + ph.TenPH }), "MaPH", "HoTen", hOCSINH.MaPH);
            ViewBag.MaQuocTich = new SelectList(db.QUOCTICHes, "MaQuocTich", "TenQuocTich", hOCSINH.MaQuocTich);
            ViewBag.MaTonGiao = new SelectList(db.TONGIAOs, "MaTonGiao", "TenTonGiao", hOCSINH.MaTonGiao);
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
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
