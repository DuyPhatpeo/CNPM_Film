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
    public class SliderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Slider
        public ActionResult Index()
        {
            var slider = db.Sliders.Where(p => p.TrangThai == 1 || p.TrangThai == 2).ToList();
            return View(slider);
        }

        // GET: Admin/Slider/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy slider");
                return RedirectToAction("Index");
            }
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy slider");
                return RedirectToAction("Index");
            }
            return View(slider);
        }

        // GET: Admin/Slider/Create
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

        // POST: Admin/Slider/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Slider slider)
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
                if (slider.SapXep == null)
                {
                    slider.SapXep = 1;
                }
                else
                {
                    slider.SapXep = slider.SapXep + 1;
                }
                //Xử lý tự động cho các trường sau:
                //---Create At
                slider.NgayTao = DateTime.Now;
                //---Create By
                slider.NguoiTao = Convert.ToInt32(Session["UserId"]);
                //Slug
                slider.URL = XString.Str_Slug(slider.TenSlider);
                //Update at
                slider.NgayCapNhat = DateTime.Now;
                //Update by
                slider.NguoiCapNhat = Convert.ToInt32(Session["UserId"]);
                //xu ly hinh anh
                var img = Request.Files["img"];//lay thong tin file
                if (img.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jpeg", ".png", ".gif" };
                    //kiem tra tap tin co hay khong
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))//lay phan mo rong cua tap tin
                    {
                        string slug = slider.URL;
                        //ten file = Slug + phan mo rong cua tap tin
                        string imgName = slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        slider.Anh = imgName;
                        //upload hinh
                        string PathDir = "~/Public/img/slider/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        img.SaveAs(PathFile);
                    }
                }//ket thuc phan upload hinh anh
                //them moi mau tin vao db
                db.Sliders.Add(slider);
                db.SaveChanges();
                //hiển thị thông báo thành công
                TempData["message"] = new XMessage("success", "Tạo mới Slider thành công!");
                return RedirectToAction("Index");
            }

            return View(slider);
        }

        // GET: Admin/Slider/Edit/5
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
                TempData["message"] = new XMessage("danger", "Không tìm thấy slider");
                return RedirectToAction("Index");
            }
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy slider");
                return RedirectToAction("Index");
            }
            return View(slider);
        }

        // POST: Admin/Slider/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Slider slider)
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
                db.Sliders.Attach(slider);
                //Xử lý tự động cho các trường sau:

                //---Create At
                slider.NgayTao = DateTime.Now;
                //---Create By
                slider.NguoiTao = Convert.ToInt32(Session["UserId"]);
                //Slug
                slider.URL = XString.Str_Slug(slider.TenSlider);
                //Update at
                slider.NgayCapNhat = DateTime.Now;
                //Update by
                slider.NguoiCapNhat = Convert.ToInt32(Session["UserId"]);
                // Kiểm tra và xóa file hình ảnh
                string imageName = slider.Anh;
                if (!string.IsNullOrEmpty(imageName))
                {
                    string slug = XString.Str_Slug(slider.URL);
                    string fileExtension = Path.GetExtension(imageName); // Lấy phần mở rộng của tệp tin
                    string imagePath = Path.Combine(Server.MapPath("~/Public/img/slider/"), slug + fileExtension);

                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }
                slider.Anh = null;
                //xu ly hinh anh
                var img = Request.Files["img"];//lay thong tin file
                if (img.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jpeg", ".png", ".gif" };
                    //kiem tra tap tin co hay khong
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))//lay phan mo rong cua tap tin
                    {
                        string slug = slider.URL;
                        //ten file = Slug + phan mo rong cua tap tin
                        string imgName = slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        slider.Anh = imgName;
                        //upload hinh
                        string PathDir = "~/Public/img/slider/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        img.SaveAs(PathFile);
                    }
                }//ket thuc phan upload hinh anh
                 // Lưu thông tin slider vào cơ sở dữ liệu
                db.Entry(slider).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slider);
        }

        // GET: Admin/Slider/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy slider");
                return RedirectToAction("Index");
            }
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy slider");
                return RedirectToAction("Index");
            }
            return View(slider);
        }

        // POST: Admin/Slider/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slider slider = db.Sliders.Find(id);

            if (slider == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẩu tin để xóa");
                return RedirectToAction("Trash");
            }
            db.Sliders.Remove(slider);
            db.SaveChanges();


            if (!string.IsNullOrEmpty(slider.Anh))
            {
                string PathDir = "~/Public/img/slider/";
                string DelPath = Path.Combine(Server.MapPath(PathDir), slider.Anh);
                System.IO.File.Delete(DelPath);
            }

            // Thông báo thành công
            TempData["message"] = new XMessage("success", "Xóa danh mục thành công");

            // Trở lại trang thùng rác
            return RedirectToAction("Trash");
        }


        // POST: Admin/Slider/Status
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                //hien thi thong bao
                TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");
                return RedirectToAction("Index");
            }
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");
                return RedirectToAction("Index");
            }
            //cap nhat trang thai
            slider.TrangThai = (slider.TrangThai == 1) ? 2 : 1;
            //cap nhat update at
            slider.NgayCapNhat = DateTime.Now;
            //cap nhat update by
            slider.NguoiCapNhat = Convert.ToInt32(Session["UserID"]);
            //update db
            db.SaveChanges();
            //hien thi thong bao
            TempData["message"] = new XMessage("success", "Cập nhật trạng thái thành công");
            //tro ve trang Index
            return RedirectToAction("Index");
        }
        // POST: Admin/Slider/DelTrash
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                //hien thi thong bao
                TempData["message"] = new XMessage("danger", "Xóa mẩu tin thất bại");
                return RedirectToAction("Index");
            }
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                TempData["message"] = new XMessage("danger", "Xóa mẩu tin thất bại");
                return RedirectToAction("Index");
            }
            //cap nhat trang thai
            slider.TrangThai = 0;
            //cap nhat NgayCapNhat
            slider.NgayCapNhat = DateTime.Now;
            //cap nhat Nguoi cap nhat
            slider.NguoiCapNhat = Convert.ToInt32(Session["UserID"]);
            //update db
            db.SaveChanges();
            //hien thi thong bao
            TempData["message"] = new XMessage("success", "Xóa mẩu tin thành công");
            //tro ve trang Index
            return RedirectToAction("Index");

        }
        public ActionResult Trash()
        {
            var trashSlider = db.Sliders.Where(p => p.TrangThai == 0).ToList();
            return View(trashSlider);
        }
        // POST: Admin/Slider/Undo
        public ActionResult Undo(int? id)
        {
            if (id == null)
            {
                //hien thi thong bao
                TempData["message"] = new XMessage("danger", "Phục hồi mẩu tin thất bại");
                return RedirectToAction("Index");
            }
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                TempData["message"] = new XMessage("danger", "Phục hồi mẩu tin thất bại");
                return RedirectToAction("Index");
            }
            //cap nhat trang thai = 2
            slider.TrangThai = 2;
            //cap nhat update at

            //cap nhat NgayCapNhat
            slider.NgayCapNhat = DateTime.Now;
            //cap nhat Nguoi cap nhat
            slider.NguoiCapNhat = Convert.ToInt32(Session["UserID"]);
            //update db
            db.SaveChanges();
            //hien thi thong bao
            TempData["message"] = new XMessage("success", "Phục hồi mẩu tin thành công");
            //tro ve trang Index
            return RedirectToAction("Trash");
        }
    }
}