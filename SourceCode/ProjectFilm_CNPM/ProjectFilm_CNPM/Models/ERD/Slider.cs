using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectFilm_CNPM.Models.ERD
{
    [Table("Slider")]
    public class Slider
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên Slider không được để trống")]
        [Display(Name = "Tên Slider")]
        public string TenSlider { get; set; }
        [Display(Name = "Liên kết")]
        public string URL { get; set; }
        [Display(Name = "Hình ảnh")]
        public string Anh { get; set; }
        [Display(Name = "Sắp xếp")]
        public int? SapXep { get; set; }
        [Required(ErrorMessage = "Vị trí không được để trống")]
        [Display(Name = "Vị trí")]
        public string ViTri { get; set; }
        [Required(ErrorMessage = "Mô tả không được để trống")]
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }
        [Required(ErrorMessage = "Từ khóa không được để trống")]
        [Display(Name = "Từ khóa")]
        public string TuKhoa { get; set; }
        [Required(ErrorMessage = "Tên chủ đề không được để trống")]
        [Display(Name = "Tên chủ đề")]
        public string TenChuDe { get; set; }
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
    }
}