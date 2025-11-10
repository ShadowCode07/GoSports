using GoSportsAPI.Models.Lobbies;
using GoSportsAPI.Models.Locations;
using GoSportsAPI.Models.Sports;
using GoSportsAPI.Models.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GoSportsAPI.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Location> locations { get; set; }
        public DbSet<LocationType> locationTypes { get; set; }
        public DbSet<Lobby> lobbies { get; set; }
        public DbSet<Sport> sports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Location>()
                .Property(l => l.Version)
                .IsRowVersion();

            modelBuilder.Entity<LocationType>()
                .Property(l => l.Version)
                .IsRowVersion();

            modelBuilder.Entity<Lobby>()
                .Property(l => l.Version)
                .IsRowVersion();

            modelBuilder.Entity<Sport>()
                .Property(s => s.Version)
                .IsRowVersion();

            modelBuilder.Entity<Location>()
                .Property(f => f.LocationId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<LocationType>()
                .Property(f => f.LocationTypeId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Lobby>()
                .Property(f => f.LobbyId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<AppUser>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Lobby>()
                .HasIndex(l => l.Name)
                .IsUnique();

            modelBuilder.Entity<Sport>()
                .Property(f => f.SportId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Sport>()
                .HasIndex(l => l.Name)
                .IsUnique();

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

            modelBuilder.Entity<Lobby>()
                .HasOne(l => l.Sport)
                .WithMany()
                .HasForeignKey(l => l.SportId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Location>()
                .HasMany(l => l.Sports)
                .WithMany(s => s.Locations)
                .UsingEntity(j => j.ToTable("LocationSports"));
        }
    }
}
