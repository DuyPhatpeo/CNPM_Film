using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectFilm_CNPM.Models.ERD
{
    [Table("NguoiDung")]
    public class NguoiDung
    {
        public NguoiDung()
        {
            this.HoaDons = new HashSet<HoaDon>();
        }
        [Key]
        public int MaND { get; set; }

        [Required(ErrorMessage = "Họ và tên không được để trống")]
        [Display(Name = "Họ và tên")]
        public string TenND { get; set; }
        [Required(ErrorMessage = "Ngày sinh không được để trống")]
        [Display(Name = "Ngày sinh")]
        public DateTime? NgaySinh { get; set; }

        [Display(Name = "Giới tính")]
        [Required(ErrorMessage = "Giới tính không được để trống")]
        public bool? GioiTinh { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [StringLength(10)]

        [Display(Name = "Số điện thoại")]
        public string SDT { get; set; }
        [Display(Name = "Ảnh đại diện")]
        public string Anh {  get; set; }


        [EmailAddress]
        [Required(ErrorMessage = "Email không được để trống")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [StringLength(100, ErrorMessage = "Độ dài của {0} phải ít nhất là {2} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string MatKhau { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("MatKhau", ErrorMessage = "Mật khẩu và xác nhận mật khẩu không khớp.")]
        public string XacNhanMatKhau { get; set; }

        [Display(Name = "Vai trò")]
        public string Role { get; set; }

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
        public ICollection<HoaDon> HoaDons { get; set; }
    }
}