using ProjectFilm_CNPM.Models;
using ProjectFilm_CNPM.Models.ERD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace ProjectFilm_CNPM.Controllers
{
    public class SiteController : Controller
    {
        // GET: Home
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(string slug = null)
        {
            if (slug == null)
            {
                //chuyen ve trang chu
                return RedirectToAction("Home", "Site");
            }
            else
            {
                Link links = db.Links.Where(m => m.URL == slug).FirstOrDefault();
                if (links != null)
                {
                    string typelink = links.LoaiLienKet;
                    switch (typelink)
                    {
                        case "phim":
                            return this.Details(slug);
                        case "chu-de":
                            return this.PostTopic(slug);
                        default:
                            return this.Error404();
                    }
                }
                else
                {
                    //slug khong co trong bang Links
                    //slug co trong bang product?
                    Phim phim = db.Phims.Where(m => m.TenRutGon == slug && m.TrangThai == 1).FirstOrDefault();
                    if (phim != null)
                    {
                        return this.Details(slug);
                    }
                    else
                    {
                        BaiViet posts = db.BaiViets.Where(m => m.LienKet == slug && m.TrangThai == 1).FirstOrDefault();
                        if (posts != null)
                        {
                            return this.PostDetail(posts);
                        }
                        else
                        {
                            return this.Error404();
                        }
                    }
                }
            }
        }
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult PostDetail(BaiViet posts)
        {
            return View("PostDetail");
        }
        // trang bài viết
        public ActionResult PostPage(string slug)
        {
            BaiViet baiViet = db.BaiViets.Where(m => m.LienKet == slug && m.TrangThai == 1).FirstOrDefault();
            return View(baiViet);
        }
        public ActionResult PostTopic(string slug)
        {
            ChuDe topics = db.ChuDes.Where(m => m.TenRutGon == slug && m.TrangThai == 1).FirstOrDefault();
            if (topics != null)
            {
                return View("PostTopic", topics);
            }
            return Error404();
        }

        public ActionResult Error404()
        {
            return View();
        }
        public PartialViewResult CardItem()
        {

            List<Phim> list = db.Phims.Where(m => m.TrangThai == 1).ToList();
            return PartialView("CardItem", list);
        }
        public ActionResult Details(string slug)
        {
            if (slug == null)
            {
                return RedirectToAction("Error404", "Site");
            }

            Phim phim = db.Phims.FirstOrDefault(m => m.TenRutGon == slug && m.TrangThai == 1);

            if (phim == null)
            {
                return RedirectToAction("Error404", "Site");
            }

            // Lấy tất cả các thể loại của phim
            var listTheLoai = (from tl in db.TheLoais
                               join p in db.Phims on tl.MaPhim equals p.MaPhim
                               where p.MaPhim == phim.MaPhim
                               select tl).ToList();

            ViewBag.listTheLoai = listTheLoai;

            
            List<SuatChieu> listSuatChieu = db.SuatChieus.Where(m => m.MaPhim == phim.MaPhim).ToList();


            var distinctDates = listSuatChieu.Select(s => s.GioChieu.Date).Distinct().OrderBy(d => d).ToList();

            // Tạo một Dictionary để lưu trữ danh sách suất chiếu cho mỗi ngày
            Dictionary<DateTime, List<SuatChieu>> suatChieusByDate = new Dictionary<DateTime, List<SuatChieu>>();

            foreach (var date in distinctDates)
            {
                // Lọc ra danh sách suất chiếu cho ngày hiện tại và sắp xếp theo thời gian
                var suatChieusForDate = listSuatChieu.Where(s => s.GioChieu.Date == date).OrderBy(s => s.GioChieu).ToList();

                // Thêm vào Dictionary
                suatChieusByDate.Add(date, suatChieusForDate);
            }

            ViewBag.phim = phim;
            ViewBag.suatChieusByDate = suatChieusByDate;

            return View();
        }


        //list bài viết ở trang index
        public PartialViewResult ListBaiViet()
        {
            var list = db.BaiViets.Where(m => m.TrangThai == 1).Take(4).ToList();
            return PartialView("ListBaiViet", list);
        }
        public ActionResult GetTinhTrangGhe(int maPhong)
        {
           
            var availableSeats = db.Ghes.Where(g => g.MaPhong == maPhong && g.TinhTrangGhe == false).ToList();

            
            return Json(availableSeats, JsonRequestBehavior.AllowGet);
        }



    }
}