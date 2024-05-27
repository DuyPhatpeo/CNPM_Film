namespace ProjectFilm_CNPM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createv31 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Ghe", "TinhTrangGhe");
            DropColumn("dbo.NguoiDung", "TongTienTichLuy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NguoiDung", "TongTienTichLuy", c => c.Int(nullable: false));
            AddColumn("dbo.Ghe", "TinhTrangGhe", c => c.Boolean());
        }
    }
}
