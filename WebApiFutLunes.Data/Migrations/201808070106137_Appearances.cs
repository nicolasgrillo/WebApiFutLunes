using System.Data.Entity.Migrations;

namespace WebApiFutLunes.Data.Migrations
{
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
