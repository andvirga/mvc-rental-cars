namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmailRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clients", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clients", "Email", c => c.String());
        }
    }
}
