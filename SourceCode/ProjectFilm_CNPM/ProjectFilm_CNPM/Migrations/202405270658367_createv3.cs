namespace ProjectFilm_CNPM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createv3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NguoiDung", "TongTienTichLuy", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NguoiDung", "TongTienTichLuy");
        }
    }
}
