namespace WebApiFutLunes.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Matches : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LocationTitle = c.String(),
                        LocationMapUrl = c.String(),
                        MatchDate = c.DateTime(nullable: false),
                        PlayerLimit = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "Match_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Match_Id");
            AddForeignKey("dbo.AspNetUsers", "Match_Id", "dbo.Matches", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Match_Id", "dbo.Matches");
            DropIndex("dbo.AspNetUsers", new[] { "Match_Id" });
            DropColumn("dbo.AspNetUsers", "Match_Id");
            DropTable("dbo.Matches");
        }
    }
}
