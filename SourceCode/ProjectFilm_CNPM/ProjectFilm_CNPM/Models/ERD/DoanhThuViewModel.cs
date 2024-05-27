using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFilm_CNPM.Models.ERD
{
    public class DoanhThuViewModel
    {
        public int MaPhim { get; set; }
        public string TenPhim { get; set; }
        public int TongTien { get; set; }
        public DateTime NgayChieu { get; set; }
        public int TongSoVe {  get; set; }
    }
}