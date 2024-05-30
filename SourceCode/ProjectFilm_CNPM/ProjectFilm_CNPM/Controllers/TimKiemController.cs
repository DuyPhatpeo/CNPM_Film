using ProjectFilm_CNPM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectFilm_CNPM.Controllers
{
    public class TimKiemController : Controller
    {
        // GET: TimKiem
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(string searchString)
        {
            ViewBag.searchString = searchString;
            var phim = db.Phims.Where(p => p.TenPhim.ToLower().Contains(searchString.ToLower())).ToList();
            if (phim != null)
            {
                return View(phim);
            }
            return View();
        }
    }
}
//done