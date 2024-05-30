using ProjectFilm_CNPM.Models;
using ProjectFilm_CNPM.Models.ERD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectFilm_CNPM.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DoanhThuController : Controller
    {
        // GET: Admin/DoanhThu
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult DoanhThuTheoPhim()
        {
            var doanhthutheophim = db.ChiTietHoaDons
                .Where(cthd => cthd.HoaDon.TrangThai == 1) 
                .Join(db.SuatChieus, cthd => cthd.MaSuatChieu, sc => sc.MaSuatChieu, (cthd, sc) => new { cthd, sc })
                .Join(db.HoaDons, temp => temp.cthd.MaHD, hd => hd.MaHD, (temp, hd) => new { temp.cthd, temp.sc, hd })

                .GroupBy(x => x.sc.MaPhim)
                .Select(g => new DoanhThuViewModel
                {
                    MaPhim = g.Key,
                    TenPhim = g.FirstOrDefault().sc.Phim.TenPhim,
                    NgayChieu = g.FirstOrDefault().sc.GioChieu,
                    TongSoVe = g.Select(x => x.cthd).Count(),
                    TongTien = g.Sum(x => x.cthd.Ghe.GiaGhe)
                })
                .ToList();

            return View(doanhthutheophim);
        }
        public ActionResult DoanhThuKhachHang()
        {
            // Lấy danh sách khách hàng từ cơ sở dữ liệu
            var danhSachKhachHang = db.NguoiDungs.ToList();

            // Tạo một danh sách để lưu thông tin về số tiền tích lũy của mỗi khách hàng
            var thongTinTichLuyKhachHang = new List<Tuple<NguoiDung, int>>();

            foreach (var khachHang in danhSachKhachHang)
            {
                int? tongTienTichLuy = db.HoaDons
                    .Where(hd => hd.MaND == khachHang.MaND)
                    .Sum(hd => (int?)hd.TongTien); // Sử dụng kiểu dữ liệu nullable int?

                // Kiểm tra nếu tổng tiền tích lũy là null, thì gán giá trị mặc định là 0
                int tongTien = tongTienTichLuy ?? 0;

                thongTinTichLuyKhachHang.Add(new Tuple<NguoiDung, int>(khachHang, tongTien));
            }
            return View(thongTinTichLuyKhachHang);
        }

    }
}