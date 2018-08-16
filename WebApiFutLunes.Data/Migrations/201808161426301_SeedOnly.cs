namespace WebApiFutLunes.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedOnly : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserSubscriptions", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.UserSubscriptions", new[] { "User_Id" });
            AddColumn("dbo.UserSubscriptions", "User", c => c.String());
            DropColumn("dbo.UserSubscriptions", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserSubscriptions", "User_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.UserSubscriptions", "User");
            CreateIndex("dbo.UserSubscriptions", "User_Id");
            AddForeignKey("dbo.UserSubscriptions", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
