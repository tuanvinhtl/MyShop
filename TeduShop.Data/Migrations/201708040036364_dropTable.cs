namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dropTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ApplicatonUserRoles", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApplicatonUserRoles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
    }
}
