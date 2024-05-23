using ProjectFilm_CNPM.Models;
using ProjectFilm_CNPM.Models.ERD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace ProjectFilm_CNPM.Controllers
{
    public class SiteController : Controller
    {
        // GET: Home
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(string slug = null)
        {
            if (slug == null)
            {
                //chuyen ve trang chu
                return RedirectToAction("Home", "Site");
            }
            else
            {
                Link links = db.Links.Where(m => m.URL == slug).FirstOrDefault();
                if (links != null)
                {
                    string typelink = links.LoaiLienKet;
                    switch (typelink)
                    {
                        case "chu-de":
                            return this.PostTopic(slug);
                        case "bai-viet":
                            return this.PostPage(slug);
                        default:
                            return this.Error404();
                    }
                }
                else
                {
                    //slug khong co trong bang Links
                    //slug co trong bang product?
                    Phim phim = db.Phims.Where(m => m.TenRutGon == slug && m.TrangThai == 1).FirstOrDefault();
                    if (phim != null)
                    {
                        return this.Details(slug);
                    }
                    else
                    {
                        BaiViet posts = db.BaiViets.Where(m => m.LienKet == slug && m.TrangThai == 1).FirstOrDefault();
                        if (posts != null)
                        {
                            return this.PostDetail(posts);
                        }
                        else
                        {
                            return this.Error404();
                        }
                    }
                }
            }
        }
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult PostDetail(BaiViet posts)
        {
            return View("PostDetail");
        }
        public ActionResult PostPage(string slug)
        {
            BaiViet baiViet = db.BaiViets.Where(m => m.LienKet == slug && m.TrangThai == 1).FirstOrDefault();
            return View("PostPage");
        }
        public ActionResult PostTopic(string slug)
        {
            ChuDe topics = db.ChuDes.Where(m => m.TenRutGon == slug && m.TrangThai == 1).FirstOrDefault();
            if (topics != null)
            {
                return View("PostTopic", topics);
            }
            return Error404();
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
        public ActionResult Details(string slug)
        {
            if (slug == null)
            {
                return RedirectToAction("Error404", "Site");
            }
            Phim phim = db.Phims.Where(m=> m.TenRutGon == slug && m.TrangThai == 1).FirstOrDefault();
            List<SuatChieu> listSuatChieu = db.SuatChieus.Where(m => m.MaPhim == phim.MaPhim).ToList();
            if (phim == null)
            {
                return RedirectToAction("Error404", "Site");
            }
            ViewBag.phim = phim;
            return View(listSuatChieu);
        }
        
        //List của bài viết
        public PartialViewResult ListBaiViet()
        {
            var list = db.BaiViets.Where(m => m.TrangThai == 1).Take(4).ToList();
            return PartialView("ListTopic",list);
        }
    }
}