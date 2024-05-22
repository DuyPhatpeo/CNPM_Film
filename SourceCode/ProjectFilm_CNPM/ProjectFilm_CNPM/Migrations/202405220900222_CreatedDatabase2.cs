namespace ProjectFilm_CNPM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedDatabase2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ghe", "TinhTrangGhe", c => c.Boolean());
            AddColumn("dbo.NguoiDung", "XacNhanMatKhau", c => c.String());
            AlterColumn("dbo.NguoiDung", "MatKhau", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.NguoiDung", "MatKhau", c => c.String(nullable: false));
            DropColumn("dbo.NguoiDung", "XacNhanMatKhau");
            DropColumn("dbo.Ghe", "TinhTrangGhe");
        }
    }
}
