using GoSportsAPI.Mdels.Lobbies;
using GoSportsAPI.Mdels.Locations;
using GoSportsAPI.Models.Sports;
using Microsoft.EntityFrameworkCore;

namespace GoSportsAPI.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Location> locations { get; set; }
        public DbSet<LocationType> locationTypes { get; set; }
        public DbSet<Lobby> lobbies { get; set; }
        public DbSet<Sport> Sports { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>()
            .Property(f => f.Id)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<LocationType>()
            .Property(f => f.Id)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<Lobby>()
            .Property(f => f.Id)
            .ValueGeneratedOnAdd();

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
