using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectFilm_CNPM.Models.ERD
{
    [Table("Menu")]
    public class Menu
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên menu không để trống")]
        [Display(Name = "Tên Menu")]
        public string TenMenu { get; set; }
        [Display(Name = "Bảng dữ liệu")]
        public int? TableId { get; set; }
        [Display(Name = "Kiểu Menu")]
        public string KieuMenu { get; set; }
        [Display(Name = "Vị trí")]
        public string ViTri { get; set; }
        [Display(Name = "Liên kết")]
        public string LienKet { get; set; }
        [Display(Name = "Sắp xếp")]
        public int? Order { get; set; }
        [Display(Name = "Người tạo")]
        [Required(ErrorMessage = "Người tạo không để trống")]
        public int NguoiTao { get; set; }
        [Display(Name = "Ngày tạo")]
        [Required(ErrorMessage = "Ngày tạo không để trống")]
        public DateTime NgayTao { get; set; }
        [Display(Name = "Người cập nhật")]
        public int? NguoiCapNhat { get; set; }
        [Display(Name = "Ngày cập nhật")]
        public DateTime? NgayCapNhat { get; set; }
        [Display(Name = "Trạng thái")]
        [Required(ErrorMessage = "Trạng thái không để trống")]
        public int TrangThai { get; set; }
    }
}