using Microsoft.EntityFrameworkCore;
using UserService.Models;

namespace UserService.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        { 
        }
        public DbSet<User> users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=mssqlstud.fhict.local;Database=dbi465821_user;User Id=dbi465821_user;Password=Voucugklir2;TrustServerCertificate=True");
        }

    }
}
