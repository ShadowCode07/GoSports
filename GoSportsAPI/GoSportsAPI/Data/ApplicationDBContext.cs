using GoSportsAPI.Mdels.Lobbies;
using GoSportsAPI.Mdels.Locations;
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
        public DbSet<Lobby> lobby { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>()
                .HasOne(l => l.LocationType)
                .WithOne()
                .HasForeignKey<LocationType>(t => t.LocationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Lobby>()
            .HasOne(l => l.Location)
            .WithMany(loc => loc.Lobbies)
            .HasForeignKey(l => l.LocationId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
