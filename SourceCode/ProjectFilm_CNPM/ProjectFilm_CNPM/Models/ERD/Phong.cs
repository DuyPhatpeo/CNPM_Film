using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectFilm_CNPM.Models.ERD
{
    [Table("Phong")]
    public class Phong
    {
        public Phong() {
            this.Ghes = new HashSet<Ghe>();
            this.SuatChieus = new HashSet<SuatChieu>();
        }
        [Key]
        public int MaPhong { get; set; }

        [Required(ErrorMessage = "Tên phòng không được để trống")]
        [Display(Name = "Tên phòng")]
        public string TenPhong { get; set; }

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
        public ICollection<Ghe> Ghes { get; set; }
        public ICollection<SuatChieu> SuatChieus { get; set; }
    }
}