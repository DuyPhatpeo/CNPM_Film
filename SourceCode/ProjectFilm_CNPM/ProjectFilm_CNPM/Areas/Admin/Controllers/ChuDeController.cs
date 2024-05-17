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
    public class ChuDeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/ChuDe
        public ActionResult Index()
        {
            var chuDes = db.ChuDes.Include(c => c.Phim);
            return View(chuDes.ToList());
        }

        // GET: Admin/ChuDe/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChuDe chuDe = db.ChuDes.Find(id);
            if (chuDe == null)
            {
                return HttpNotFound();
            }
            return View(chuDe);
        }

        // GET: Admin/ChuDe/Create
        public ActionResult Create()
        {
            ViewBag.MaPhim = new SelectList(db.Phims, "MaPhim", "TenPhim");
            return View();
        }

        // POST: Admin/ChuDe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MaPhim,TenRutGon,MoTa,TuKhoa,TenChuDe,NguoiTao,NgayTao,NguoiCapNhat,NgayCapNhat,TrangThai")] ChuDe chuDe)
        {
            if (ModelState.IsValid)
            {
                db.ChuDes.Add(chuDe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaPhim = new SelectList(db.Phims, "MaPhim", "TenPhim", chuDe.MaPhim);
            return View(chuDe);
        }

        // GET: Admin/ChuDe/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChuDe chuDe = db.ChuDes.Find(id);
            if (chuDe == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaPhim = new SelectList(db.Phims, "MaPhim", "TenPhim", chuDe.MaPhim);
            return View(chuDe);
        }

        // POST: Admin/ChuDe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MaPhim,TenRutGon,MoTa,TuKhoa,TenChuDe,NguoiTao,NgayTao,NguoiCapNhat,NgayCapNhat,TrangThai")] ChuDe chuDe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chuDe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaPhim = new SelectList(db.Phims, "MaPhim", "TenPhim", chuDe.MaPhim);
            return View(chuDe);
        }

        // GET: Admin/ChuDe/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChuDe chuDe = db.ChuDes.Find(id);
            if (chuDe == null)
            {
                return HttpNotFound();
            }
            return View(chuDe);
        }

        // POST: Admin/ChuDe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChuDe chuDe = db.ChuDes.Find(id);
            db.ChuDes.Remove(chuDe);
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
