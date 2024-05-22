using ProjectFilm_CNPM.Models;
using ProjectFilm_CNPM.Models.ERD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectFilm_CNPM.Controllers
{
    public class SiteController : Controller
    {
        // GET: Home
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Error404()
        {
            return View();
        }
        public PartialViewResult CardItem()
        {

            List<Phim> list = db.Phims.Where(m => m.TrangThai == 1).ToList();
            return PartialView("CardItem", list);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error404", "Site");
            }
            Phim phim = db.Phims.Find(id);
            if (phim == null)
            {
                return RedirectToAction("Error404", "Site");
            }
            return View(phim);
        }
    }
}