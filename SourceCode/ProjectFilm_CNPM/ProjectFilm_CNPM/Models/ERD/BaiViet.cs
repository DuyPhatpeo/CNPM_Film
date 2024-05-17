using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectFilm_CNPM.Models.ERD
{
    [Table("BaiViet")]
    public class BaiViet
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Chủ đề bài viết không được để trống")]
        [Display(Name = "Chủ đề bài viết")]
        public int ChuDeBV { get; set; }
        [Required(ErrorMessage = "Tên bài viết không được để trống")]
        [Display(Name = "Tên bài viết")]
        public string TenBV { get; set; }
        [Display(Name = "Liên kết")]
        public string LienKet { get; set; }
        [Display(Name = "Chi tiết")]
        public string ChiTiet { get; set; }
        [Display(Name = "Ảnh bài viết")]
        public string Anh { get; set; }
        [Display(Name = "Kiểu bài viết")]
        public string KieuBV { get; set; }
    }
}