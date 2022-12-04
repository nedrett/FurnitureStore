using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Infrastructure.Data
{
    using Configuration;
    using Models;

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new ArmChairConfiguration());
            builder.ApplyConfiguration(new ChairConfiguration());
            builder.ApplyConfiguration(new SofaConfiguration());
            builder.ApplyConfiguration(new TableConfiguration());
            builder.ApplyConfiguration(new TvTableConfiguration());
            
            base.OnModelCreating(builder);
        }

        public DbSet<Table> Tables { get; set; }

        public DbSet<Chair> Chairs { get; set; }

        public DbSet<Sofa> Sofas { get; set; }

        public DbSet<ArmChair> ArmChairs { get; set; }

        public DbSet<TvTable> TvTables { get; set; }
    }
}