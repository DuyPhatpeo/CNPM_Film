using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectFilm_CNPM.Models.ERD
{
    [Table("Phim")]
    public class Phim
    {
        public Phim() 
        {
            this.TheLoais = new HashSet<TheLoai>();
            this.SuatChieus = new HashSet<SuatChieu>();
            this.ChuDes = new HashSet<ChuDe>();
        }
        [Key]
        public int MaPhim { get; set; }
        [Required(ErrorMessage = "Tên phim không được để trống")]
        [Display(Name = "Tên phim")]
        public string TenPhim { get; set; }
        [Display(Name = "Tên rút gọn")]
        public string TenRutGon { get; set; }
        [Required(ErrorMessage = "Thời lượng không được để trống")]
        [Display(Name = "Thời lượng(Phút)")]
        public int ThoiLuong { get; set; }
        [Display(Name = "Hình ảnh")]
        public string Anh { get; set; }
        [Required(ErrorMessage = "Diễn viên không được để trống")]
        [Display(Name = "Đạo diễn")]
        public string DaoDien { get; set; }
        [Required(ErrorMessage = "Diễn viên không được để trống")]
        [Display(Name = "Diễn viên")]
        public string DienVien { get; set; }
        [Required(ErrorMessage = "Quốc gia không được để trống")]
        [Display(Name = "Quốc gia")]
        
        public string QuocGia { get; set; }
        [Required(ErrorMessage = "Năm phát hành không được để trống")]
        [Display(Name = "Năm phát hành")]
        public int NamPhatHanh { get; set; }
        [Required(ErrorMessage = "Phân loại không được để trống")]
        [Display(Name = "Phân loại")]
        public int PhanLoai { get; set; }
        [Required(ErrorMessage = "Mô tả không được để trống")]
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }
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
        [Required(ErrorMessage = "Trạng thái không được để trống")]
        [Display(Name = "Trạng thái")]
        public int TrangThai { get; set; }
        public ICollection<TheLoai> TheLoais { get; set; }
        public ICollection<ChuDe> ChuDes { get; set; }
        public ICollection<SuatChieu> SuatChieus { get; set; }
    }
}