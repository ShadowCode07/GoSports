using GoSportsAPI.Mdels.Location;
using Microsoft.EntityFrameworkCore;

namespace GoSportsAPI.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Location> Location { get; set; }
        public DbSet<LocationType> locationType { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>()
                .HasOne(l => l.LocationType)
                .WithOne()
                .HasForeignKey<LocationType>(t => t.LocationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
