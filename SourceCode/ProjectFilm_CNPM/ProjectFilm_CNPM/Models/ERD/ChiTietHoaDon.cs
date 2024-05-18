using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectFilm_CNPM.Models.ERD
{
    [Table("ChiTietHoaDon")]
    public class ChiTietHoaDon
    {
        [Key]
        public int MaCTHD { get; set; }

        [Required(ErrorMessage = "Mã hóa đơn không được để trống")]
        [ForeignKey("HoaDon")]
        [Display(Name = "Mã hóa đơn")]
        public int MaHD { get; set; }

        [Required(ErrorMessage = "Mã ghế không được để trống")]
        [ForeignKey("Ghe")]
        [Display(Name = "Mã ghế")]
        public int MaGhe { get; set; }

        [Required(ErrorMessage = "Mã suất chiếu không được để trống")]
        [ForeignKey("SuatChieu")]
        [Display(Name = "Mã suất chiếu")]
        public int MaSuatChieu { get; set; }

        [Required(ErrorMessage = "Bắp nước không được để trống")]
        [Display(Name = "Bắp nước")]
        public bool BapNuoc { get; set; }

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

        // Navigation properties
        public virtual HoaDon HoaDon { get; set; }
        public virtual Ghe Ghe { get; set; }
        public virtual SuatChieu SuatChieu { get; set; }
    }
}