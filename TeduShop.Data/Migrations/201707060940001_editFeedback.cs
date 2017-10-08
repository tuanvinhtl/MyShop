namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editFeedback : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Feedbacks", "Subject", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Feedbacks", "Subject");
        }
    }
}
