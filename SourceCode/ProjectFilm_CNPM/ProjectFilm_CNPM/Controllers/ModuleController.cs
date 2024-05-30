using ProjectFilm_CNPM.Models;
using ProjectFilm_CNPM.Models.ERD;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.IO;
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

        //Menu cấp 1
        public PartialViewResult MainMenuSub()
        {
            //Lấy danh sách các phim trong menu ra
            ViewBag.phim = (from phim in db.Phims
                             join menu in db.Menus on phim.MaPhim equals menu.TableId
                             where menu.TrangThai == 1 && menu.KieuMenu == "phim"
                             select phim).ToList();
            //Lấy danh sách các chủ đề trong menu ra
            ViewBag.topic = (from chude in db.ChuDes
                            join menu in db.Menus on chude.Id equals menu.TableId
                            where menu.TrangThai == 1 && menu.KieuMenu == "topic"
                            select chude).ToList();
            //Lấy danh sách các bài viết trong menu ra
            ViewBag.page = (from baiviet in db.BaiViets
                            join menu in db.Menus on baiviet.Id equals menu.TableId
                            where menu.TrangThai == 1 && menu.KieuMenu == "page"
                            select baiviet).ToList();   
            return PartialView("MainMenuSub");

        }
        public PartialViewResult Slider()
        {

            List<Slider> list = db.Sliders.Where(m => m.ViTri == "Slider" && m.TrangThai == 1).ToList();
            return PartialView("Slider", list);

        }
     
        
    }
}
//done