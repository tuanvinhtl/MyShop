namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUserRole : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicatonUserRoles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicatonUserRoles", "Discriminator");
        }
    }
}
