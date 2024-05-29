using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectFilm_CNPM.Library;
using ProjectFilm_CNPM.Models;
using ProjectFilm_CNPM.Models.ERD;

namespace ProjectFilm_CNPM.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PhongController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Phong
        public ActionResult Index()
        {
            var phong = db.Phongs.Where(p => p.TrangThai == 1 || p.TrangThai == 2).ToList();
            return View(phong);
        }

        // GET: Admin/Phong/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Phong phong)
        {
            bool check = db.Phongs.Any(p => p.TenPhong == phong.TenPhong);
            if (check)
            {
                ModelState.AddModelError("TenPhong", "Tên phòng đã tồn tại trong hệ thống.");
                return View(phong);
            }
            if (ModelState.IsValid)
            {
                phong.NgayTao = DateTime.Now;
                //---Create By
                phong.NguoiTao = Convert.ToInt32(Session["UserId"]);
                //Update at
                phong.NgayCapNhat = DateTime.Now;
                //trang thai
                phong.TrangThai = 1;
                //Update by
                phong.NguoiCapNhat = Convert.ToInt32(Session["UserId"]);
                db.Phongs.Add(phong);
                db.SaveChanges();
                //hiển thị thông báo thành công
                TempData["message"] = new XMessage("success", "Tạo mới phòng thành công!");
                return RedirectToAction("Index");
            }

            return View(phong);
        }

        // GET: Admin/Phim/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy phòng");
                return RedirectToAction("Index");
            }
            Phong phong = db.Phongs.Find(id);
            if (phong == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy phòng");
                return RedirectToAction("Index");
            }
            return View(phong);
        }

        // POST: Admin/Phim/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Phong phong)
        {
            bool check = db.Phongs.Any(p => p.TenPhong == phong.TenPhong);
            if (check)
            {
                ModelState.AddModelError("TenPhong", "Tên phòng đã tồn tại trong hệ thống.");
                return View(phong);
            }
            if (ModelState.IsValid)
            {
                //Xử lý tự động cho các trường sau:
                //---Create At
                phong.NgayTao = DateTime.Now;
                //---Create By
                phong.NguoiTao = Convert.ToInt32(Session["UserId"]);
                //Update at
                phong.NgayCapNhat = DateTime.Now;
                //Update by
                phong.NguoiCapNhat = Convert.ToInt32(Session["UserId"]);
                //trang thai
                phong.TrangThai = 1;
                // Lưu thông tin phim vào cơ sở dữ liệu
                db.Entry(phong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(phong);
        }

        // GET: Admin/Phong/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy phòng");
                return RedirectToAction("Index");
            }
            Phong phong = db.Phongs.Find(id);
            if (phong == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy phòng");
                return RedirectToAction("Index");
            }
            return View(phong);
        }

        // POST: Admin/Phong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Phong phong = db.Phongs.Find(id);

            if (phong == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẩu tin để xóa");
                return RedirectToAction("Trash");
            }
            db.Phongs.Remove(phong);
            db.SaveChanges();
            // Thông báo thành công
            TempData["message"] = new XMessage("success", "Xóa danh mục thành công");

            // Trở lại trang thùng rác
            return RedirectToAction("Trash");
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
            Phong phong = db.Phongs.Find(id);
            if (phong == null)
            {
                TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");
                return RedirectToAction("Index");
            }
            //cap nhat trang thai
            phong.TrangThai = (phong.TrangThai == 1) ? 2 : 1;
            //cap nhat update at
            phong.NgayCapNhat = DateTime.Now;
            //cap nhat update by
            phong.NguoiCapNhat = Convert.ToInt32(Session["UserID"]);
            //update db
            db.SaveChanges();
            //hien thi thong bao
            TempData["message"] = new XMessage("success", "Cập nhật trạng thái thành công");
            //tro ve trang Index
            return RedirectToAction("Index");
        }
        // POST: Admin/Product/DelTrash
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                //hien thi thong bao
                TempData["message"] = new XMessage("danger", "Xóa mẩu tin thất bại");
                return RedirectToAction("Index");
            }
            Phong phong = db.Phongs.Find(id);
            if (phong == null)
            {
                TempData["message"] = new XMessage("danger", "Xóa mẩu tin thất bại");
                return RedirectToAction("Index");
            }
            //cap nhat trang thai
            phong.TrangThai = 0;
            //cap nhat NgayCapNhat
            phong.NgayCapNhat = DateTime.Now;
            //cap nhat Nguoi cap nhat
            phong.NguoiCapNhat = Convert.ToInt32(Session["UserID"]);
            //update db
            db.SaveChanges();
            //hien thi thong bao
            TempData["message"] = new XMessage("success", "Xóa mẩu tin thành công");
            //tro ve trang Index
            return RedirectToAction("Index");

        }
        public ActionResult Trash()
        {
            var trashphong = db.Phongs.Where(p => p.TrangThai == 0).ToList();
            return View(trashphong);
        }
        // POST: Admin/Supplier/Undo
        public ActionResult Undo(int? id)
        {
            if (id == null)
            {
                //hien thi thong bao
                TempData["message"] = new XMessage("danger", "Phục hồi mẩu tin thất bại");
                return RedirectToAction("Index");
            }
            Phong phong = db.Phongs.Find(id);
            if (phong == null)
            {
                TempData["message"] = new XMessage("danger", "Phục hồi mẩu tin thất bại");
                return RedirectToAction("Index");
            }
            //cap nhat trang thai = 2
            phong.TrangThai = 2;
            //cap nhat update at

            //cap nhat NgayCapNhat
            phong.NgayCapNhat = DateTime.Now;
            //cap nhat Nguoi cap nhat
            phong.NguoiCapNhat = Convert.ToInt32(Session["UserID"]);
            //update db
            db.SaveChanges();
            //hien thi thong bao
            TempData["message"] = new XMessage("success", "Phục hồi mẩu tin thành công");
            //tro ve trang Index
            return RedirectToAction("Trash");
        }
    }
}