namespace WebApiFutLunes.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MatchStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Matches", "Open", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Matches", "Open");
        }
    }
}
