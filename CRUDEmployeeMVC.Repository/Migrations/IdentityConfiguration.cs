namespace CRUDEmployeeMVC.Repository.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class IdentityConfiguration : DbMigrationsConfiguration<CRUDEmployeeMVC.Repository.IdentityDatabase>
    {
        public IdentityConfiguration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CRUDEmployeeMVC.Repository.IdentityDatabase context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
