namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewModel2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "Domain", c => c.String(nullable: false));
            DropColumn("dbo.Cars", "Doamin");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cars", "Doamin", c => c.String(nullable: false));
            DropColumn("dbo.Cars", "Domain");
        }
    }
}
