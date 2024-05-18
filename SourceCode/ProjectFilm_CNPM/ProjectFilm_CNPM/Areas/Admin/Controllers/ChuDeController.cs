using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using ProjectFilm_CNPM.Library;
using ProjectFilm_CNPM.Models;
using ProjectFilm_CNPM.Models.ERD;

namespace ProjectFilm_CNPM.Areas.Admin.Controllers
{
    public class ChuDeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var list = db.ChuDes.Where(m => m.TrangThai != 0).ToList();
            
            return View(list.ToList());
        }

        // GET: Admin/ChuDe/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy chủ đề");
                return RedirectToAction("Index");
            }
            ChuDe chuDe = db.ChuDes.Find(id);
            if (chuDe == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy chủ đề");
                return RedirectToAction("Index");
            }
            return View(chuDe);
        }

        // GET: Admin/ChuDe/Create
        public ActionResult Create()
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

            return View();
        }

        // POST: Admin/ChuDe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ChuDe chuDe)
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
                //---Create At
                chuDe.NgayTao = DateTime.Now;
                //---Create By
                chuDe.NguoiTao = Convert.ToInt32(Session["UserId"]);
                //Slug
                chuDe.TenRutGon = XString.Str_Slug(chuDe.TenChuDe);
                //Update at
                chuDe.NgayCapNhat = DateTime.Now;
                //Update by
                chuDe.NguoiCapNhat = Convert.ToInt32(Session["UserId"]);
                db.ChuDes.Add(chuDe);
                db.SaveChanges();
                //hiển thị thông báo thành công
                TempData["message"] = new XMessage("success", "Tạo mới chủ đề thành công!");
                return RedirectToAction("Index");
            }
            return View(chuDe);
        }

        // GET: Admin/ChuDe/Edit/5
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
                TempData["message"] = new XMessage("danger", "Không tìm thấy chủ đề");
                return RedirectToAction("Index");
            }
            ChuDe chuDe = db.ChuDes.Find(id);
            if (chuDe == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy chủ đề");
                return RedirectToAction("Index");
            }
            return View(chuDe);
        }

        // POST: Admin/ChuDe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ChuDe chuDe)
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
                //---Create At
                chuDe.NgayTao = DateTime.Now;
                //---Create By
                chuDe.NguoiTao = Convert.ToInt32(Session["UserId"]);
                //Slug
                chuDe.TenRutGon = XString.Str_Slug(chuDe.TenChuDe);
                //Update at
                chuDe.NgayCapNhat = DateTime.Now;
                //Update by
                chuDe.NguoiCapNhat = Convert.ToInt32(Session["UserId"]);
                //Xu ly cho muc TenRutGon
                chuDe.TenRutGon = XString.Str_Slug(chuDe.TenChuDe);
                db.Entry(chuDe).State = EntityState.Modified;
                //Thong bao thanh cong
                TempData["message"] = new XMessage("success", "Sửa chủ đề thành công");
                //Cap nhat du lieu, sua them cho phan LienKet phuc vu cho ChuDe
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }
            return View(chuDe);
        }

        // GET: Admin/ChuDe/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy chủ đề");
                return RedirectToAction("Index");
            }
            ChuDe chuDe = db.ChuDes.Find(id);
            if (chuDe == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy chủ đề");
                return RedirectToAction("Index");
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
            //tim thay mau tin thi xoa, cap nhat cho Links
            db.ChuDes.Remove(chuDe);
            if (db.SaveChanges() == 1)
            {
                var baiviet = db.BaiViets.Where(bv=> bv.ChuDeBV == chuDe.Id).ToList();
                foreach(var bv in baiviet)
                {
                    db.BaiViets.Remove(bv);
                }
            }
            else
            {
                //Thong bao thanh cong
                TempData["message"] = new XMessage("success", "Xóa chủ đề thành công");
            }

            db.SaveChanges();
            return RedirectToAction("Trash");
        }

        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                //hien thi thong bao
                TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");
                return RedirectToAction("Index");
            }
            ChuDe chuDe = db.ChuDes.Find(id);
            if (chuDe == null)
            {
                TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");
                return RedirectToAction("Index");
            }
            //cap nhat trang thai
            chuDe.TrangThai = (chuDe.TrangThai == 1) ? 2 : 1;
            //cap nhat update at
            chuDe.NgayCapNhat = DateTime.Now;
            //cap nhat update by
            chuDe.NguoiCapNhat = Convert.ToInt32(Session["UserID"]);
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
                return RedirectToAction("Index", "ChuDe");
            }

            //khi nhap nut thay doi TrangThai cho mot mau tin
            ChuDe topics = db.ChuDes.Find(id);
            //kiem tra id cua categories co ton tai?
            if (topics == null)
            {
                //Thong bao that bai
                TempData["message"] = new XMessage("danger", "Phục hồi dữ liệu thất bại");

                //chuyen huong trang
                return RedirectToAction("Index", "ChuDe");
            }
            //thay doi trang thai TrangThai tu 1 thanh 2 va nguoc lai
            topics.TrangThai = 2;

            //cap nhat gia tri cho Nguoi/Ngay cap nhat
            topics.NguoiCapNhat = Convert.ToInt32(Session["UserId"].ToString());
            topics.NgayCapNhat = DateTime.Now;

            //Goi ham Update trong TopicDAO
            db.Entry(topics).State = EntityState.Modified;
            db.SaveChanges();
            //Thong bao thanh cong
            TempData["message"] = new XMessage("success", "Phục hồi dữ liệu thành công");

            //khi cap nhat xong thi chuyen ve Trash
            return RedirectToAction("Trash", "ChuDe");
        }
        public ActionResult DelTrash(int? id)
        {
            //khi nhap nut thay doi Status cho mot mau tin
            ChuDe topics = db.ChuDes.Find(id);

            //thay doi trang thai TrangThai tu 1,2 thanh 0
            topics.TrangThai = 0;

            //cap nhat gia tri cho Nguoi/Ngay cap nhat
            topics.NguoiCapNhat = Convert.ToInt32(Session["UserId"].ToString());
            topics.NgayCapNhat = DateTime.Now;

            //Goi ham Update
            db.Entry(topics).State = EntityState.Modified;
            db.SaveChanges();

            //Thong bao thanh cong
            TempData["message"] = new XMessage("success", "Xóa mẩu tin thành công");

            //khi cap nhat xong thi chuyen ve Index
            return RedirectToAction("Index", "ChuDe");
        }
        public ActionResult Trash(int? id)
        {
            var list = db.ChuDes.Where(m => m.TrangThai == 0).ToList();
            return View(list);
        }
    }
}
