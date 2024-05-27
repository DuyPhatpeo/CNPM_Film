namespace ProjectFilm_CNPM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createdv2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HoaDon", "TongTien", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HoaDon", "TongTien");
        }
    }
}
