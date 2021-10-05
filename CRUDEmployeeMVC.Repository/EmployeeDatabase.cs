using CRUDEmployeeMVC.Model;
using System.Data.Entity;

namespace CRUDEmployeeMVC.Repository
{
    public class EmployeeDatabase : DbContext
    {
        public EmployeeDatabase() : base("PracticeNET")
        {
        }
        public DbSet<Employee> Employee { get; set; }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                var entity = entry.Entity;
                if(entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entity.GetType().GetProperty("IsDeleted").SetValue(entity, true);
                }
            }
            return base.SaveChanges();
        }
    }
}
