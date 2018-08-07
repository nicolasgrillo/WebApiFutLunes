namespace WebApiFutLunes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Appearances : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Appearances", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Appearances");
        }
    }
}
