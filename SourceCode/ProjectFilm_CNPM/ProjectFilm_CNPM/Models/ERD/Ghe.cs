using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectFilm_CNPM.Models.ERD
{
    [Table("Ghe")]
    public class Ghe
    {
        public Ghe() {
            this.ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
        }
        [Key]
        public int MaGhe { get; set; }
        [Required(ErrorMessage = "Tên ghế không được để trống")]
        [StringLength(2, ErrorMessage = "Tên ghế không được vượt quá 2 ký tự")]
        [Display(Name = "Tên ghế")]
        public string TenGhe { get; set; }

        [ForeignKey("Phong")]
        [Required(ErrorMessage = "Mã phòng không được để trống")]
        [Display(Name = "Mã phòng")]
        public int MaPhong { get; set; }

        [Required(ErrorMessage = "Loại ghế không được để trống")]
        [StringLength(10, ErrorMessage = "Loại ghế không được vượt quá 10 ký tự")]
        [Display(Name = "Loại ghế")]
        public string LoaiGhe { get; set; }

        [Required(ErrorMessage = "Giá ghế không được để trống")]
        [Display(Name = "Giá ghế")]
        public int GiaGhe { get; set; }

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
        public virtual Phong Phong { get; set; }
        public ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}