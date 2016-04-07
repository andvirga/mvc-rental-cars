namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class carmodelmodify : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cars", "Doamin", c => c.String(nullable: false));
            AlterColumn("dbo.Cars", "Brand", c => c.String(nullable: false));
            AlterColumn("dbo.Cars", "Model", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cars", "Model", c => c.String());
            AlterColumn("dbo.Cars", "Brand", c => c.String());
            AlterColumn("dbo.Cars", "Doamin", c => c.String());
        }
    }
}
