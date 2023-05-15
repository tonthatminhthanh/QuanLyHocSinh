using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;
using QuanLyHocSinh.Models;

namespace QuanLyHocSinh.Controllers
{
    [Authorize(Roles = "User")]
    public class KETQUAsController : Controller
    {
        private QLHSHVTEntities db = new QLHSHVTEntities();

        // GET: KETQUAs
        public string GetMax()
        {
            var maMax = db.KETQUAs.ToList().Select(hp => hp.MaKQ).Max();
            var maInt = int.Parse(maMax.Substring(2));
            var maIntNew = maInt + 1;
            return $"KQ{maIntNew:D8}";
        }
        [HttpPost]
        public ActionResult ExportListKQToExcel(List<KETQUA> kETQUAs, string maHS)
        {
            var hOCSINH = db.HOCSINHs.Where(hs => hs.MaHS == maHS).Single();
            KETQUA kETQUA = kETQUAs.FirstOrDefault();
            CTHP cTHP = db.CTHPs.FirstOrDefault(hp => hp.MaCTHP == kETQUA.MaCTHP);
            NAMHOC nAMHOC = db.NAMHOCs.Where(nh => nh.MaNH == cTHP.MaNH).Single();
            string tenHS = hOCSINH.HoHS + " " + hOCSINH.TenHS;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add("Danh sách điểm của học sinh " + tenHS);
            excelWorksheet.Cells[1, 1].Value = "KẾT QUẢ HỌC TẬP HỌC SINH";
            excelWorksheet.Cells[2, 1].Value = "Họ và tên học sinh: " + tenHS;
            excelWorksheet.Cells[3, 1].Value = "Năm học: " + nAMHOC.NamBatDau + " - " + nAMHOC.NamKetThuc;
            excelWorksheet.Cells[4, 1].Value = "STT";
            excelWorksheet.Cells[4, 2].Value = "Tên môn học";
            excelWorksheet.Cells[4, 3].Value = "Điểm quá trình";
            excelWorksheet.Cells[4, 4].Value = "Điểm 1 tiết";
            excelWorksheet.Cells[4, 5].Value = "Điểm cuối kỳ";
            excelWorksheet.Cells[4, 6].Value = "Điểm trung bình";
            int stt = 1, rowNum = 5;
            float sum = 0.0f;
            foreach(var kq in kETQUAs)
            {
                var hp = db.CTHPs.FirstOrDefault(c => c.MaCTHP == kq.MaCTHP);
                string mh = db.MONHOCs.FirstOrDefault(m => m.MaMH == hp.MaMH).TenMH;
                excelWorksheet.Cells[rowNum, 1].Value = stt;
                excelWorksheet.Cells[rowNum, 2].Value = mh;
                excelWorksheet.Cells[rowNum, 3].Value = kq.DiemQT;
                excelWorksheet.Cells[rowNum, 4].Value = kq.Diem1T;
                excelWorksheet.Cells[rowNum, 5].Value = kq.DiemKT;
                excelWorksheet.Cells[rowNum, 6].Value = kq.DiemTB;
                sum += (float)kq.DiemTB;
                stt++;
                rowNum++;
            }
            excelWorksheet.Cells[rowNum, 5].Value = "Điểm tổng";
            excelWorksheet.Cells[rowNum, 6].Value = Math.Round(sum / (float)(stt - 1),1);

            byte[] data = excelPackage.GetAsByteArray();

            return File(data,
                "\"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet\"",
                "DanhSachKQHT_" + tenHS + "_" + nAMHOC.NamBatDau + "-" + nAMHOC.NamKetThuc + ".xls");
        }

        public ActionResult Index(string maHS, string maNH)
        {
            if (maHS == null || maHS == "")
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var nAMHOCs = db.NAMHOCs.Select(nh => new { MaNH = nh.MaNH, NamHoc = nh.NamBatDau + " - " + nh.NamKetThuc });
            ViewBag.MaHS = maHS;
            ViewBag.MaNH = new SelectList(nAMHOCs, "MaNH", "NamHoc");
            var kETQUAs = db.KETQUAs.Include(k => k.CTHP).Where(k => k.CTHP.MaHS == maHS && k.CTHP.MaNH.Contains(maNH));
            return View(kETQUAs.ToList());
        }
        // GET: KETQUAs/Create
        public ActionResult Create(string maHS)
        {
            var filteredCTHPList = db.CTHPs.Where(c => c.MaHS == maHS && !db.KETQUAs.Any(k => k.MaCTHP == c.MaCTHP))
                .Select(c => new
                {
                    c.MaCTHP,
                    info = "Môn: " + db.MONHOCs.FirstOrDefault(m => m.MaMH == c.MaMH).TenMH 
                    + " , năm học " 
                    + db.NAMHOCs.FirstOrDefault(n => n.MaNH == c.MaNH).NamBatDau 
                    + " - " 
                    + db.NAMHOCs.FirstOrDefault(n => n.MaNH == c.MaNH).NamKetThuc})
                .ToList(); 
            ViewBag.MaCTHP = new SelectList(filteredCTHPList, "MaCTHP", "info");
            ViewBag.MaHS = maHS;
            ViewBag.MaNH = db.NAMHOCs.FirstOrDefault().MaNH;
            return View();
        }

