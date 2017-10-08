namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUsers", "CreatedDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.ApplicationUsers", "OrderBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApplicationUsers", "OrderBy", c => c.Int(nullable: false));
            DropColumn("dbo.ApplicationUsers", "CreatedDate");
        }
    }
}
