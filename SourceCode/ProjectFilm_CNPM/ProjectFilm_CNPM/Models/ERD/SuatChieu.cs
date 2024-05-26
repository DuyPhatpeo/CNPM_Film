using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectFilm_CNPM.Models.ERD
{
    [Table("SuatChieu")]
    public class SuatChieu
    {
        public SuatChieu()
        {
            this.ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
        }
        [Key]
        public int MaSuatChieu { get; set; }

        [Required(ErrorMessage = "Mã phim không được để trống")]
        [ForeignKey("Phim")]
        [Display(Name = "Mã phim")]
        public int MaPhim { get; set; }

        [Required(ErrorMessage = "Giờ chiếu không được để trống")]
        [Display(Name = "Giờ chiếu")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime GioChieu { get; set; }

        [Required(ErrorMessage = "Người tạo không được để trống")]
        [Display(Name = "Người tạo")]
        public int NguoiTao { get; set; }

        [Required(ErrorMessage = "Ngày tạo không được để trống")]
        [Display(Name = "Ngày tạo")]
        public DateTime NgayTao { get; set; }

        [Display(Name = "Người cập nhật")]
        public int? NguoiCapNhat { get; set; }

        [Display(Name = "Ngày cập nhật")]
        public DateTime? NgayCapNhat { get; set; }
        [Display(Name = "Trạng thái")]
        public int? TrangThai { get; set; }


        // Navigation property
        public virtual Phim Phim { get; set; }
        public ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}