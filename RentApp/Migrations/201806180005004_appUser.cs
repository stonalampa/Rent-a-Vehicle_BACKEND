namespace RentApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppUsers", "Username", c => c.String());
            AddColumn("dbo.AppUsers", "Role", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppUsers", "Role");
            DropColumn("dbo.AppUsers", "Username");
        }
    }
}
