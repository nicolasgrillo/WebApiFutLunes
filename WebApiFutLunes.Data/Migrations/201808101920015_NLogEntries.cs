namespace WebApiFutLunes.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NLogEntries : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NLogEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Exception = c.String(),
                        Level = c.String(),
                        StackTrace = c.String(),
                        Username = c.String(),
                        Date = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.NLogEntries");
        }
    }
}
