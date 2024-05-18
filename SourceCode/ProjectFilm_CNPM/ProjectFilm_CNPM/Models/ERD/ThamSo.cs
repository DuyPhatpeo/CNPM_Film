using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectFilm_CNPM.Models.ERD
{
    [Table("ThamSo")]
    public class ThamSo
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Tên tham số ")]
        public string TenThamSo {  get; set; }
        [Display(Name = "Đơn vị tính")]
        public string DonViTinh { get; set; }
        [Display(Name = "Giá trị")]
        public string GiaTri {  get; set; }
        [Display(Name = "Trạng thái")]
        public int TrangThai { get; set; }
    }
}