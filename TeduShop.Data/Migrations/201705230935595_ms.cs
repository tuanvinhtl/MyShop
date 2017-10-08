namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ms : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductCategories", "ProductCategoryParentID", "dbo.ParentProductCategories");
            DropIndex("dbo.ProductCategories", new[] { "ProductCategoryParentID" });
            AlterColumn("dbo.ProductCategories", "ProductCategoryParentID", c => c.Int(nullable: false));
            CreateIndex("dbo.ProductCategories", "ProductCategoryParentID");
            AddForeignKey("dbo.ProductCategories", "ProductCategoryParentID", "dbo.ParentProductCategories", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductCategories", "ProductCategoryParentID", "dbo.ParentProductCategories");
            DropIndex("dbo.ProductCategories", new[] { "ProductCategoryParentID" });
            AlterColumn("dbo.ProductCategories", "ProductCategoryParentID", c => c.Int());
            CreateIndex("dbo.ProductCategories", "ProductCategoryParentID");
            AddForeignKey("dbo.ProductCategories", "ProductCategoryParentID", "dbo.ParentProductCategories", "ID");
        }
    }
}
