namespace RentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class carMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vehicles", "CarModel", c => c.String());
            DropColumn("dbo.Vehicles", "Model");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vehicles", "Model", c => c.String());
            DropColumn("dbo.Vehicles", "CarModel");
        }
    }
}
