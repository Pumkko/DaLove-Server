using DaLove_Server.Data.Domain;
using Microsoft.EntityFrameworkCore;

namespace DaLove_Server.Data
{
    public class DaLoveDbContext : DbContext
    {
        public DbSet<UserMemory> Memories { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        public DaLoveDbContext(DbContextOptions<DaLoveDbContext> option) : base(option)
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfile>().HasIndex(p => p.UniqueUserName).IsUnique();
            modelBuilder.Entity<UserMemory>().HasIndex(p => p.MemoryUniqueName).IsUnique();
        }
    }
}
