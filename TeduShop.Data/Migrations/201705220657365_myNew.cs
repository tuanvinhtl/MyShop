namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class myNew : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ParentProductCategorys", newName: "ParentProductCategories");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ParentProductCategories", newName: "ParentProductCategorys");
        }
    }
}
