namespace ProjectFilm_CNPM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedDatabase1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BaiViet", "MoTa", c => c.String(nullable: false));
            AddColumn("dbo.BaiViet", "TuKhoa", c => c.String(nullable: false));
            AddColumn("dbo.BaiViet", "NguoiTao", c => c.Int(nullable: false));
            AddColumn("dbo.BaiViet", "NgayTao", c => c.DateTime(nullable: false));
            AddColumn("dbo.BaiViet", "NguoiCapNhat", c => c.Int());
            AddColumn("dbo.BaiViet", "NgayCapNhat", c => c.DateTime());
            AddColumn("dbo.BaiViet", "TrangThai", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BaiViet", "TrangThai");
            DropColumn("dbo.BaiViet", "NgayCapNhat");
            DropColumn("dbo.BaiViet", "NguoiCapNhat");
            DropColumn("dbo.BaiViet", "NgayTao");
            DropColumn("dbo.BaiViet", "NguoiTao");
            DropColumn("dbo.BaiViet", "TuKhoa");
            DropColumn("dbo.BaiViet", "MoTa");
        }
    }
}
