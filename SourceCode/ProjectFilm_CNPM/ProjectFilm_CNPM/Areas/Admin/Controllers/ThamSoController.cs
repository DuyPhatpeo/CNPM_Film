using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectFilm_CNPM.Library;
using ProjectFilm_CNPM.Models;
using ProjectFilm_CNPM.Models.ERD;

namespace ProjectFilm_CNPM.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ThamSoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/ThamSo
        public ActionResult Index()
        {
            var list = db.ThamSos.Where(m => m.TrangThai != 0).ToList();
            return View(list);
        }

        // GET: Admin/ThamSo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ThamSo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ThamSo thamSo)
        {
            bool check = db.ThamSos.Any(p => p.TenThamSo == thamSo.TenThamSo);
            if (check)
            {
                ModelState.AddModelError("TenThamSo", "Tên ghế đã tồn tại trong hệ thống.");
                return View(check);
            }
            if (ModelState.IsValid)
            {
                db.ThamSos.Add(thamSo);
                db.SaveChanges();
                //Thong bao thanh cong
                TempData["message"] = new XMessage("success", "Tạo mới tham số thành công");
                return RedirectToAction("Index");
            }

            return View(thamSo);
        }

        // GET: Admin/ThamSo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy tham số");
                return RedirectToAction("Index");
            }
            ThamSo thamSo = db.ThamSos.Find(id);
            if (thamSo == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy viết");
                return RedirectToAction("Index");
            }
            return View(thamSo);
        }

        // POST: Admin/ThamSo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ThamSo thamSo)
        {
            bool check = db.ThamSos.Any(p => p.TenThamSo == thamSo.TenThamSo);
            if (check)
            {
                ModelState.AddModelError("TenThamSo", "Tên ghế đã tồn tại trong hệ thống.");
                return View(check);
            }
            if (ModelState.IsValid)
            {
                db.Entry(thamSo).State = EntityState.Modified;
                db.SaveChanges();
                //Thong bao thanh cong
                TempData["message"] = new XMessage("success", "Cập nhật tham số thành công");
                return RedirectToAction("Index");
            }
            return View(thamSo);
        }

        // GET: Admin/ThamSo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy tham số");
                return RedirectToAction("Index");
            }
            ThamSo thamSo = db.ThamSos.Find(id);
            if (thamSo == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy viết");
                return RedirectToAction("Index");
            }
            return View(thamSo);
        }

        // POST: Admin/ThamSo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ThamSo thamSo = db.ThamSos.Find(id);
            db.ThamSos.Remove(thamSo);
            //Thong bao thanh cong
            TempData["message"] = new XMessage("success", "Xóa tham số thành công");
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                //hien thi thong bao
                TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");
                return RedirectToAction("Index");
            }
            ThamSo thamSo = db.ThamSos.Find(id);
            if (thamSo == null)
            {
                TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");
                return RedirectToAction("Index");
            }
            //cap nhat trang thai
            thamSo.TrangThai = (thamSo.TrangThai == 1) ? 2 : 1;
            //update db
            db.SaveChanges();
            //hien thi thong bao
            TempData["message"] = new XMessage("success", "Cập nhật trạng thái thành công");
            //tro ve trang Index
            return RedirectToAction("Index");
        }
        public ActionResult Undo(int? id)
        {
            if (id == null)
            {
                //Thong bao that bai
                TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");
                //chuyen huong trang
                return RedirectToAction("Index", "ThamSo");
            }

            //khi nhap nut thay doi TrangThai cho mot mau tin
            ThamSo thamSo = db.ThamSos.Find(id);
            //kiem tra id cua categories co ton tai?
            if (thamSo == null)
            {
                //Thong bao that bai
                TempData["message"] = new XMessage("danger", "Phục hồi dữ liệu thất bại");

                //chuyen huong trang
                return RedirectToAction("Index", "ThamSo");
            }
            //thay doi trang thai TrangThai tu 1 thanh 2 va nguoc lai
            thamSo.TrangThai = 2;



            //Goi ham Update trong TopicDAO
            db.Entry(thamSo).State = EntityState.Modified;
            db.SaveChanges();
            //Thong bao thanh cong
            TempData["message"] = new XMessage("success", "Phục hồi dữ liệu thành công");

            //khi cap nhat xong thi chuyen ve Trash
            return RedirectToAction("Trash", "ThamSo");
        }
        public ActionResult DelTrash(int? id)
        {
            //khi nhap nut thay doi Status cho mot mau tin
            ThamSo thamSo = db.ThamSos.Find(id);

            //thay doi trang thai TrangThai tu 1,2 thanh 0
            thamSo.TrangThai = 0;


            db.Entry(thamSo).State = EntityState.Modified;
            db.SaveChanges();

            //Thong bao thanh cong
            TempData["message"] = new XMessage("success", "Xóa mẩu tin thành công");

            //khi cap nhat xong thi chuyen ve Index
            return RedirectToAction("Index", "ThamSo");
        }
        public ActionResult Trash(int? id)
        {
            var list = db.ThamSos.Where(m => m.TrangThai == 0).ToList();
            return View(list);
        }

    }
}
//done