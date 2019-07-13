namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeAuth_V1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        AuthenticationID = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                        Role = c.String(),
                        AccessToken = c.String(),
                        TokenType = c.String(),
                        ExpiresIn = c.String(),
                    })
                .PrimaryKey(t => t.AuthenticationID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        AuthenticationID = c.Int(nullable: false, identity: true),
                        EmployeeName = c.String(),
                        AnnualSalary = c.String(),
                        MonthlySalary = c.String(),
                        EmployeeType = c.String(),
                    })
                .PrimaryKey(t => t.AuthenticationID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Employees");
            DropTable("dbo.Users");
        }
    }
}
