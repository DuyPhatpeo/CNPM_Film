using ProjectFilm_CNPM.Library;
using ProjectFilm_CNPM.Models;
using ProjectFilm_CNPM.Models.ERD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectFilm_CNPM.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HoaDonController : Controller
    {
        // GET: Admin/HoaDon
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        { 
            return View(db.HoaDons.ToList());
        }
        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy hóa đơn");
                return RedirectToAction("Index");
            }
            var hd = db.HoaDons.Find(id);
            if(hd == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy hóa đơn");
                return RedirectToAction("Index");
            }
            var cthd = db.ChiTietHoaDons.Where(m=>m.MaHD == hd.MaHD).ToList();
            return View(cthd);   
        }

    }
}
//done