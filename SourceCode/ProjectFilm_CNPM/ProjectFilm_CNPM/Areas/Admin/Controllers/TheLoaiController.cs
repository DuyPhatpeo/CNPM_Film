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
    public class TheLoaiController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private List<string> genres = new List<string>{
            "Phiêu lưu - Hành động",
            "Tình cảm",
            "Hài kịch",
            "Khoa học - viễn tưởng",
            "Tài liệu"};

        // GET: Admin/TheLoai
        public ActionResult Index()
        {
            var list = db.TheLoais.Where(m=> m.TrangThai !=0).ToList();
            return View(list);
        }

        // GET: Admin/TheLoai/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy thể loại");
                return RedirectToAction("Index");
            }
            TheLoai theLoai = db.TheLoais.Find(id);
            if (theLoai == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy thể loại");
            }
            return View(theLoai);
        }

        // GET: Admin/TheLoai/Create
        public ActionResult Create()
        {
            // Lấy ds phim từ csdl
            var danhSachPhim = db.Phims.ToList();
            ViewBag.Genres = new SelectList(genres);

            // Chuyển ds phim thành SelectListItem
            var phimList = danhSachPhim.Select(p => new SelectListItem
            {
                Value = p.MaPhim.ToString(),
                Text = p.TenPhim
            });

            ViewBag.PhimList = phimList;
            return View();
        }

        // POST: Admin/TheLoai/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TheLoai theLoai)
        {
            ViewBag.Genres = new SelectList(genres);
            // Lấy ds phim từ csdl
            var danhSachPhim = db.Phims.ToList();

            // Chuyển ds phim thành SelectListItem
            var phimList = danhSachPhim.Select(p => new SelectListItem
            {
                Value = p.MaPhim.ToString(),
                Text = p.TenPhim
            });

            ViewBag.PhimList = phimList;
            // Lấy danh sách thể loại từ cơ sở dữ liệu
            var existingGenres = db.TheLoais.Select(tl => tl.TenTheLoai.ToLower()).ToList();

            // Kiểm tra xem thể loại mới đã tồn tại chưa
            if (existingGenres.Contains(theLoai.TenTheLoai.ToLower()))
            {
                // Nếu thể loại đã tồn tại, hiển thị thông báo lỗi và trả về view Create
                ModelState.AddModelError("TenTheLoai", "Thể loại này đã tồn tại.");
                return View(theLoai);
            }
            if (ModelState.IsValid)
            {
                //Xử lý tự động cho các trường sau:
                //---Create At
                theLoai.NgayTao = DateTime.Now;
                //---Create By
                theLoai.NguoiTao = Convert.ToInt32(Session["UserId"]);
                //Update at
                theLoai.NgayCapNhat = DateTime.Now;
                //Update by
                theLoai.NguoiCapNhat = Convert.ToInt32(Session["UserId"]);
                
                db.TheLoais.Add(theLoai);
                db.SaveChanges();
                //hiển thị thông báo thành công
                TempData["message"] = new XMessage("success", "Tạo mới thể loại thành công!");
                return RedirectToAction("Index");
            }

            return View(theLoai);
        }

        // GET: Admin/TheLoai/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Genres = new SelectList(genres);
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
                TempData["message"] = new XMessage("danger", "Không tìm thấy thể loại");
                return RedirectToAction("Index");
            }
            TheLoai theLoai = db.TheLoais.Find(id);
            if (theLoai == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy thể loại");
                return RedirectToAction("Index");
            }
            return View(theLoai);
        }

        // POST: Admin/TheLoai/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TheLoai theLoai)
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
            // Lấy danh sách thể loại từ cơ sở dữ liệu
            var existingGenres = db.TheLoais.Select(tl => tl.TenTheLoai.ToLower()).ToList();

            // Kiểm tra xem thể loại mới đã tồn tại chưa
            if (existingGenres.Contains(theLoai.TenTheLoai.ToLower()))
            {
                // Nếu thể loại đã tồn tại, hiển thị thông báo lỗi và trả về view Create
                ModelState.AddModelError("TenTheLoai", "Thể loại này đã tồn tại.");
                return View(theLoai);
            }
            if (ModelState.IsValid)
            {
                //Xử lý tự động cho các trường sau:
                //---Create At
                theLoai.NgayTao = DateTime.Now;
                //---Create By
                theLoai.NguoiTao = Convert.ToInt32(Session["UserId"]);
                //Update at
                theLoai.NgayCapNhat = DateTime.Now;
                //Update by
                theLoai.NguoiCapNhat = Convert.ToInt32(Session["UserId"]);
                
                 // Lưu thông tin TheLoai vào cơ sở dữ liệu
                db.Entry(theLoai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(theLoai);
        }

        // GET: Admin/TheLoai/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy thể loại");
                return RedirectToAction("Index");
            }
            TheLoai theLoai = db.TheLoais.Find(id);
            if (theLoai == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy thể loại");
                return RedirectToAction("Index");
            }
            return View(theLoai);
        }

        // POST: Admin/TheLoai/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TheLoai theLoai = db.TheLoais.Find(id);

            if (theLoai == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẩu tin để xóa");
                return RedirectToAction("Trash");
            }
            db.TheLoais.Remove(theLoai);
            db.SaveChanges();

            // Thông báo thành công
            TempData["message"] = new XMessage("success", "Xóa danh mục thành công");

            // Trở lại trang thùng rác
            return RedirectToAction("Trash");
        }


        // POST: Admin/TheLoai/Status
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                //hien thi thong bao
                TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");
                return RedirectToAction("Index");
            }
            TheLoai theLoai = db.TheLoais.Find(id);
            if (theLoai == null)
            {
                TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");
                return RedirectToAction("Index");
            }
            //cap nhat trang thai
            theLoai.TrangThai = (theLoai.TrangThai == 1) ? 2 : 1;
            //cap nhat update at
            theLoai.NgayCapNhat = DateTime.Now;
            //cap nhat update by
            theLoai.NguoiCapNhat = Convert.ToInt32(Session["UserID"]);
            //update db
            db.SaveChanges();
            //hien thi thong bao
            TempData["message"] = new XMessage("success", "Cập nhật trạng thái thành công");
            //tro ve trang Index
            return RedirectToAction("Index");
        }
        // POST: Admin/TheLoai/DelTrash
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                //hien thi thong bao
                TempData["message"] = new XMessage("danger", "Xóa mẩu tin thất bại");
                return RedirectToAction("Index");
            }
            TheLoai theLoai = db.TheLoais.Find(id);
            if (theLoai == null)
            {
                TempData["message"] = new XMessage("danger", "Xóa mẩu tin thất bại");
                return RedirectToAction("Index");
            }
            //cap nhat trang thai
            theLoai.TrangThai = 0;
            //cap nhat NgayCapNhat
            theLoai.NgayCapNhat = DateTime.Now;
            //cap nhat Nguoi cap nhat
            theLoai.NguoiCapNhat = Convert.ToInt32(Session["UserID"]);
            //update db
            db.SaveChanges();
            //hien thi thong bao
            TempData["message"] = new XMessage("success", "Xóa mẩu tin thành công");
            //tro ve trang Index
            return RedirectToAction("Index");

        }
        public ActionResult Trash()
        {
            var trash = db.TheLoais.Where(p => p.TrangThai == 0).ToList();
            return View(trash);
        }
        // POST: Admin/TheLoai/Undo
        public ActionResult Undo(int? id)
        {
            if (id == null)
            {
                //hien thi thong bao
                TempData["message"] = new XMessage("danger", "Phục hồi mẩu tin thất bại");
                return RedirectToAction("Index");
            }
            TheLoai theLoai = db.TheLoais.Find(id);
            if (theLoai == null)
            {
                TempData["message"] = new XMessage("danger", "Phục hồi mẩu tin thất bại");
                return RedirectToAction("Index");
            }
            //cap nhat trang thai = 2
            theLoai.TrangThai = 2;
            //cap nhat update at

            //cap nhat NgayCapNhat
            theLoai.NgayCapNhat = DateTime.Now;
            //cap nhat Nguoi cap nhat
            theLoai.NguoiCapNhat = Convert.ToInt32(Session["UserID"]);
            //update db
            db.SaveChanges();
            //hien thi thong bao
            TempData["message"] = new XMessage("success", "Phục hồi mẩu tin thành công");
            //tro ve trang Index
            return RedirectToAction("Trash");
        }
    }
}
