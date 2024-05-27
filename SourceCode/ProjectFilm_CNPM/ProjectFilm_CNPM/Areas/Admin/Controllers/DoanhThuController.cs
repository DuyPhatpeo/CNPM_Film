using ProjectFilm_CNPM.Models;
using ProjectFilm_CNPM.Models.ERD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectFilm_CNPM.Areas.Admin.Controllers
{
    public class DoanhThuController : Controller
    {
        // GET: Admin/DoanhThu
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var totalRevenuePerMovie = db.ChiTietHoaDons
                .Where(cthd => cthd.HoaDon.TrangThai == 1) 
                .Join(db.SuatChieus, cthd => cthd.MaSuatChieu, sc => sc.MaSuatChieu, (cthd, sc) => new { cthd, sc }) 
                .GroupBy(x => x.sc.MaPhim)
                .Select(g => new DoanhThuViewModel
                {
                    MaPhim = g.Key,
                    TenPhim = g.FirstOrDefault().sc.Phim.TenPhim,
                    NgayChieu = g.FirstOrDefault().sc.GioChieu,
                    TongSoVe = g.Select(x => x.cthd).Count(),
                    TongTien = g.Sum(x => x.cthd.HoaDon.TongTien) 
                })
                .ToList();

            return View(totalRevenuePerMovie);
        }
    }
}