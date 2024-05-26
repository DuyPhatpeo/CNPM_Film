namespace ProjectFilm_CNPM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BaiViet",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChuDeBV = c.Int(nullable: false),
                        TenBV = c.String(nullable: false),
                        LienKet = c.String(),
                        ChiTiet = c.String(),
                        Anh = c.String(),
                        KieuBV = c.String(),
                        MoTa = c.String(nullable: false),
                        TuKhoa = c.String(nullable: false),
                        NguoiTao = c.Int(nullable: false),
                        NgayTao = c.DateTime(nullable: false),
                        NguoiCapNhat = c.Int(),
                        NgayCapNhat = c.DateTime(),
                        TrangThai = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChuDe", t => t.ChuDeBV, cascadeDelete: true)
                .Index(t => t.ChuDeBV);
            
            CreateTable(
                "dbo.ChuDe",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MaPhim = c.Int(),
                        TenRutGon = c.String(),
                        MoTa = c.String(nullable: false),
                        TuKhoa = c.String(nullable: false),
                        TenChuDe = c.String(nullable: false),
                        NguoiTao = c.Int(nullable: false),
                        NgayTao = c.DateTime(nullable: false),
                        NguoiCapNhat = c.Int(),
                        NgayCapNhat = c.DateTime(),
                        TrangThai = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Phim", t => t.MaPhim)
                .Index(t => t.MaPhim);
            
            CreateTable(
                "dbo.Phim",
                c => new
                    {
                        MaPhim = c.Int(nullable: false, identity: true),
                        TenPhim = c.String(nullable: false),
                        TenRutGon = c.String(),
                        ThoiLuong = c.Int(nullable: false),
                        Anh = c.String(),
                        DaoDien = c.String(nullable: false),
                        DienVien = c.String(nullable: false),
                        QuocGia = c.String(nullable: false),
                        NamPhatHanh = c.Int(nullable: false),
                        PhanLoai = c.Int(nullable: false),
                        MoTa = c.String(nullable: false),
                        NguoiTao = c.Int(nullable: false),
                        NgayTao = c.DateTime(nullable: false),
                        NguoiCapNhat = c.Int(),
                        NgayCapNhat = c.DateTime(),
                        TrangThai = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaPhim);
            
            CreateTable(
                "dbo.SuatChieu",
                c => new
                    {
                        MaSuatChieu = c.Int(nullable: false, identity: true),
                        MaPhim = c.Int(nullable: false),
                        MaPhong = c.Int(nullable: false),
                        GioChieu = c.DateTime(nullable: false),
                        NguoiTao = c.Int(nullable: false),
                        NgayTao = c.DateTime(nullable: false),
                        NguoiCapNhat = c.Int(),
                        NgayCapNhat = c.DateTime(),
                        TrangThai = c.Int(),
                    })
                .PrimaryKey(t => t.MaSuatChieu)
                .ForeignKey("dbo.Phim", t => t.MaPhim, cascadeDelete: true)
                .ForeignKey("dbo.Phong", t => t.MaPhong, cascadeDelete: true)
                .Index(t => t.MaPhim)
                .Index(t => t.MaPhong);
            
            CreateTable(
                "dbo.ChiTietHoaDon",
                c => new
                    {
                        MaCTHD = c.Int(nullable: false, identity: true),
                        MaHD = c.Int(nullable: false),
                        MaSuatChieu = c.Int(nullable: false),
                        BapNuoc = c.Boolean(nullable: false),
                        NguoiTao = c.Int(nullable: false),
                        NgayTao = c.DateTime(nullable: false),
                        NguoiCapNhat = c.Int(),
                        NgayCapNhat = c.DateTime(),
                        TrangThai = c.Int(),
                    })
                .PrimaryKey(t => t.MaCTHD)
                .ForeignKey("dbo.HoaDon", t => t.MaHD, cascadeDelete: true)
                .ForeignKey("dbo.SuatChieu", t => t.MaSuatChieu, cascadeDelete: true)
                .Index(t => t.MaHD)
                .Index(t => t.MaSuatChieu);
            
            CreateTable(
                "dbo.HoaDon",
                c => new
                    {
                        MaHD = c.Int(nullable: false, identity: true),
                        MaND = c.Int(nullable: false),
                        NgayLapHD = c.DateTime(nullable: false),
                        NguoiTao = c.Int(nullable: false),
                        NgayTao = c.DateTime(nullable: false),
                        NguoiCapNhat = c.Int(),
                        NgayCapNhat = c.DateTime(),
                        TrangThai = c.Int(),
                    })
                .PrimaryKey(t => t.MaHD)
                .ForeignKey("dbo.NguoiDung", t => t.MaND, cascadeDelete: true)
                .Index(t => t.MaND);
            
            CreateTable(
                "dbo.NguoiDung",
                c => new
                    {
                        MaND = c.Int(nullable: false, identity: true),
                        TenND = c.String(nullable: false),
                        NgaySinh = c.DateTime(nullable: false),
                        GioiTinh = c.Boolean(nullable: false),
                        SDT = c.String(nullable: false, maxLength: 10),
                        Anh = c.String(),
                        Email = c.String(nullable: false),
                        MatKhau = c.String(nullable: false, maxLength: 100),
                        XacNhanMatKhau = c.String(),
                        Role = c.String(),
                        NguoiTao = c.Int(nullable: false),
                        NgayTao = c.DateTime(nullable: false),
                        NguoiCapNhat = c.Int(),
                        NgayCapNhat = c.DateTime(),
                        TrangThai = c.Int(),
                    })
                .PrimaryKey(t => t.MaND);
            
            CreateTable(
                "dbo.Phong",
                c => new
                    {
                        MaPhong = c.Int(nullable: false, identity: true),
                        TenPhong = c.String(nullable: false),
                        NguoiTao = c.Int(nullable: false),
                        NgayTao = c.DateTime(nullable: false),
                        NguoiCapNhat = c.Int(),
                        NgayCapNhat = c.DateTime(),
                        TrangThai = c.Int(),
                    })
                .PrimaryKey(t => t.MaPhong);
            
            CreateTable(
                "dbo.Ghe",
                c => new
                    {
                        MaGhe = c.Int(nullable: false, identity: true),
                        TenGhe = c.String(nullable: false, maxLength: 2),
                        MaPhong = c.Int(nullable: false),
                        LoaiGhe = c.String(nullable: false, maxLength: 10),
                        GiaGhe = c.Int(nullable: false),
                        NguoiTao = c.Int(nullable: false),
                        NgayTao = c.DateTime(nullable: false),
                        NguoiCapNhat = c.Int(),
                        NgayCapNhat = c.DateTime(),
                        TrangThai = c.Int(),
                        TinhTrangGhe = c.Boolean(),
                    })
                .PrimaryKey(t => t.MaGhe)
                .ForeignKey("dbo.Phong", t => t.MaPhong, cascadeDelete: true)
                .Index(t => t.MaPhong);
            
            CreateTable(
                "dbo.TheLoai",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MaPhim = c.Int(nullable: false),
                        TenTheLoai = c.String(nullable: false),
                        NguoiTao = c.Int(nullable: false),
                        NgayTao = c.DateTime(nullable: false),
                        NguoiCapNhat = c.Int(),
                        NgayCapNhat = c.DateTime(),
                        TrangThai = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Phim", t => t.MaPhim, cascadeDelete: true)
                .Index(t => t.MaPhim);
            
            CreateTable(
                "dbo.Links",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        URL = c.String(),
                        BangLienKet = c.Int(nullable: false),
                        LoaiLienKet = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Menu",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenMenu = c.String(nullable: false),
                        TableId = c.Int(),
                        KieuMenu = c.String(),
                        ViTri = c.String(),
                        LienKet = c.String(),
                        Order = c.Int(),
                        NguoiTao = c.Int(nullable: false),
                        NgayTao = c.DateTime(nullable: false),
                        NguoiCapNhat = c.Int(),
                        NgayCapNhat = c.DateTime(),
                        TrangThai = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Slider",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenSlider = c.String(nullable: false),
                        URL = c.String(),
                        Anh = c.String(),
                        SapXep = c.Int(),
                        ViTri = c.String(nullable: false),
                        MoTa = c.String(nullable: false),
                        TuKhoa = c.String(nullable: false),
                        TenChuDe = c.String(nullable: false),
                        NguoiTao = c.Int(nullable: false),
                        NgayTao = c.DateTime(nullable: false),
                        NguoiCapNhat = c.Int(),
                        NgayCapNhat = c.DateTime(),
                        TrangThai = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ThamSo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenThamSo = c.String(),
                        DonViTinh = c.String(),
                        GiaTri = c.String(),
                        TrangThai = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Ten = c.String(),
                        SoDienThoai = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.BaiViet", "ChuDeBV", "dbo.ChuDe");
            DropForeignKey("dbo.ChuDe", "MaPhim", "dbo.Phim");
            DropForeignKey("dbo.TheLoai", "MaPhim", "dbo.Phim");
            DropForeignKey("dbo.SuatChieu", "MaPhong", "dbo.Phong");
            DropForeignKey("dbo.Ghe", "MaPhong", "dbo.Phong");
            DropForeignKey("dbo.SuatChieu", "MaPhim", "dbo.Phim");
            DropForeignKey("dbo.ChiTietHoaDon", "MaSuatChieu", "dbo.SuatChieu");
            DropForeignKey("dbo.ChiTietHoaDon", "MaHD", "dbo.HoaDon");
            DropForeignKey("dbo.HoaDon", "MaND", "dbo.NguoiDung");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.TheLoai", new[] { "MaPhim" });
            DropIndex("dbo.Ghe", new[] { "MaPhong" });
            DropIndex("dbo.HoaDon", new[] { "MaND" });
            DropIndex("dbo.ChiTietHoaDon", new[] { "MaSuatChieu" });
            DropIndex("dbo.ChiTietHoaDon", new[] { "MaHD" });
            DropIndex("dbo.SuatChieu", new[] { "MaPhong" });
            DropIndex("dbo.SuatChieu", new[] { "MaPhim" });
            DropIndex("dbo.ChuDe", new[] { "MaPhim" });
            DropIndex("dbo.BaiViet", new[] { "ChuDeBV" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.ThamSo");
            DropTable("dbo.Slider");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Menu");
            DropTable("dbo.Links");
            DropTable("dbo.TheLoai");
            DropTable("dbo.Ghe");
            DropTable("dbo.Phong");
            DropTable("dbo.NguoiDung");
            DropTable("dbo.HoaDon");
            DropTable("dbo.ChiTietHoaDon");
            DropTable("dbo.SuatChieu");
            DropTable("dbo.Phim");
            DropTable("dbo.ChuDe");
            DropTable("dbo.BaiViet");
        }
    }
}
