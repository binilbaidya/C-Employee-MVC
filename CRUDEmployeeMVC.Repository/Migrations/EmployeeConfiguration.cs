namespace CRUDEmployeeMVC.Repository.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class EmployeeConfiguration : DbMigrationsConfiguration<CRUDEmployeeMVC.Repository.EmployeeDatabase>
    {
        public EmployeeConfiguration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CRUDEmployeeMVC.Repository.EmployeeDatabase context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
