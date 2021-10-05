namespace CRUDEmployeeMVC.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialEmpM : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProfilePic = c.String(),
                        FullName = c.String(nullable: false),
                        Phone = c.Long(nullable: false),
                        Gender = c.String(nullable: false),
                        Age = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        EmpDoc = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Employees");
        }
    }
}
