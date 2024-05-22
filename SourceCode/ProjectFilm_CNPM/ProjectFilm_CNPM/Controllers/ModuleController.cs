using ProjectFilm_CNPM.Models;
using ProjectFilm_CNPM.Models.ERD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectFilm_CNPM.Controllers
{
    public class ModuleController : Controller
    {
        // GET: Module
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult MainMenuSub()
        {
            var menus = db.Menus.Where(x => x.TrangThai == 1 && x.ViTri == "MainMenu").ToList();
            return PartialView("MainMenuSub", menus);

        }
        public PartialViewResult Slider()
        {

            List<Slider> list = db.Sliders.Where(m => m.ViTri == "Slider" && m.TrangThai == 1).ToList();
            return PartialView("Slider", list);

        }
        public PartialViewResult CardItem()
        {

            List<Phim> list = db.Phims.Where(m => m.TrangThai == 1).ToList();
            return PartialView("CardItem", list);
        }
        public ActionResult DetailsItem(int? id)
        {

            Phim phim = db.Phims.Find(id);
            return View(phim);
        }
    }
}