namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeAuth_V3 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Users", newName: "Users");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Users", newName: "Users");
        }
    }
}
