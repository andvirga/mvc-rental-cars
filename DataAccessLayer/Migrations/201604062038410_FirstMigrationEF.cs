namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigrationEF : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        CarID = c.Long(nullable: false, identity: true),
                        Domain = c.String(),
                        Brand = c.String(),
                        Model = c.String(),
                        DailyTariff = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AutomaticDrive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CarID);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientID = c.Long(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.ClientID);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ReservationID = c.Long(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Client_ClientID = c.Long(),
                        RentedCar_CarID = c.Long(),
                    })
                .PrimaryKey(t => t.ReservationID)
                .ForeignKey("dbo.Clients", t => t.Client_ClientID)
                .ForeignKey("dbo.Cars", t => t.RentedCar_CarID)
                .Index(t => t.Client_ClientID)
                .Index(t => t.RentedCar_CarID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "RentedCar_CarID", "dbo.Cars");
            DropForeignKey("dbo.Reservations", "Client_ClientID", "dbo.Clients");
            DropIndex("dbo.Reservations", new[] { "RentedCar_CarID" });
            DropIndex("dbo.Reservations", new[] { "Client_ClientID" });
            DropTable("dbo.Reservations");
            DropTable("dbo.Clients");
            DropTable("dbo.Cars");
        }
    }
}
