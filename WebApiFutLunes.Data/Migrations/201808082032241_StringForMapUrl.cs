namespace WebApiFutLunes.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StringForMapUrl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Matches", "LocationMapUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Matches", "LocationMapUrl");
        }
    }
}
