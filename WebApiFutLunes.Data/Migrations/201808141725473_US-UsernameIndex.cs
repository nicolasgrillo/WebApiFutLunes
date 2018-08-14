namespace WebApiFutLunes.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class USUsernameIndex : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserSubscriptions", "User", c => c.String());
            DropColumn("dbo.UserSubscriptions", "Username");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserSubscriptions", "Username", c => c.String());
            DropColumn("dbo.UserSubscriptions", "User");
        }
    }
}
