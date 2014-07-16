namespace Locator.DAL.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Locations", "Date", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Locations", "Date");
        }
    }
}
