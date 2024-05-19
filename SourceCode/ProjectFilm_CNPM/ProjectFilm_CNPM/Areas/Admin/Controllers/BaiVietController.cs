using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using ProjectFilm_CNPM.Library;
using ProjectFilm_CNPM.Models;
using ProjectFilm_CNPM.Models.ERD;

namespace ProjectFilm_CNPM.Areas.Admin.Controllers
{
    public class BaiVietController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/BaiViet
        public ActionResult Index()
        {
            var list = db.BaiViets.Where(m => m.TrangThai != 0).ToList();
            return View(list);
        }

        // GET: Admin/BaiViet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy bài viết");
                return RedirectToAction("Index");
            }
            BaiViet baiViet = db.BaiViets.Find(id);
            if (baiViet == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy  bài viết");
                return RedirectToAction("Index");
            }
            return View(baiViet);
        }

        // GET: Admin/BaiViet/Create
        public ActionResult Create()
        {
            // Chuyển ds phim thành SelectListItem
            var topiclist = db.ChuDes.ToList().Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.TenChuDe
            });

            ViewBag.TopicList = topiclist;

            return View();
        }

        // POST: Admin/BaiViet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BaiViet baiViet)
        {
            // Chuyển ds phim thành SelectListItem
            var topiclist = db.ChuDes.ToList().Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.TenChuDe
            });
            ViewBag.TopicList = topiclist;
            if (ModelState.IsValid)
            {
                baiViet.LienKet = XString.Str_Slug(baiViet.TenBV);
                //Xử lý tự động cho các trường sau:
                //---Create At
                baiViet.NgayTao = DateTime.Now;
                //---Create By
                baiViet.NguoiTao = Convert.ToInt32(Session["UserId"]);
                //Update at
                baiViet.NgayCapNhat = DateTime.Now;
                //Update by
                baiViet.NguoiCapNhat = Convert.ToInt32(Session["UserId"]);

                //xu ly hinh anh
                var img = Request.Files["img"];//lay thong tin file
                if (img.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jpeg", ".png", ".gif" };
                    //kiem tra tap tin co hay khong
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))//lay phan mo rong cua tap tin
                    {
                        string slug = baiViet.LienKet;
                        //ten file = Slug + phan mo rong cua tap tin
                        string imgName = slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        baiViet.Anh = imgName;
                        //upload hinh
                        string PathDir = "~/Public/img/baiviet/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        img.SaveAs(PathFile);
                    }
                }//ket thuc phan upload hinh anh
                db.BaiViets.Add(baiViet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(baiViet);
        }

        // GET: Admin/BaiViet/Edit/5
        public ActionResult Edit(int? id)
        {
            // Chuyển ds phim thành SelectListItem
            var topiclist = db.ChuDes.ToList().Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.TenChuDe
            });

            ViewBag.TopicList = topiclist;
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy bài viết");
                return RedirectToAction("Index");
            }
            BaiViet baiViet = db.BaiViets.Find(id);
            if (baiViet == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy bài viết");
                return RedirectToAction("Index");
            }
            return View(baiViet);
        }

        // POST: Admin/BaiViet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BaiViet baiViet)
        {
            // Chuyển ds phim thành SelectListItem
            var topiclist = db.ChuDes.ToList().Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.TenChuDe
            });

            ViewBag.TopicList = topiclist;
            baiViet.LienKet = XString.Str_Slug(baiViet.TenBV);
            if (ModelState.IsValid)
            {
                //Xử lý tự động cho các trường sau:
                //---Create At
                baiViet.NgayTao = DateTime.Now;
                //---Create By
                baiViet.NguoiTao = Convert.ToInt32(Session["UserId"]);
                //Update at
                baiViet.NgayCapNhat = DateTime.Now;
                //Update by
                baiViet.NguoiCapNhat = Convert.ToInt32(Session["UserId"]);
                string imageName = baiViet.Anh;
                if (!string.IsNullOrEmpty(imageName))
                {
                    string slug = XString.Str_Slug(baiViet.TenBV);
                    string fileExtension = Path.GetExtension(imageName); // Lấy phần mở rộng của tệp tin
                    string imagePath = Path.Combine(Server.MapPath("~/Public/img/baiviet/"), slug + fileExtension);

                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }
                baiViet.Anh = null;
                var img = Request.Files["img"];//lay thong tin file
                if (img.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jpeg", ".png", ".gif" };
                    //kiem tra tap tin co hay khong
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))//lay phan mo rong cua tap tin
                    {
                        string slug = baiViet.LienKet;
                        //ten file = Slug + phan mo rong cua tap tin
                        string imgName = slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        baiViet.Anh = imgName;
                        //upload hinh
                        string PathDir = "~/Public/img/baiviet/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        img.SaveAs(PathFile);
                    }
                }//ket thuc phan upload hinh anh
                db.Entry(baiViet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(baiViet);
        }

        // GET: Admin/BaiViet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy bài viết");
                return RedirectToAction("Index");
            }
            BaiViet baiViet = db.BaiViets.Find(id);
            if (baiViet == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy bài viết");
                return RedirectToAction("Index");
            }
            return View(baiViet);
        }

        // POST: Admin/BaiViet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BaiViet baiViet = db.BaiViets.Find(id);

            if (baiViet == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẩu tin để xóa");
                return RedirectToAction("Trash");
            }
            db.BaiViets.Remove(baiViet);
            db.SaveChanges();


            if (!string.IsNullOrEmpty(baiViet.Anh))
            {
                string PathDir = "~/Public/img/baiviet/";
                string DelPath = Path.Combine(Server.MapPath(PathDir), baiViet.Anh);
                System.IO.File.Delete(DelPath);
            }

            // Thông báo thành công
            TempData["message"] = new XMessage("success", "Xóa danh mục thành công");

            // Trở lại trang thùng rác
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
            BaiViet baiViet = db.BaiViets.Find(id);
            if (baiViet == null)
            {
                TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");
                return RedirectToAction("Index");
            }
            //cap nhat trang thai
            baiViet.TrangThai = (baiViet.TrangThai == 1) ? 2 : 1;
            //cap nhat update at
            baiViet.NgayCapNhat = DateTime.Now;
            //cap nhat update by
            baiViet.NguoiCapNhat = Convert.ToInt32(Session["UserID"]);
            //update db
            db.SaveChanges();
            //hien thi thong bao
            TempData["message"] = new XMessage("success", "Cập nhật trạng thái thành công");
            //tro ve trang Index
            return RedirectToAction("Index");
        }
        // POST: Admin/Phim/DelTrash
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                //hien thi thong bao
                TempData["message"] = new XMessage("danger", "Xóa mẩu tin thất bại");
                return RedirectToAction("Index");
            }
            BaiViet baiViet = db.BaiViets.Find(id);
            if (baiViet == null)
            {
                TempData["message"] = new XMessage("danger", "Xóa mẩu tin thất bại");
                return RedirectToAction("Index");
            }
            //cap nhat trang thai
            baiViet.TrangThai = 0;
            //cap nhat NgayCapNhat
            baiViet.NgayCapNhat = DateTime.Now;
            //cap nhat Nguoi cap nhat
            baiViet.NguoiCapNhat = Convert.ToInt32(Session["UserID"]);
            //update db
            db.SaveChanges();
            //hien thi thong bao
            TempData["message"] = new XMessage("success", "Xóa mẩu tin thành công");
            //tro ve trang Index
            return RedirectToAction("Index");

        }
        public ActionResult Trash()
        {
            var trash = db.BaiViets.Where(p => p.TrangThai == 0).ToList();
            return View(trash);
        }
        // POST: Admin/Phim/Undo
        public ActionResult Undo(int? id)
        {
            if (id == null)
            {
                //hien thi thong bao
                TempData["message"] = new XMessage("danger", "Phục hồi mẩu tin thất bại");
                return RedirectToAction("Index");
            }
            BaiViet baiViet = db.BaiViets.Find(id);
            if (baiViet == null)
            {
                TempData["message"] = new XMessage("danger", "Phục hồi mẩu tin thất bại");
                return RedirectToAction("Index");
            }
            //cap nhat trang thai = 2
            baiViet.TrangThai = 2;
            //cap nhat update at

            //cap nhat NgayCapNhat
            baiViet.NgayCapNhat = DateTime.Now;
            //cap nhat Nguoi cap nhat
            baiViet.NguoiCapNhat = Convert.ToInt32(Session["UserID"]);
            //update db
            db.SaveChanges();
            //hien thi thong bao
            TempData["message"] = new XMessage("success", "Phục hồi mẩu tin thành công");
            //tro ve trang Index
            return RedirectToAction("Trash");
        }
    }
}