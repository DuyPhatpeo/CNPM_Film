using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.EnterpriseServices;
using Antlr.Runtime.Tree;

namespace ProjectFilm_CNPM.Models.ERD
{
    [Table("HoaDon")]
    public class HoaDon
    {
        public HoaDon()
        {
            this.ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
        }
        [Key]
        public int MaHD { get; set; }

        [Required(ErrorMessage = "Mã người dùng không được để trống")]
        [ForeignKey("NguoiDung")]
        [Display(Name = "Mã người dùng")]
        public int MaND { get; set; }

        [Required(ErrorMessage = "Ngày lập hóa đơn không được để trống")]
        [Display(Name = "Ngày lập hóa đơn")]
        public DateTime NgayLapHD { get; set; }

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
        public int TongTien {  get; set; }
        public virtual NguoiDung NguoiDung { get; set; }
        public ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}