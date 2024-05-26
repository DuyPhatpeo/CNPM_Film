using Newtonsoft.Json.Linq;
using ProjectFilm_CNPM.Models;
using ProjectFilm_CNPM.Models.ERD;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectFilm_CNPM.Controllers
{
    public class SiteController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(string slug = null)
        {
            if (slug == null)
            {
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

            var listTheLoai = (from tl in db.TheLoais
                               join p in db.Phims on tl.MaPhim equals p.MaPhim
                               where p.MaPhim == phim.MaPhim
                               select tl).ToList();

            ViewBag.listTheLoai = listTheLoai;

            List<SuatChieu> listSuatChieu = db.SuatChieus.Where(m => m.MaPhim == phim.MaPhim).ToList();

            var distinctDates = listSuatChieu.Select(s => s.GioChieu.Date).Distinct().OrderBy(d => d).ToList();

            Dictionary<DateTime, List<SuatChieu>> suatChieusByDate = new Dictionary<DateTime, List<SuatChieu>>();

            foreach (var date in distinctDates)
            {
                var suatChieusForDate = listSuatChieu.Where(s => s.GioChieu.Date == date).OrderBy(s => s.GioChieu).ToList();
                suatChieusByDate.Add(date, suatChieusForDate);
            }

            ViewBag.phim = phim;
            ViewBag.suatChieusByDate = suatChieusByDate;

            return View();
        }

        public PartialViewResult ListBaiViet()
        {
            var list = db.BaiViets.Where(m => m.TrangThai == 1).Take(4).ToList();
            return PartialView("ListBaiViet", list);
        }

        public JsonResult GetTinhTrangGhe()
        {
            if (Session["NguoiDung"] == null)
            {
                return Json(new { redirectToLogin = true }, JsonRequestBehavior.AllowGet);
            }
            var seats = db.Ghes.Where(g=> g.TinhTrangGhe == false && g.TrangThai ==1)
                               .Select(g => new
                               {
                                   g.MaGhe,
                                   g.TinhTrangGhe,
                                   g.GiaGhe,
                                   g.LoaiGhe
                               })
                               .ToList();
            return Json(seats, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult TaoHoaDon()
        {
            if (Session["NguoiDung"] == null)
            {
                return RedirectToAction("Index");
            }
            string requestBody;
            using (var reader = new StreamReader(Request.InputStream))
            {
                requestBody = reader.ReadToEnd();
            }
            // Parse dữ liệu JSON
            var requestData = JObject.Parse(requestBody);
            int maSuatChieu = int.Parse(requestData["maSuatChieu"]?.ToString());
            var maNguoiDung = Session["NguoiDung"]?.ToString();
            var selectedSeats = requestData["selectedSeats"]?.Select(x => int.Parse(x.ToString())).ToList();
            HoaDon hoaDon = new HoaDon();
            hoaDon.NgayLapHD = DateTime.Now;
            hoaDon.NgayTao = DateTime.Now;
            hoaDon.NgayCapNhat = DateTime.Now;
            hoaDon.TrangThai = 1;
            hoaDon.NguoiTao = Convert.ToInt32(Session["NguoiDung"]);
            hoaDon.NguoiCapNhat = Convert.ToInt32(Session["NguoiDung"]);
            hoaDon.MaND = Convert.ToInt32(Session["NguoiDung"]);
            db.HoaDons.Add(hoaDon);
            db.SaveChanges();
            if (db.SaveChanges() != 0)
            {
                foreach (var seat in selectedSeats)
                {
                    ChiTietHoaDon chiTietHoaDon = new ChiTietHoaDon
                    {
                        MaHD = hoaDon.MaHD,
                        MaSuatChieu = maSuatChieu,
                        MaGhe = seat,
                        BapNuoc = false,
                        NgayTao = DateTime.Now,
                        NgayCapNhat = DateTime.Now,
                        TrangThai = 1,
                        NguoiTao = Convert.ToInt32(Session["NguoiDung"]),
                        NguoiCapNhat = Convert.ToInt32(Session["NguoiDung"])
                    };
                    db.ChiTietHoaDons.Add(chiTietHoaDon);
                }
                db.SaveChanges();
            }

            return View();
        }

    }
}
