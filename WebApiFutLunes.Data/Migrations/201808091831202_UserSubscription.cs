namespace WebApiFutLunes.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserSubscription : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AspNetUsers", new[] { "Match_Id" });
            CreateTable(
                "dbo.UserSubscriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubscriptionDate = c.DateTime(nullable: false),
                        User_Id = c.String(maxLength: 128),
                        Match_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Match_Id);
            
            DropColumn("dbo.AspNetUsers", "Match_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Match_Id", c => c.Int());
            DropForeignKey("dbo.UserSubscriptions", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.UserSubscriptions", new[] { "Match_Id" });
            DropIndex("dbo.UserSubscriptions", new[] { "User_Id" });
            DropTable("dbo.UserSubscriptions");
            CreateIndex("dbo.AspNetUsers", "Match_Id");
        }
    }
}
