namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addContact : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contacts", "Lat", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Contacts", "Lng", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contacts", "Lng", c => c.Int(nullable: false));
            AlterColumn("dbo.Contacts", "Lat", c => c.Int(nullable: false));
        }
    }
}
