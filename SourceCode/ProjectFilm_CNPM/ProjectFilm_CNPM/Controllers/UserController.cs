using ProjectFilm_CNPM.Models;
using ProjectFilm_CNPM.Models.ERD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectFilm_CNPM.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult ProfileUser()
        {
            int maND = Convert.ToInt32(Session["NguoiDung"]);
            NguoiDung nguoiDung = db.NguoiDungs.Where(m => m.MaND == maND).FirstOrDefault();
            
            return View(nguoiDung);
        }
        public ActionResult DangNhap()
        {

            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(string email, string password)
        {
            NguoiDung nd = db.NguoiDungs.Where(m => m.Email == email && m.MatKhau == password).FirstOrDefault();
            if (nd != null)
            {
                Session["NguoiDung"] = nd.MaND;
                return RedirectToAction("Index", "Site");
            }
            else
            {
                ViewBag.Error = "<strong>Tài khoản hoặc mật khẩu không đúng</strong>";
                return View();
            }
        }
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(NguoiDung model)
        {
            if (ModelState.IsValid)
            {
                // Check if the email is not duplicated
                if (!db.NguoiDungs.Any(m => m.Email == model.Email))
                {
                    model.Role = "Users";
                    model.NgayTao = DateTime.Now;
                    model.NguoiTao = Convert.ToInt32(Session["UserId"]);
                    model.NgayCapNhat = DateTime.Now;
                    model.NguoiCapNhat = Convert.ToInt32(Session["UserId"]);
                    model.TrangThai = 1;
                    db.NguoiDungs.Add(model);
                    db.SaveChanges();
                    // Assuming 'DangNhap' is the name of the action method for login
                    return RedirectToAction("DangNhap");
                }
                else
                {
                    ModelState.AddModelError("Email", "Email đã tồn tại!!!.");
                }
            }
            return View(model);
        }

        public ActionResult DangXuat()
        {
            Session["NguoiDung"] = null;
            return RedirectToAction("Index", "Site");
        }
    }
}