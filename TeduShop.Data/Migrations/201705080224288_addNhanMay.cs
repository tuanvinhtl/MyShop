namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNhanMay : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BangNhanMay",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        KhachHang_ID = c.Int(),
                        MayTinh_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BangKhachHang", t => t.KhachHang_ID)
                .ForeignKey("dbo.BangMayTinh", t => t.MayTinh_ID)
                .Index(t => t.KhachHang_ID)
                .Index(t => t.MayTinh_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BangNhanMay", "MayTinh_ID", "dbo.BangMayTinh");
            DropForeignKey("dbo.BangNhanMay", "KhachHang_ID", "dbo.BangKhachHang");
            DropIndex("dbo.BangNhanMay", new[] { "MayTinh_ID" });
            DropIndex("dbo.BangNhanMay", new[] { "KhachHang_ID" });
            DropTable("dbo.BangNhanMay");
        }
    }
}
