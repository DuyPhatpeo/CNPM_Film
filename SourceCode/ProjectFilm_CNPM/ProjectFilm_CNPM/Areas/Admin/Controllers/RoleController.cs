using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
    public class RoleController : Controller
    {
        // GET: Admin/Role
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var item = db.Roles.ToList();
            return View(item);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IdentityRole model)
        {
            bool check = db.Roles.Any(p => p.Name == model.Name);
            if (check)
            {
                ModelState.AddModelError("TenPhim", "Tên quyền đã tồn tại trong hệ thống.");
                return View(model);
            }
            if (ModelState.IsValid)
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                roleManager.Create(model);
                return RedirectToAction("Index");   
            }
            return View();
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy quyền");
                return RedirectToAction("Index");
            }
            var item = db.Roles.Find(id);
            if (item == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy quyền");
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IdentityRole model)
        {
            bool check = db.Roles.Any(p => p.Name == model.Name);
            if (check)
            {
                ModelState.AddModelError("TenPhim", "Tên quyền đã tồn tại trong hệ thống.");
                return View(model);
            }
            if (ModelState.IsValid)
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                roleManager.Update(model);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
//done