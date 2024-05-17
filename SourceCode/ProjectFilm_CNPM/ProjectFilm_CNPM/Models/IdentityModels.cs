using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProjectFilm_CNPM.Models.ERD;

namespace ProjectFilm_CNPM.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Ten {  get; set; }
        public string SoDienThoai { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("StrConnect", throwIfV1Schema: false)
        {
            
        }
        public DbSet<Phim> Phims { get; set; }
        public DbSet<TheLoai> TheLoais { get; set; }
        public DbSet<BaiViet> BaiViets { get; set; }
        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public DbSet<ChuDe> ChuDes { get; set; }
        public DbSet<Ghe> Ghes { get; set; }
        public DbSet<SuatChieu> SuatChieus { get; set; }
        public DbSet<Phong> Phongs { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<ThamSo> ThamSos { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}