namespace Locator.DAL.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PushChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserPushes", "UserID", "dbo.Users");
            DropIndex("dbo.UserPushes", new[] { "UserID" });
            AddColumn("dbo.UserPushes", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserPushes", "DeviceAppId", c => c.String(nullable: false));
            AddColumn("dbo.UserPushes", "ClientVersion", c => c.String());
            AddColumn("dbo.UserPushes", "PlatformType", c => c.Int(nullable: false));
            AlterColumn("dbo.UserPushes", "UserId", c => c.Long(nullable: false));
            CreateIndex("dbo.UserPushes", "UserId");
            AddForeignKey("dbo.UserPushes", "UserId", "dbo.Users", "ID");
            DropColumn("dbo.UserPushes", "PhoneID");
            DropColumn("dbo.UserPushes", "PushUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserPushes", "PushUrl", c => c.String());
            AddColumn("dbo.UserPushes", "PhoneID", c => c.String(nullable: false));
            DropForeignKey("dbo.UserPushes", "UserId", "dbo.Users");
            DropIndex("dbo.UserPushes", new[] { "UserId" });
            AlterColumn("dbo.UserPushes", "UserID", c => c.Long(nullable: false));
            DropColumn("dbo.UserPushes", "PlatformType");
            DropColumn("dbo.UserPushes", "ClientVersion");
            DropColumn("dbo.UserPushes", "DeviceAppId");
            DropColumn("dbo.UserPushes", "IsActive");
            CreateIndex("dbo.UserPushes", "UserID");
            AddForeignKey("dbo.UserPushes", "UserID", "dbo.Users", "ID");
        }
    }
}
