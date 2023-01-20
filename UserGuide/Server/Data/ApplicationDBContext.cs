using Microsoft.EntityFrameworkCore;

namespace UserGuide.Server.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options ):base(options)
        {

        }

        public DbSet<UserData> User { get;set; }
    }


}
