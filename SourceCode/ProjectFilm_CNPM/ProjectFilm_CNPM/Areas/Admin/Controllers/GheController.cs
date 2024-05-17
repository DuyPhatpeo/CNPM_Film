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
    public class GheController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Ghe
        public ActionResult Index()
        {
            var ghes = db.Ghes.Include(g => g.Phong);
            return View(ghes.ToList());
        }

        // GET: Admin/Ghe/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ghe ghe = db.Ghes.Find(id);
            if (ghe == null)
            {
                return HttpNotFound();
            }
            return View(ghe);
        }

        // GET: Admin/Ghe/Create
        public ActionResult Create()
        {
            ViewBag.MaPhong = new SelectList(db.Phongs, "MaPhong", "TenPhong");
            return View();
        }

        // POST: Admin/Ghe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaGhe,TenGhe,MaPhong,LoaiGhe,GiaGhe,NguoiTao,NgayTao,NguoiCapNhat,NgayCapNhat,TrangThai")] Ghe ghe)
        {
            if (ModelState.IsValid)
            {
                db.Ghes.Add(ghe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaPhong = new SelectList(db.Phongs, "MaPhong", "TenPhong", ghe.MaPhong);
            return View(ghe);
        }

        // GET: Admin/Ghe/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ghe ghe = db.Ghes.Find(id);
            if (ghe == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaPhong = new SelectList(db.Phongs, "MaPhong", "TenPhong", ghe.MaPhong);
            return View(ghe);
        }

        // POST: Admin/Ghe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaGhe,TenGhe,MaPhong,LoaiGhe,GiaGhe,NguoiTao,NgayTao,NguoiCapNhat,NgayCapNhat,TrangThai")] Ghe ghe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ghe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaPhong = new SelectList(db.Phongs, "MaPhong", "TenPhong", ghe.MaPhong);
            return View(ghe);
        }

        // GET: Admin/Ghe/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ghe ghe = db.Ghes.Find(id);
            if (ghe == null)
            {
                return HttpNotFound();
            }
            return View(ghe);
        }

        // POST: Admin/Ghe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ghe ghe = db.Ghes.Find(id);
            db.Ghes.Remove(ghe);
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
