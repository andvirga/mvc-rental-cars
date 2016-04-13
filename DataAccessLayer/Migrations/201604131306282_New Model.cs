namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservations", "Client_ClientID", "dbo.Clients");
            DropForeignKey("dbo.Reservations", "RentedCar_CarID", "dbo.Cars");
            DropIndex("dbo.Reservations", new[] { "Client_ClientID" });
            DropIndex("dbo.Reservations", new[] { "RentedCar_CarID" });
            RenameColumn(table: "dbo.Reservations", name: "Client_ClientID", newName: "ClientID");
            RenameColumn(table: "dbo.Reservations", name: "RentedCar_CarID", newName: "CarID");
            AlterColumn("dbo.Reservations", "ClientID", c => c.Long(nullable: false));
            AlterColumn("dbo.Reservations", "CarID", c => c.Long(nullable: false));
            CreateIndex("dbo.Reservations", "ClientID");
            CreateIndex("dbo.Reservations", "CarID");
            AddForeignKey("dbo.Reservations", "ClientID", "dbo.Clients", "ClientID", cascadeDelete: true);
            AddForeignKey("dbo.Reservations", "CarID", "dbo.Cars", "CarID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "CarID", "dbo.Cars");
            DropForeignKey("dbo.Reservations", "ClientID", "dbo.Clients");
            DropIndex("dbo.Reservations", new[] { "CarID" });
            DropIndex("dbo.Reservations", new[] { "ClientID" });
            AlterColumn("dbo.Reservations", "CarID", c => c.Long());
            AlterColumn("dbo.Reservations", "ClientID", c => c.Long());
            RenameColumn(table: "dbo.Reservations", name: "CarID", newName: "RentedCar_CarID");
            RenameColumn(table: "dbo.Reservations", name: "ClientID", newName: "Client_ClientID");
            CreateIndex("dbo.Reservations", "RentedCar_CarID");
            CreateIndex("dbo.Reservations", "Client_ClientID");
            AddForeignKey("dbo.Reservations", "RentedCar_CarID", "dbo.Cars", "CarID");
            AddForeignKey("dbo.Reservations", "Client_ClientID", "dbo.Clients", "ClientID");
        }
    }
}
