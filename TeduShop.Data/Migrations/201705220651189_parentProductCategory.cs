namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class parentProductCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParentProductCategorys",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.ProductCategories", "ProductCategoryParentID", c => c.Int());
            CreateIndex("dbo.ProductCategories", "ProductCategoryParentID");
            AddForeignKey("dbo.ProductCategories", "ProductCategoryParentID", "dbo.ParentProductCategorys", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductCategories", "ProductCategoryParentID", "dbo.ParentProductCategorys");
            DropIndex("dbo.ProductCategories", new[] { "ProductCategoryParentID" });
            DropColumn("dbo.ProductCategories", "ProductCategoryParentID");
            DropTable("dbo.ParentProductCategorys");
        }
    }
}
