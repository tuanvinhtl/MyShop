namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BangChiTietSuaChua",
                c => new
                    {
                        IDKhachHang = c.Int(nullable: false),
                        IDMayTinh = c.Int(nullable: false),
                        MoTaSuaChua = c.String(),
                        NgaySuaChua = c.DateTime(nullable: false),
                        NguoiSuaChua = c.String(),
                        BaoHanh = c.Int(),
                        TrangThai = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.IDKhachHang, t.IDMayTinh })
                .ForeignKey("dbo.BangKhachHang", t => t.IDKhachHang, cascadeDelete: true)
                .ForeignKey("dbo.BangMayTinh", t => t.IDMayTinh, cascadeDelete: true)
                .Index(t => t.IDKhachHang)
                .Index(t => t.IDMayTinh);
            
            CreateTable(
                "dbo.BangKhachHang",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(),
                        PhoneNumber = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.BangMayTinh",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CategoryPC = c.Boolean(),
                        Desciption = c.String(),
                        CreatedDate = c.DateTime(),
                        Status = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BangChiTietSuaChua", "IDMayTinh", "dbo.BangMayTinh");
            DropForeignKey("dbo.BangChiTietSuaChua", "IDKhachHang", "dbo.BangKhachHang");
            DropIndex("dbo.BangChiTietSuaChua", new[] { "IDMayTinh" });
            DropIndex("dbo.BangChiTietSuaChua", new[] { "IDKhachHang" });
            DropTable("dbo.BangMayTinh");
            DropTable("dbo.BangKhachHang");
            DropTable("dbo.BangChiTietSuaChua");
        }
    }
}
