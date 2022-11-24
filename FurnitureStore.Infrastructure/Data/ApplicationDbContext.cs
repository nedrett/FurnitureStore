using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Infrastructure.Data
{
    using Models;

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Table> Tables { get; set; }

        public DbSet<Chair> Chairs { get; set; }

        public DbSet<Sofa> Sofas { get; set; }

        public DbSet<ArmChair> ArmChairs { get; set; }
    }
}