using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectFilm_CNPM.Models;
using ProjectFilm_CNPM.Models.ERD;

namespace ProjectFilm_CNPM.Areas.Admin.Controllers
{
    public class SuatChieuController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/SuatChieu
        public ActionResult Index()
        {
            var suatChieus = db.SuatChieus.Include(s => s.Phim);
            return View(suatChieus.ToList());
        }

        // GET: Admin/SuatChieu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuatChieu suatChieu = db.SuatChieus.Find(id);
            if (suatChieu == null)
            {
                return HttpNotFound();
            }
            return View(suatChieu);
        }

        // GET: Admin/SuatChieu/Create
        public ActionResult Create()
        {
            ViewBag.MaPhim = new SelectList(db.Phims, "MaPhim", "TenPhim");
            return View();
        }

        // POST: Admin/SuatChieu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSuatChieu,MaPhim,GioChieu,NguoiTao,NgayTao,NguoiCapNhat,NgayCapNhat,TrangThai")] SuatChieu suatChieu)
        {
            if (ModelState.IsValid)
            {
                db.SuatChieus.Add(suatChieu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaPhim = new SelectList(db.Phims, "MaPhim", "TenPhim", suatChieu.MaPhim);
            return View(suatChieu);
        }

        // GET: Admin/SuatChieu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuatChieu suatChieu = db.SuatChieus.Find(id);
            if (suatChieu == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaPhim = new SelectList(db.Phims, "MaPhim", "TenPhim", suatChieu.MaPhim);
            return View(suatChieu);
        }

        // POST: Admin/SuatChieu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSuatChieu,MaPhim,GioChieu,NguoiTao,NgayTao,NguoiCapNhat,NgayCapNhat,TrangThai")] SuatChieu suatChieu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(suatChieu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaPhim = new SelectList(db.Phims, "MaPhim", "TenPhim", suatChieu.MaPhim);
            return View(suatChieu);
        }

        // GET: Admin/SuatChieu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuatChieu suatChieu = db.SuatChieus.Find(id);
            if (suatChieu == null)
            {
                return HttpNotFound();
            }
            return View(suatChieu);
        }

        // POST: Admin/SuatChieu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SuatChieu suatChieu = db.SuatChieus.Find(id);
            db.SuatChieus.Remove(suatChieu);
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
