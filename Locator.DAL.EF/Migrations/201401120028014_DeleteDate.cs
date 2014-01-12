namespace Locator.DAL.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteDate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Locations", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Locations", "Date", c => c.DateTime(nullable: false));
        }
    }
}
