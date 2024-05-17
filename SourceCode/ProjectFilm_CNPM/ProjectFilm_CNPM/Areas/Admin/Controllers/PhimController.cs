using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectFilm_CNPM.Library;
using ProjectFilm_CNPM.Models;
using ProjectFilm_CNPM.Models.ERD;

namespace ProjectFilm_CNPM.Areas.Admin.Controllers
{
    public class PhimController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Phim
        public ActionResult Index()
        {
            return View(db.Phims.ToList());
        }

        // GET: Admin/Phim/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy phim");
                return RedirectToAction("Index");
            }
            Phim phim = db.Phims.Find(id);
            if (phim == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy phim");
            }
            return View(phim);
        }

        // GET: Admin/Phim/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Phim/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Phim phim)
        {
            if (ModelState.IsValid)
            {
                //Xử lý tự động cho các trường sau:
                //---Create At
                phim.NgayTao = DateTime.Now;
                //---Create By
                phim.NguoiTao = Convert.ToInt32(Session["UserId"]);
                //Slug
                phim.TenRutGon = XString.Str_Slug(phim.TenPhim);
                //Update at
                phim.NgayCapNhat = DateTime.Now;
                //Update by
                phim.NguoiCapNhat = Convert.ToInt32(Session["UserId"]);

                db.Phims.Add(phim);
                db.SaveChanges();
                //hiển thị thông báo thành công
                TempData["message"] = new XMessage("success", "Tạo mới phim thành công!");
                return RedirectToAction("Index");
            }

            return View(phim);
        }

        // GET: Admin/Phim/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phim phim = db.Phims.Find(id);
            if (phim == null)
            {
                return HttpNotFound();
            }
            return View(phim);
        }

        // POST: Admin/Phim/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaPhim,TenPhim,TenRutGon,ThoiLuong,Anh,DaoDien,DienVien,QuocGia,NamPhatHanh,PhanLoai,MoTa,NguoiTao,NgayTao,NguoiCapNhat,NgayCapNhat,TrangThai")] Phim phim)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phim).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(phim);
        }

        // GET: Admin/Phim/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phim phim = db.Phims.Find(id);
            if (phim == null)
            {
                return HttpNotFound();
            }
            return View(phim);
        }

        // POST: Admin/Phim/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Phim phim = db.Phims.Find(id);
            db.Phims.Remove(phim);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Admin/Category/Status
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                //hien thi thong bao
                TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");
                return RedirectToAction("Index");
            }
            Phim phim = db.Phims.Find(id);
            if (phim == null)
            {
                TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");
                return RedirectToAction("Index");
            }
            //cap nhat trang thai
            phim.TrangThai = (phim.TrangThai == 1) ? 2 : 1;
            //cap nhat update at
            phim.NgayCapNhat = DateTime.Now;
            //cap nhat update by
            phim.NguoiCapNhat = Convert.ToInt32(Session["UserID"]);
            //update db
            db.SaveChanges();
            //hien thi thong bao
            TempData["message"] = new XMessage("success", "Cập nhật trạng thái thành công");
            //tro ve trang Index
            return RedirectToAction("Index");
        }

    }
}
