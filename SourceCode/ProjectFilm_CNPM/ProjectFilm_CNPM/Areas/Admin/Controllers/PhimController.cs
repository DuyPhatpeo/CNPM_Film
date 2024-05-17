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
    public class PhimController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Phim
        public ActionResult Index()
        {
            var phim = db.Phims.Where(p => p.TrangThai == 1 || p.TrangThai ==2).ToList();
            return View(phim);
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
                //xu ly hinh anh
                var img = Request.Files["img"];//lay thong tin file
                if (img.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jpeg", ".png", ".gif" };
                    //kiem tra tap tin co hay khong
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))//lay phan mo rong cua tap tin
                    {
                        string slug = phim.TenRutGon;
                        //ten file = Slug + phan mo rong cua tap tin
                        string imgName = slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        phim.Anh = imgName;
                        //upload hinh
                        string PathDir = "~/Public/img/phim/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        img.SaveAs(PathFile);
                    }
                }//ket thuc phan upload hinh anh
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
                TempData["message"] = new XMessage("danger", "Không tìm thấy phim");
                return RedirectToAction("Index");
            }
            Phim phim = db.Phims.Find(id);
            if (phim == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy phim");
                return RedirectToAction("Index");
            }
            return View(phim);
        }

        // POST: Admin/Phim/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Phim phim)
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
                // Kiểm tra và xóa file hình ảnh
                string imageName = phim.Anh;
                if (!string.IsNullOrEmpty(imageName))
                {
                    string slug = XString.Str_Slug(phim.TenPhim);
                    string fileExtension = Path.GetExtension(imageName); // Lấy phần mở rộng của tệp tin
                    string imagePath = Path.Combine(Server.MapPath("~/Public/img/phim/"), slug + fileExtension);

                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }
                phim.Anh = null;
                //xu ly hinh anh
                var img = Request.Files["img"];//lay thong tin file
                if (img.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jpeg", ".png", ".gif" };
                    //kiem tra tap tin co hay khong
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))//lay phan mo rong cua tap tin
                    {
                        string slug = phim.TenRutGon;
                        //ten file = Slug + phan mo rong cua tap tin
                        string imgName = slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        phim.Anh = imgName;
                        //upload hinh
                        string PathDir = "~/Public/img/phim/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        img.SaveAs(PathFile);
                    }
                }//ket thuc phan upload hinh anh
                 // Lưu thông tin phim vào cơ sở dữ liệu
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
                //hien thi thong bao
                TempData["message"] = new XMessage("danger", "Xóa mẩu tin thất bại");
                return RedirectToAction("Trash");
            }
            Phim phim = db.Phims.Find(id);
            if (phim == null)
            {
                //hien thi thong bao
                TempData["message"] = new XMessage("danger", "Xóa mẩu tin thất bại");
                return RedirectToAction("Trash");
            }
            return View(phim);
        }

        // POST: Admin/Phim/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Phim phim = db.Phims.Find(id);

            if (phim == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẩu tin để xóa");
                return RedirectToAction("Trash");
            }
            db.Phims.Remove(phim);
            db.SaveChanges();


            if (!string.IsNullOrEmpty(phim.Anh))
            {
                string PathDir = "~/Public/img/phim/";
                string DelPath = Path.Combine(Server.MapPath(PathDir), phim.Anh);
                System.IO.File.Delete(DelPath);
            }

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
        // POST: Admin/Product/DelTrash
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                //hien thi thong bao
                TempData["message"] = new XMessage("danger", "Xóa mẩu tin thất bại");
                return RedirectToAction("Index");
            }
            Phim phim = db.Phims.Find(id);
            if (phim == null)
            {
                TempData["message"] = new XMessage("danger", "Xóa mẩu tin thất bại");
                return RedirectToAction("Index");
            }
            //cap nhat trang thai
            phim.TrangThai = 0;
            //cap nhat NgayCapNhat
            phim.NgayCapNhat = DateTime.Now;
            //cap nhat Nguoi cap nhat
            phim.NguoiCapNhat = Convert.ToInt32(Session["UserID"]);
            //update db
            db.SaveChanges();
            //hien thi thong bao
            TempData["message"] = new XMessage("success", "Xóa mẩu tin thành công");
            //tro ve trang Index
            return RedirectToAction("Index");

        }
        public ActionResult Trash()
        {
            var trashPhim = db.Phims.Where(p => p.TrangThai == 0).ToList();
            return View(trashPhim);
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
            Phim phim = db.Phims.Find(id);
            if (phim == null)
            {
                TempData["message"] = new XMessage("danger", "Phục hồi mẩu tin thất bại");
                return RedirectToAction("Index");
            }
            //cap nhat trang thai = 2
            phim.TrangThai = 2;
            //cap nhat update at
            
            //cap nhat NgayCapNhat
            phim.NgayCapNhat = DateTime.Now;
            //cap nhat Nguoi cap nhat
            phim.NguoiCapNhat = Convert.ToInt32(Session["UserID"]);
            //update db
            db.SaveChanges();
            //hien thi thong bao
            TempData["message"] = new XMessage("success", "Phục hồi mẩu tin thành công");
            //tro ve trang Index
            return RedirectToAction("Trash");
        }
    }
}
