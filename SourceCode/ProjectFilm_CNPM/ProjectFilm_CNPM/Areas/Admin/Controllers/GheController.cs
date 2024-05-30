
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
    [Authorize(Roles = "Admin")]
    public class GheController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private List<string> loaighe = new List<string>{
            "Đơn",
            "Đôi",
            "VIP"};

        // GET: Admin/Ghe
        public ActionResult Index()
        {
            var list = db.Ghes.Where(m => m.TrangThai != 0).ToList();
            return View(list);
        }

        // GET: Admin/Ghe/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy ghế");
                return RedirectToAction("Index");
            }
            Ghe ghe = db.Ghes.Find(id);
            if (ghe == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy ghế");
                return RedirectToAction("Index");
            }
            return View(ghe);
        }

        // GET: Admin/Ghe/Create
        public ActionResult Create()
        {

            // Chuyển ds ghe thành SelectListItem
            var phonglist = db.Phongs.ToList().Select(p => new SelectListItem
            {
                Value = p.MaPhong.ToString(),
                Text = p.TenPhong
            });
            ViewBag.LoaiGhe = loaighe;
            ViewBag.PhongList = phonglist;
            return View();
        }

        // POST: Admin/Ghe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ghe ghe)
        {
            
            // Chuyển ds ghe thành SelectListItem
            var phonglist = db.Phongs.ToList().Select(p => new SelectListItem
            {
                Value = p.MaPhong.ToString(),
                Text = p.TenPhong
            });

            ViewBag.PhongList = phonglist;
            ViewBag.LoaiGhe = loaighe;

            // Lấy loại ghế mà người dùng đã chọn từ form
            var selectedLoaiGhe = Request.Form["LoaiGhe"];
            bool check = db.Ghes.Any(p => p.TenGhe == ghe.TenGhe && p.MaPhong == ghe.MaPhong);
            if (check)
            {
                ModelState.AddModelError("TenGhe", "Tên ghế đã tồn tại trong phòng đó.");
                return View(ghe);
            }
            if (ModelState.IsValid)
            {
                var tenThamSo = string.Format("Giá vé ({0})", selectedLoaiGhe);
                var thamSoGiaVe = db.ThamSos.FirstOrDefault(ts => ts.TenThamSo == tenThamSo);
                if (thamSoGiaVe != null)
                {
                    // Gán giá vé cho đối tượng ghế
                    ghe.GiaGhe = Convert.ToInt32(thamSoGiaVe.GiaTri);
                }
                else
                {
                    ModelState.AddModelError("", $"Không tìm thấy giá vé cho loại ghế '{selectedLoaiGhe}'");
                    return View(ghe);
                }
                //Xử lý tự động cho các trường sau:
                //---Create At
                ghe.NgayTao = DateTime.Now;
                //---Create By
                ghe.NguoiTao = Convert.ToInt32(Session["UserId"]);
                //Update at
                ghe.NgayCapNhat = DateTime.Now;
                //Update by
                ghe.NguoiCapNhat = Convert.ToInt32(Session["UserId"]);
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
            // Chuyển ds ghe thành SelectListItem
            var phonglist = db.Phongs.ToList().Select(p => new SelectListItem
            {
                Value = p.MaPhong.ToString(),
                Text = p.TenPhong
            });

            ViewBag.PhongList = phonglist;
            ViewBag.LoaiGhe = loaighe;
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy ghế");
                return RedirectToAction("Index");
            }
            Ghe ghe = db.Ghes.Find(id);
            if (ghe == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy ghế");
                return RedirectToAction("Index");
            }
            return View(ghe);
        }

        // POST: Admin/Ghe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Ghe ghe)
        {
            bool check = db.Ghes.Any(p => p.TenGhe == ghe.TenGhe);
            if (check)
            {
                ModelState.AddModelError("TenGhe", "Tên ghế đã tồn tại trong phòng đó.");
                return View(check);
            }
            // Chuyển ds ghe thành SelectListItem
            var phonglist = db.Phongs.ToList().Select(p => new SelectListItem
            {
                Value = p.MaPhong.ToString(),
                Text = p.TenPhong
            });

            ViewBag.PhongList = phonglist;
            ViewBag.LoaiGhe = loaighe;
            // Lấy loại ghế mà người dùng đã chọn từ form
            var selectedLoaiGhe = Request.Form["LoaiGhe"];
            if (ModelState.IsValid)
            {
                var tenThamSo = string.Format("Giá vé ({0})", selectedLoaiGhe);
                var thamSoGiaVe = db.ThamSos.FirstOrDefault(ts => ts.TenThamSo == tenThamSo);

                if (thamSoGiaVe != null)
                {
                    // Gán giá vé cho đối tượng ghế
                    ghe.GiaGhe = Convert.ToInt32(thamSoGiaVe.GiaTri);
                }
                else
                {
                    ModelState.AddModelError("", $"Không tìm thấy giá vé cho loại ghế '{selectedLoaiGhe}'");
                    return View(ghe);
                }
                //Xử lý tự động cho các trường sau:
                //---Create At
                ghe.NgayTao = DateTime.Now;
                //---Create By
                ghe.NguoiTao = Convert.ToInt32(Session["UserId"]);
                //Update at
                ghe.NgayCapNhat = DateTime.Now;
                //Update by
                ghe.NguoiCapNhat = Convert.ToInt32(Session["UserId"]);
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
                TempData["message"] = new XMessage("danger", "Không tìm thấy ghế");
                return RedirectToAction("Index");
            }
            Ghe ghe = db.Ghes.Find(id);
            if (ghe == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy ghế");
                return RedirectToAction("Index");
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

        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                //hien thi thong bao
                TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");
                return RedirectToAction("Index");
            }
            Ghe ghe = db.Ghes.Find(id);
            if (ghe == null)
            {
                TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");
                return RedirectToAction("Index");
            }
            //cap nhat trang thai
            ghe.TrangThai = (ghe.TrangThai == 1) ? 2 : 1;
            //cap nhat update at
            ghe.NgayCapNhat = DateTime.Now;
            //cap nhat update by
            ghe.NguoiCapNhat = Convert.ToInt32(Session["UserID"]);
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
                return RedirectToAction("Index", "Ghe");
            }

            //khi nhap nut thay doi TrangThai cho mot mau tin
            Ghe ghe = db.Ghes.Find(id);
            //kiem tra id cua categories co ton tai?
            if (ghe == null)
            {
                //Thong bao that bai
                TempData["message"] = new XMessage("danger", "Phục hồi dữ liệu thất bại");

                //chuyen huong trang
                return RedirectToAction("Index", "Ghe");
            }
            //thay doi trang thai TrangThai tu 1 thanh 2 va nguoc lai
            ghe.TrangThai = 2;

            //cap nhat gia tri cho Nguoi/Ngay cap nhat
            ghe.NguoiCapNhat = Convert.ToInt32(Session["UserId"].ToString());
            ghe.NgayCapNhat = DateTime.Now;

            //Goi ham Update trong TopicDAO
            db.Entry(ghe).State = EntityState.Modified;
            db.SaveChanges();
            //Thong bao thanh cong
            TempData["message"] = new XMessage("success", "Phục hồi dữ liệu thành công");

            //khi cap nhat xong thi chuyen ve Trash
            return RedirectToAction("Trash", "Ghe");
        }
        public ActionResult DelTrash(int? id)
        {
            //khi nhap nut thay doi Status cho mot mau tin
            Ghe ghe = db.Ghes.Find(id);

            //thay doi trang thai TrangThai tu 1,2 thanh 0
            ghe.TrangThai = 0;

            //cap nhat gia tri cho Nguoi/Ngay cap nhat
            ghe.NguoiCapNhat = Convert.ToInt32(Session["UserId"].ToString());
            ghe.NgayCapNhat = DateTime.Now;

            //Goi ham Update
            db.Entry(ghe).State = EntityState.Modified;
            db.SaveChanges();

            //Thong bao thanh cong
            TempData["message"] = new XMessage("success", "Xóa mẩu tin thành công");

            //khi cap nhat xong thi chuyen ve Index
            return RedirectToAction("Index", "Ghe");
        }
        public ActionResult Trash(int? id)
        {
            var list = db.Ghes.Where(m => m.TrangThai == 0).ToList();
            return View(list);
        }
    }
}
//done