namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditContact : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contacts", "Lat", c => c.Double(nullable: false));
            AlterColumn("dbo.Contacts", "Lng", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contacts", "Lng", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Contacts", "Lat", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
