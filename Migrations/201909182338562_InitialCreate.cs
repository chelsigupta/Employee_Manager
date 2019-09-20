namespace EmployeeManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activity",
                c => new
                    {
                        ActivityID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Timestamp = c.DateTime(nullable: false),
                        Employee_EmployeeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ActivityID)
                .ForeignKey("dbo.Employee", t => t.Employee_EmployeeID, cascadeDelete: true)
                .Index(t => t.Employee_EmployeeID);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        Shift = c.Int(nullable: false),
                        Photo = c.String(),
                        Color = c.String(),
                        Permission = c.Int(nullable: false),
                        Department_DepartmentID = c.Int(),
                        Manager_EmployeeID = c.Int(),
                        Position_PositionID = c.Int(),
                    })
                .PrimaryKey(t => t.EmployeeID)
                .ForeignKey("dbo.Department", t => t.Department_DepartmentID)
                .ForeignKey("dbo.Employee", t => t.Manager_EmployeeID)
                .ForeignKey("dbo.Position", t => t.Position_PositionID)
                .Index(t => t.Department_DepartmentID)
                .Index(t => t.Manager_EmployeeID)
                .Index(t => t.Position_PositionID);
            
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.DepartmentID);
            
            CreateTable(
                "dbo.Position",
                c => new
                    {
                        PositionID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.PositionID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Activity", "Employee_EmployeeID", "dbo.Employee");
            DropForeignKey("dbo.Employee", "Position_PositionID", "dbo.Position");
            DropForeignKey("dbo.Employee", "Manager_EmployeeID", "dbo.Employee");
            DropForeignKey("dbo.Employee", "Department_DepartmentID", "dbo.Department");
            DropIndex("dbo.Employee", new[] { "Position_PositionID" });
            DropIndex("dbo.Employee", new[] { "Manager_EmployeeID" });
            DropIndex("dbo.Employee", new[] { "Department_DepartmentID" });
            DropIndex("dbo.Activity", new[] { "Employee_EmployeeID" });
            DropTable("dbo.Position");
            DropTable("dbo.Department");
            DropTable("dbo.Employee");
            DropTable("dbo.Activity");
        }
    }
}
