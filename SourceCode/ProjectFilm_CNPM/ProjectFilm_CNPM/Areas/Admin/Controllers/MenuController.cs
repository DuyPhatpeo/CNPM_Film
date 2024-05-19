using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectFilm_CNPM.Library;
using System.Web.Services.Description;
using ProjectFilm_CNPM.Models;
using ProjectFilm_CNPM.Models.ERD;

namespace ProjectFilm_CNPM.Areas.Admin.Controllers
{
    public class MenuController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Menu
        public ActionResult Index()
        {
            var topiclist = db.ChuDes.ToList();
            var phimlist = db.Phims.ToList();
            var baiViet = db.BaiViets.ToList();

            ViewBag.TopList = topiclist;
            ViewBag.PhimList = phimlist;
            ViewBag.PostList = baiViet;
            ViewBag.TopList = topiclist;
            ViewBag.PhimList = phimlist;
            ViewBag.PostList = baiViet;
            var menu = db.Menus.Where(m => m.TrangThai != 0).ToList(); //select * from Menus voi Status !=0
            return View("Index", menu);//truyen menu duoi dang model
        }
        // POST: Admin/Menu/Create
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            //-------------------------Category------------------------//
            //Xu ly cho nút ThemCategory ben Index
            if (!string.IsNullOrEmpty(form["ThemPhim"]))//nut ThemCategory duoc nhan
            {
                if (!string.IsNullOrEmpty(form["namePhim"]))//check box được nhấn
                {
                    var listitem = form["namePhim"];
                    //chuyen danh sach thanh dang mang: vi du 1,2,3,...
                    var listarr = listitem.Split(',');//cat theo dau ,
                    foreach (var row in listarr)//row = id cua các mau tin
                    {
                        int id = int.Parse(row);
                        //lay 1 ban ghi
                        Phim phim = db.Phims.Find(id);
                        
                        //tao ra menu
                        Menu menu = new Menu();
                        menu.TenMenu = phim.TenPhim;
                        menu.LienKet = phim.TenRutGon; 
                        menu.TableId = phim.MaPhim;
                        menu.KieuMenu = "phim";
                        menu.ViTri = form["ViTri"];
                        menu.Order = 0;
                        menu.NguoiTao = Convert.ToInt32(Session["UserId"].ToString());
                        menu.NgayTao = DateTime.Now;
                        menu.TrangThai= 2;//chưa xuất bản
                        db.Menus.Add(menu);
                        db.SaveChanges();
                    }
                    TempData["message"] = new XMessage("success", "Thêm menu danh mục thành công");
                }
                else
                {
                    TempData["message"] = new XMessage("danger", "Chưa chọn mục phim");
                }
            }

            //Xu ly cho nút ThemTopic ben Index
            if (!string.IsNullOrEmpty(form["ThemTopic"]))//nut ThemCategory duoc nhan
            {
                if (!string.IsNullOrEmpty(form["nameTopic"]))//check box được nhấn
                {
                    var listitem = form["nameTopic"];
                    //chuyen danh sach thanh dang mang: vi du 1,2,3,...
                    var listarr = listitem.Split(',');//cat theo dau ,
                    foreach (var row in listarr)//row = id cua các mau tin
                    {
                        int id = int.Parse(row);//ep kieu int
                                                //lay 1 ban ghi
                        ChuDe topics = db.ChuDes.Find(id);
                        //tao ra menu
                        Menu menu = new Menu();
                        menu.TenMenu = topics.TenChuDe;
                        menu.LienKet = topics.TenRutGon;
                        menu.TableId = topics.Id;
                        menu.KieuMenu = "topic";
                        menu.ViTri = form["ViTri"];
                       
                        menu.Order = 0;
                        menu.NguoiTao = Convert.ToInt32(Session["UserId"].ToString());
                        menu.NgayTao = DateTime.Now;
                        menu.TrangThai = 2;//chưa xuất bản
                        db.Menus.Add(menu);
                        db.SaveChanges();
                    }
                    TempData["message"] = new XMessage("success", "Thêm menu chủ đề bài viết thành công");
                }
                else
                {
                    TempData["message"] = new XMessage("danger", "Chưa chọn danh mục chủ đề bài viết");
                }
            }

            //-------------------------Page------------------------//
            //Xử lý cho nut Thempage ben Index
            if (!string.IsNullOrEmpty(form["ThemBaiViet"]))
            {
                if (!string.IsNullOrEmpty(form["nameBaiViet"]))//check box được nhấn tu phia Index
                {
                    var listitem = form["namePage"];
                    //chuyen danh sach thanh dang mang: vi du 1,2,3,...
                    var listarr = listitem.Split(',');//cat theo dau ,
                    foreach (var row in listarr)//row = id cua các mau tin
                    {
                        int id = int.Parse(row);//ep kieu int
                        BaiViet baiViet = db.BaiViets.Find(id);
                        //tao ra menu
                        Menu menu = new Menu();
                        menu.TenMenu = baiViet.TenBV; 
                        menu.LienKet = baiViet.LienKet;
                        menu.TableId = baiViet.Id;
                        menu.KieuMenu = "page";
                        menu.ViTri = form["ViTri"];
                        menu.Order = 0;
                        menu.NguoiTao = Convert.ToInt32(Session["UserId"].ToString());
                        menu.NgayTao = DateTime.Now;
                        menu.TrangThai = 2;//chưa xuất bản
                        db.Menus.Add(menu);
                        db.SaveChanges();
                    }
                    TempData["message"] = new XMessage("success", "Thêm menu bài viết thành công");
                }
                else//check box chưa được nhấn
                {
                    TempData["message"] = new XMessage("danger", "Chưa chọn danh mục trang đơn");
                }
            }
            
