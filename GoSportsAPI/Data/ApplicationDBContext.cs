using GoSportsAPI.Models;
using GoSportsAPI.Models.Lobbies;
using GoSportsAPI.Models.Locations;
using GoSportsAPI.Models.Sports;
using GoSportsAPI.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GoSportsAPI.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Location> Locations { get; set; }
        public DbSet<LocationType> LocationTypes { get; set; }
        public DbSet<Lobby> Lobbies { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

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
                .Property(l => l.Version)
                .IsRowVersion();


            modelBuilder.Entity<Location>()
                .Property(f => f.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<LocationType>()
                .Property(f => f.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Lobby>()
                .Property(f => f.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Sport>()
                .Property(f => f.Id)
                .ValueGeneratedOnAdd();


            //Location
            modelBuilder.Entity<Location>()
                .HasMany(l => l.Sports)
                .WithMany(s => s.Locations)
                .UsingEntity(j => j.ToTable("LocationSports"));


            modelBuilder.Entity<Location>()
                .HasOne(l => l.LocationType)
                .WithOne()
                .HasForeignKey<LocationType>(t => t.LocationId)
                .OnDelete(DeleteBehavior.Cascade);

            //Lobby
            modelBuilder.Entity<Lobby>()
                .HasIndex(l => l.Name)
                .IsUnique(true);

            modelBuilder.Entity<Lobby>()
                .HasIndex(l => l.Code)
                .IsUnique(true);

            modelBuilder.Entity<Lobby>()
                .HasOne(l => l.Location)
                .WithMany(loc => loc.Lobbies)
                .HasForeignKey(l => l.LocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Lobby>()
                .HasOne(l => l.Sport)
                .WithMany()
                .HasForeignKey(l => l.SportId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Lobby>()
                .HasOne(l => l.HostProfile)
                .WithMany()
                .HasForeignKey(l => l.HostProfileId)
                .OnDelete(DeleteBehavior.Restrict);

            //Sport
            modelBuilder.Entity<Sport>()
                .HasIndex(l => l.Name)
                .IsUnique(true);

            //UserProfile
            modelBuilder.Entity<UserProfile>()
                .HasMany(u => u.Sports)
                .WithMany(s => s.UserProfiles)
                .UsingEntity(j => j.ToTable("UserSports"));

            modelBuilder.Entity<UserProfile>()
                .HasOne(p => p.User)
                .WithOne(u => u.Profile)
                .HasForeignKey<UserProfile>(p => p.Id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserProfile>()
                .HasOne(u => u.Lobby)
                .WithMany(l => l.Users)
                .HasForeignKey(u => u.LobbyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}