namespace RentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BranchOffices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Photo = c.String(),
                        Address = c.String(),
                        Longitude = c.Single(nullable: false),
                        Latitude = c.Single(nullable: false),
                        Approved = c.Boolean(nullable: false),
                        Office = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        timestamp = c.DateTime(nullable: false),
                        Grade = c.Int(nullable: false),
                        Comment = c.String(),
                        ServiceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Driver = c.String(),
                        Available = c.Boolean(nullable: false),
                        BranchOffice = c.String(),
                        PricePerHour = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AppUsers", "dateOfBirth", c => c.DateTime(nullable: false));
            AddColumn("dbo.AppUsers", "PhotoPath", c => c.String());
            AddColumn("dbo.AppUsers", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.Services", "Logo", c => c.String());
            AddColumn("dbo.Services", "EMail", c => c.String());
            AddColumn("dbo.Services", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Services", "Description");
            DropColumn("dbo.Services", "EMail");
            DropColumn("dbo.Services", "Logo");
            DropColumn("dbo.AppUsers", "Type");
            DropColumn("dbo.AppUsers", "PhotoPath");
            DropColumn("dbo.AppUsers", "dateOfBirth");
            DropTable("dbo.Vehicles");
            DropTable("dbo.Ratings");
            DropTable("dbo.BranchOffices");
        }
    }
}