            //-------------------------Custom------------------------//
            //Xử lý cho nút ThemCustom ben Index
            if (!string.IsNullOrEmpty(form["ThemCustom"]))
            {
                if (!string.IsNullOrEmpty(form["name"]) && !string.IsNullOrEmpty(form["link"]))
                {
                    //tao ra menu
                    Menu menus = new Menu();
                    menus.TenMenu = form["name"];
                    menus.LienKet = form["link"];
                    menus.KieuMenu = "custom";
                    menus.ViTri = form["ViTri"];
                    menus.Order = 0;
                    menus.NguoiTao = Convert.ToInt32(Session["UserId"].ToString());
                    menus.NgayTao = DateTime.Now;
                    menus.TrangThai = 2;//chưa xuất bản
                    db.Menus.Add(menus);
                    db.SaveChanges();
                    TempData["message"] = new XMessage("success", "Thêm danh mục thành công");
                }

                else//check box chưa được nhấn
                {
                    TempData["message"] = new XMessage("danger", "Chưa đủ thông tin cho mục tùy chọn Menu");
                }
            }

            return RedirectToAction("Index", "Menu");
        }

        // GET: Admin/Menu/Status
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Cập nhật không thành công");
                return RedirectToAction("Index");
            }
            Menu menus = db.Menus.Find(id);
            if (menus == null)
            {
                TempData["message"] = new XMessage("danger", "Cập nhật không thành công");
                return RedirectToAction("Index");
            }
            menus.NgayTao = DateTime.Now;
            menus.NguoiTao = Convert.ToInt32(Session["UserId"].ToString());
            menus.TrangThai = (menus.TrangThai == 1) ? 2 : 1;
            db.Entry(menus).State = EntityState.Modified;
            db.SaveChanges(); ;
            TempData["message"] = new XMessage("success", "Cập nhật thành công");
            return RedirectToAction("Index");
        }

        // Admin/Menus/Detail: Hien thi mot mau tin
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy menu");
                return RedirectToAction("Index");
            }
            Menu menus = db.Menus.Find(id);
            if (menus == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy menu");
                return RedirectToAction("Index");
            }
            return View(menus);
            
        }

        /////////////////////////////////////////////////////////////////////////////////////
        // Admin/Menu/Edit: Thay doi mot mau tin
        public ActionResult Edit(int? id)
        {

            ViewBag.OrderList = new SelectList(db.Menus.Where(m=> m.TrangThai !=0));

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Menu menus = db.Menus.Find(id);

            if (menus == null)
            {
                return HttpNotFound();
            }
            return View("Edit", menus);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Menu menus)
        {
            if (ModelState.IsValid)
            {

                if (menus.Order == null)
                {
                    menus.Order = 1;
                }
                else
                {
                    menus.Order += 1;
                }

                //Xy ly cho muc UpdateAt
                menus.NgayTao = DateTime.Now;

                //Xy ly cho muc UpdateBy
                menus.NguoiTao = Convert.ToInt32(Session["UserId"]);

                //Thong bao thanh cong
                TempData["message"] = new XMessage("success", "Cập nhật thành công");

                //Cap nhat du lieu
                db.Entry(menus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrderList = new SelectList(db.Menus.Where(m => m.TrangThai != 0));
            return View(menus);
        }

        // GET: Admin/Menu/DelTrash/5
        public ActionResult DelTrash(int? id)
        {
            //khi nhap nut thay doi Status cho mot mau tin
            Menu menus = db.Menus.Find(id);

            //thay doi trang thai Status tu 1,2 thanh 0
            menus.TrangThai = 0;

            //cap nhat gia tri cho UpdateAt/By
            menus.NguoiCapNhat = Convert.ToInt32(Session["UserId"].ToString());
            menus.NgayTao = DateTime.Now;
            db.Entry(menus).State = EntityState.Modified;
            db.SaveChanges();
            //Thong bao thanh cong
            TempData["message"] = new XMessage("success", "Xóa Menu thành công");
            return RedirectToAction("Index", "Menu");
        }


        // GET: Admin/Menus/Trash/5
        public ActionResult Trash(int? id)
        {
            var menu = db.Menus.Where(m => m.TrangThai == 0).ToList(); //select * from Menus voi Status !=0
            return View(menu);
        }
        // GET: Admin/Menu/Undo/
        public ActionResult Undo(int? id)
        {
            if (id == null)
            {
                //Thong bao that bai
                TempData["message"] = new XMessage("danger", "Phục hồi menu thất bại");
                //chuyen huong trang
                return RedirectToAction("Index", "Page");
            }
            Menu menus = db.Menus.Find(id);
            if (menus == null)
            {
                //Thong bao that bai
                TempData["message"] = new XMessage("danger", "Phục hồi menu thất bại");
                return RedirectToAction("Index");
            }
            menus.TrangThai = 2;

            //cap nhat gia tri
            menus.NguoiCapNhat = Convert.ToInt32(Session["UserId"].ToString());
            menus.NgayTao = DateTime.Now;
            db.Entry(menus).State = EntityState.Modified;
            db.SaveChanges(); 
            //Thong bao thanh cong
            TempData["message"] = new XMessage("success", "Phục hồi menu thành công");
            return RedirectToAction("Trash");
        }
        // GET: Admin/Menu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menus = db.Menus.Find(id);
            if (menus == null)
            {
                return HttpNotFound();
            }
            return View(menus);
        }

        // POST: Admin/Menu/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Menu menus = db.Menus.Find(id);
            db.Menus.Remove(menus);
            db.SaveChanges();
            TempData["message"] = new XMessage("success", "Xóa menu thành công");
            return RedirectToAction("Trash");
        }
    }
}
