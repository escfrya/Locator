namespace Locator.DAL.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Login = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        DisplayName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserPushes",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        IsActive = c.Boolean(nullable: false),
                        UserId = c.Long(nullable: false),
                        DeviceAppId = c.String(nullable: false),
                        ClientVersion = c.String(),
                        PlatformType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        FromUserId = c.Long(nullable: false),
                        ToUserId = c.Long(nullable: false),
                        Message = c.String(),
                        Description = c.String(),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.FromUserId)
                .ForeignKey("dbo.Users", t => t.ToUserId)
                .Index(t => t.FromUserId)
                .Index(t => t.ToUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Locations", "ToUserId", "dbo.Users");
            DropForeignKey("dbo.Locations", "FromUserId", "dbo.Users");
            DropForeignKey("dbo.UserPushes", "UserId", "dbo.Users");
            DropIndex("dbo.Locations", new[] { "ToUserId" });
            DropIndex("dbo.Locations", new[] { "FromUserId" });
            DropIndex("dbo.UserPushes", new[] { "UserId" });
            DropTable("dbo.Locations");
            DropTable("dbo.UserPushes");
            DropTable("dbo.Users");
        }
    }
}
