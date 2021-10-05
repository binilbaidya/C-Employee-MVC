using CRUDEmployeeMVC.Model;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CRUDEmployeeMVC.Repository
{
    public class IdentityDatabase : IdentityDbContext<ApplicationUser>
    {
        public IdentityDatabase()
            : base("PracticeNET", throwIfV1Schema: false)
        {
        }

        public static IdentityDatabase Create()
        {
            return new IdentityDatabase();
        }
    }
}
