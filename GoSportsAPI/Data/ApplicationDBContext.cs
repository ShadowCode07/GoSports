using GoSportsAPI.Models.Lobbies;
using GoSportsAPI.Models.Locations;
using GoSportsAPI.Models.Sports;
using Microsoft.EntityFrameworkCore;
using System;

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
        public DbSet<Sport> sports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>()
                .Property(p => p.Version)
                .IsRowVersion();

            modelBuilder.Entity<LocationType>()
                .Property(p => p.Version)
                .IsRowVersion();

            modelBuilder.Entity<LocationType>()
                .Property(p => p.Version)
                .IsRowVersion();

            modelBuilder.Entity<LocationType>()
                .Property(p => p.Version)
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
