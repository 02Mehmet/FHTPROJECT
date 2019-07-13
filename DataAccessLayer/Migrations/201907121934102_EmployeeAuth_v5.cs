namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeAuth_v5 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "AccessToken");
            DropColumn("dbo.Users", "TokenType");
            DropColumn("dbo.Users", "ExpiresIn");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "ExpiresIn", c => c.String());
            AddColumn("dbo.Users", "TokenType", c => c.String());
            AddColumn("dbo.Users", "AccessToken", c => c.String());
        }
    }
}
