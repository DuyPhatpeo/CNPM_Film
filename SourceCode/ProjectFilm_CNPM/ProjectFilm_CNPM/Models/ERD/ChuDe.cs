﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectFilm_CNPM.Models.ERD
{
    [Table("ChuDe")]
    public class ChuDe
    {
        public ChuDe() {
            this.BaiViets = new HashSet<BaiViet>();
        }
        [Key]
        public int Id { get; set; }

        [ForeignKey("Phim")]
        [Display(Name = "Mã phim")]
        public int? MaPhim { get; set; }
        [Display(Name = "Liên kết")]
        public string TenRutGon { get; set; }
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
        public virtual Phim Phim { get; set; }
        public ICollection<BaiViet> BaiViets { get; set; }

    }
}