        // POST: KETQUAs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaKQ,MaCTHP,DiemQT,Diem1T,DiemKT,DiemTB")] KETQUA kETQUA)
        {
            var cTHP = db.CTHPs.Where(hp => hp.MaCTHP == kETQUA.MaCTHP).Single();
            string id = db.HOCSINHs.Where(hs => hs.MaHS == cTHP.MaHS).Single().MaHS;
            kETQUA.MaKQ = GetMax();
            float avg = (float)(kETQUA.DiemQT * 0.3f + kETQUA.Diem1T * 0.3f + kETQUA.DiemKT * 0.4f);
            kETQUA.DiemTB = Math.Round(avg, 1);
            if (ModelState.IsValid)
            {
                db.KETQUAs.Add(kETQUA);
                db.SaveChanges();
                return RedirectToAction("Index", new { maHS = id, maNH = db.NAMHOCs.FirstOrDefault().MaNH });
            }

            var filteredCTHPList = db.CTHPs.Where(c => c.MaHS == id && !db.KETQUAs.Any(k => k.MaCTHP == c.MaCTHP))
                .Select(c => new
                {
                    c.MaCTHP,
                    info = "Môn: " + db.MONHOCs.FirstOrDefault(m => m.MaMH == c.MaMH).TenMH
                    + " , năm học "
                    + db.NAMHOCs.FirstOrDefault(n => n.MaNH == c.MaNH).NamBatDau
                    + " - "
                    + db.NAMHOCs.FirstOrDefault(n => n.MaNH == c.MaNH).NamKetThuc
                })
                .ToList();
            ViewBag.MaCTHP = new SelectList(filteredCTHPList, "MaCTHP", "info", kETQUA.MaCTHP);
            return View(kETQUA);
        }

        // GET: KETQUAs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KETQUA kETQUA = db.KETQUAs.Find(id);
            if (kETQUA == null)
            {
                return HttpNotFound();
            }
            var cTHP = db.CTHPs.Where(hp => hp.MaCTHP == kETQUA.MaCTHP).Single();
            string ma = db.HOCSINHs.Where(hs => hs.MaHS == cTHP.MaHS).Single().MaHS;
            ViewBag.MaHS = ma;
            var filteredCTHPList = db.CTHPs.Where(c => c.MaHS == ma && !db.KETQUAs.Any(k => k.MaCTHP == c.MaCTHP))
                .Select(c => new
                {
                    c.MaCTHP,
                    info = "Môn: " + db.MONHOCs.FirstOrDefault(m => m.MaMH == c.MaMH).TenMH
                    + " , năm học "
                    + db.NAMHOCs.FirstOrDefault(n => n.MaNH == c.MaNH).NamBatDau
                    + " - "
                    + db.NAMHOCs.FirstOrDefault(n => n.MaNH == c.MaNH).NamKetThuc
                })
                .ToList();
            ViewBag.MaCTHP = new SelectList(filteredCTHPList, "MaCTHP", "info", kETQUA.MaCTHP);
            ViewBag.MaNH = db.NAMHOCs.FirstOrDefault().MaNH;
            return View(kETQUA);
        }

        // POST: KETQUAs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKQ,MaCTHP,DiemQT,Diem1T,DiemKT,DiemTB")] KETQUA kETQUA)
        {
            var cTHP = db.CTHPs.Where(hp => hp.MaCTHP == kETQUA.MaCTHP).Single();
            string id = db.HOCSINHs.Where(hs => hs.MaHS == cTHP.MaHS).Single().MaHS;

            float avg = (float)(kETQUA.DiemQT * 0.3f + kETQUA.Diem1T * 0.3f + kETQUA.DiemKT * 0.4f);
            kETQUA.DiemTB = Math.Round(avg, 1);
            if (ModelState.IsValid)
            {
                db.Entry(kETQUA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { maHS = id, maNH = db.NAMHOCs.FirstOrDefault().MaNH });
            }
            
            var filteredCTHPList = db.CTHPs.Where(c => c.MaHS == id && !db.KETQUAs.Any(k => k.MaCTHP == c.MaCTHP))
                .Select(c => new
                {
                    c.MaCTHP,
                    info = "Môn: " + db.MONHOCs.FirstOrDefault(m => m.MaMH == c.MaMH).TenMH
                    + " , năm học "
                    + db.NAMHOCs.FirstOrDefault(n => n.MaNH == c.MaNH).NamBatDau
                    + " - "
                    + db.NAMHOCs.FirstOrDefault(n => n.MaNH == c.MaNH).NamKetThuc
                })
                .ToList();
            ViewBag.MaCTHP = new SelectList(filteredCTHPList, "MaCTHP", "info", kETQUA.MaCTHP);
            return View(kETQUA);
        }

        // GET: KETQUAs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KETQUA kETQUA = db.KETQUAs.Find(id);
            if (kETQUA == null)
            {
                return HttpNotFound();
            }
            var cTHP = db.CTHPs.Where(hp => hp.MaCTHP == kETQUA.MaCTHP).Single();
            string ma = db.HOCSINHs.Where(hs => hs.MaHS == cTHP.MaHS).Single().MaHS;
            ViewBag.MaHS = ma;
            ViewBag.MaNH = db.NAMHOCs.FirstOrDefault().MaNH;
            return View(kETQUA);
        }

        // POST: KETQUAs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            KETQUA kETQUA = db.KETQUAs.Find(id);
            var cTHP = db.CTHPs.Where(hp => hp.MaCTHP == kETQUA.MaCTHP).Single();
            string ma = db.HOCSINHs.Where(hs => hs.MaHS == cTHP.MaHS).Single().MaHS;
            db.KETQUAs.Remove(kETQUA);
            db.SaveChanges();
            return RedirectToAction("Index", new { maHS = ma, maNH = db.NAMHOCs.FirstOrDefault().MaNH });
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
