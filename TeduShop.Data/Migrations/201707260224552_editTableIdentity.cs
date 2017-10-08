namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editTableIdentity : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.IdentityUserClaims", newName: "ApplicationUserClaims");
            RenameTable(name: "dbo.IdentityUserLogins", newName: "ApplicationUserLogins");
            RenameTable(name: "dbo.IdentityUserRoles", newName: "ApplicatonUserRoles");
            RenameTable(name: "dbo.IdentityRoles", newName: "ApplicationRoles");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ApplicationRoles", newName: "IdentityRoles");
            RenameTable(name: "dbo.ApplicatonUserRoles", newName: "IdentityUserRoles");
            RenameTable(name: "dbo.ApplicationUserLogins", newName: "IdentityUserLogins");
            RenameTable(name: "dbo.ApplicationUserClaims", newName: "IdentityUserClaims");
        }
    }
}
