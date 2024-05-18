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
    public class SuatChieuController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/SuatChieu
        public ActionResult Index()
        {
            var suatChieus = db.SuatChieus.Where(m => m.TrangThai != 0).ToList(); 
            return View(suatChieus);
        }

        // GET: Admin/SuatChieu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy suất chiếu");
                return RedirectToAction("Index");
            }
            SuatChieu suatChieu = db.SuatChieus.Find(id);
            if (suatChieu == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy suất chiếu");
                return RedirectToAction("Index");
            }
            return View(suatChieu);
        }

        // GET: Admin/SuatChieu/Create
        public ActionResult Create()
        {
            var danhSachPhim = db.Phims.ToList();

            // Chuyển ds phim thành SelectListItem
            var phimList = danhSachPhim.Select(p => new SelectListItem
            {
                Value = p.MaPhim.ToString(),
                Text = p.TenPhim
            });

            ViewBag.PhimList = phimList;
            return View();
        }

        // POST: Admin/SuatChieu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SuatChieu suatChieu)
        {
            // Lấy ds phim từ csdl
            var danhSachPhim = db.Phims.ToList();

            // Chuyển ds phim thành SelectListItem
            var phimList = danhSachPhim.Select(p => new SelectListItem
            {
                Value = p.MaPhim.ToString(),
                Text = p.TenPhim
            });
            if (ModelState.IsValid)
            {
                
                //Xử lý tự động cho các trường sau:
                //---NgayTao
                suatChieu.NgayTao = DateTime.Now;
                //---NguoiTao
                suatChieu.NguoiTao = Convert.ToInt32(Session["UserId"]);
                //NgayCapNhat
                suatChieu.NgayCapNhat = DateTime.Now;
                //NguoiCapNhat
                suatChieu.NguoiCapNhat = Convert.ToInt32(Session["UserId"]);
                db.SuatChieus.Add(suatChieu);
                //hiển thị thông báo thành công
                TempData["message"] = new XMessage("success", "Tạo mới suất chiếu thành công!");
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(suatChieu);
        }

        // GET: Admin/SuatChieu/Edit/5
        public ActionResult Edit(int? id)
        {
            // Lấy ds phim từ csdl
            var danhSachPhim = db.Phims.ToList();

            // Chuyển ds phim thành SelectListItem
            var phimList = danhSachPhim.Select(p => new SelectListItem
            {
                Value = p.MaPhim.ToString(),
                Text = p.TenPhim
            });

            ViewBag.PhimList = phimList;
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy suất chiếu");
                return RedirectToAction("Index");
            }
            SuatChieu suatChieu = db.SuatChieus.Find(id);
            if (suatChieu == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy suất chiếu");
                return RedirectToAction("Index");
            }
            return View(suatChieu);
        }

        // POST: Admin/SuatChieu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SuatChieu suatChieu)
        {
            // Lấy ds phim từ csdl
            var danhSachPhim = db.Phims.ToList();

            // Chuyển ds phim thành SelectListItem
            var phimList = danhSachPhim.Select(p => new SelectListItem
            {
                Value = p.MaPhim.ToString(),
                Text = p.TenPhim
            });

            ViewBag.PhimList = phimList;
            if (ModelState.IsValid)
            {
                //Xử lý tự động cho các trường sau:
                //---NgayTao
                suatChieu.NgayTao = DateTime.Now;
                //---NguoiTao
                suatChieu.NguoiTao = Convert.ToInt32(Session["UserId"]);
                //NgayCapNhat
                suatChieu.NgayCapNhat = DateTime.Now;
                //NguoiCapNhat
                suatChieu.NguoiCapNhat = Convert.ToInt32(Session["UserId"]);
                //Thong bao thanh cong
                TempData["message"] = new XMessage("success", "Sửa suất chiếu thành công");
                db.Entry(suatChieu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(suatChieu);
        }

        // GET: Admin/SuatChieu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy suất chiếu");
                return RedirectToAction("Index");
            }
            SuatChieu suatChieu = db.SuatChieus.Find(id);
            if (suatChieu == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy suất chiếu");
                return RedirectToAction("Index");
            }
            return View(suatChieu);
        }

        // POST: Admin/SuatChieu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SuatChieu suatChieu = db.SuatChieus.Find(id);
            //Thong bao thanh cong
            TempData["message"] = new XMessage("success", "Xóa suất chiếu thành công");
            db.SuatChieus.Remove(suatChieu);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                //hien thi thong bao
                TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");
                return RedirectToAction("Index");
            }
            SuatChieu suatChieu = db.SuatChieus.Find(id);
            if (suatChieu == null)
            {
                TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");
                return RedirectToAction("Index");
            }
            //cap nhat trang thai
            suatChieu.TrangThai = (suatChieu.TrangThai == 1) ? 2 : 1;
            //cap nhat update at
            suatChieu.NgayCapNhat = DateTime.Now;
            //cap nhat update by
            suatChieu.NguoiCapNhat = Convert.ToInt32(Session["UserID"]);
            //update db
            db.SaveChanges();
            //hien thi thong bao
            TempData["message"] = new XMessage("success", "Cập nhật trạng thái thành công");
            //tro ve trang Index
            return RedirectToAction("Index");
        }
        public ActionResult Undo(int? id)
        {
            if (id == null)
            {
                //Thong bao that bai
                TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");
                //chuyen huong trang
                return RedirectToAction("Index", "SuatChieu");
            }

            //khi nhap nut thay doi TrangThai cho mot mau tin
            SuatChieu suatChieu = db.SuatChieus.Find(id);
            //kiem tra id cua categories co ton tai?
            if (suatChieu == null)
            {
                //Thong bao that bai
                TempData["message"] = new XMessage("danger", "Phục hồi dữ liệu thất bại");

                //chuyen huong trang
                return RedirectToAction("Index", "SuatChieu");
            }
            //thay doi trang thai TrangThai tu 1 thanh 2 va nguoc lai
            suatChieu.TrangThai = 2;

            //cap nhat gia tri cho Nguoi/Ngay cap nhat
            suatChieu.NguoiCapNhat = Convert.ToInt32(Session["UserId"].ToString());
            suatChieu.NgayCapNhat = DateTime.Now;

            //Goi ham Update trong TopicDAO
            db.Entry(suatChieu).State = EntityState.Modified;
            db.SaveChanges();
            //Thong bao thanh cong
            TempData["message"] = new XMessage("success", "Phục hồi dữ liệu thành công");

            //khi cap nhat xong thi chuyen ve Trash
            return RedirectToAction("Trash", "SuatChieu");
        }

        public ActionResult DelTrash(int? id)
        {
            //khi nhap nut thay doi Status cho mot mau tin
            SuatChieu suatChieu = db.SuatChieus.Find(id);

            //thay doi trang thai TrangThai tu 1,2 thanh 0
            suatChieu.TrangThai = 0;

            //cap nhat gia tri cho Nguoi/Ngay cap nhat
            suatChieu.NguoiCapNhat = Convert.ToInt32(Session["UserId"].ToString());
            suatChieu.NgayCapNhat = DateTime.Now;

            //Goi ham Update
            db.Entry(suatChieu).State = EntityState.Modified;
            db.SaveChanges();

            //Thong bao thanh cong
            TempData["message"] = new XMessage("success", "Xóa mẩu tin thành công");

            //khi cap nhat xong thi chuyen ve Index
            return RedirectToAction("Index", "SuatChieu");
        }
        public ActionResult Trash(int? id)
        {
            var list = db.SuatChieus.Where(m => m.TrangThai == 0).ToList();
            return View(list);
        }
    }
}
