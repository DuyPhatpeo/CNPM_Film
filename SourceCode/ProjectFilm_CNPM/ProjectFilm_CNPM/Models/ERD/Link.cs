using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectFilm_CNPM.Models.ERD
{
    public class Link
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "URL")]
        public string URL { get; set; }
        [Required(ErrorMessage = "ID bảng liên kết không được để trống")]
        [Display(Name = "Bảng liên kết")]
        public int BangLienKet { get; set; }
        [Display(Name = "Loại liên kết")]
        public string LoaiLienKet { get; set; }
    }
}