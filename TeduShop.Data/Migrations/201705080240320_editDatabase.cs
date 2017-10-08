namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editDatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BangNhanMay", "NgayNhan", c => c.DateTime(nullable: false));
            AddColumn("dbo.BangNhanMay", "TinhTrangMay", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BangNhanMay", "TinhTrangMay");
            DropColumn("dbo.BangNhanMay", "NgayNhan");
        }
    }
}
