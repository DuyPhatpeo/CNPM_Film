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
    public class TheLoaiController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/TheLoai
        public ActionResult Index()
        {
            var theLoais = db.TheLoais.Include(t => t.Phim);
            return View(theLoais.ToList());
        }

        // GET: Admin/TheLoai/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TheLoai theLoai = db.TheLoais.Find(id);
            if (theLoai == null)
            {
                return HttpNotFound();
            }
            return View(theLoai);
        }

        // GET: Admin/TheLoai/Create
        public ActionResult Create()
        {
            ViewBag.MaPhim = new SelectList(db.Phims, "MaPhim", "TenPhim");
            return View();
        }

        // POST: Admin/TheLoai/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MaPhim,TenTheLoai,NguoiTao,NgayTao,NguoiCapNhat,NgayCapNhat,TrangThai")] TheLoai theLoai)
        {
            if (ModelState.IsValid)
            {
                db.TheLoais.Add(theLoai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaPhim = new SelectList(db.Phims, "MaPhim", "TenPhim", theLoai.MaPhim);
            return View(theLoai);
        }

        // GET: Admin/TheLoai/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TheLoai theLoai = db.TheLoais.Find(id);
            if (theLoai == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaPhim = new SelectList(db.Phims, "MaPhim", "TenPhim", theLoai.MaPhim);
            return View(theLoai);
        }

        // POST: Admin/TheLoai/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MaPhim,TenTheLoai,NguoiTao,NgayTao,NguoiCapNhat,NgayCapNhat,TrangThai")] TheLoai theLoai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(theLoai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaPhim = new SelectList(db.Phims, "MaPhim", "TenPhim", theLoai.MaPhim);
            return View(theLoai);
        }

        // GET: Admin/TheLoai/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TheLoai theLoai = db.TheLoais.Find(id);
            if (theLoai == null)
            {
                return HttpNotFound();
            }
            return View(theLoai);
        }

        // POST: Admin/TheLoai/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TheLoai theLoai = db.TheLoais.Find(id);
            db.TheLoais.Remove(theLoai);
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
