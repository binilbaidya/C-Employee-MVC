namespace CRUDEmployeeMVC.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmpDocM : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "EmpDoc", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "EmpDoc");
        }
    }
}
