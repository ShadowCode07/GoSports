using GoSportsAPI.Data;
using GoSportsAPI.Models.Locations;
using GoSportsAPI.Models.Sports;
using System;

namespace GoSportsAPI
{
    public class Seed
    {
        private readonly ApplicationDBContext _context;

        public Seed(ApplicationDBContext context)
        {
            _context = context;
        }

        public void SeedDataContext()
        {
            if (!_context.sports.Any() && !_context.locations.Any() && !_context.locationTypes.Any())
            {
                var football = new Sport { Id = Guid.NewGuid(), Name = "Football" };
                var basketball = new Sport { Id = Guid.NewGuid(), Name = "Basketball" };
                var tennis = new Sport { Id = Guid.NewGuid(), Name = "Tennis" };
                var swimming = new Sport { Id = Guid.NewGuid(), Name = "Swimming" };

                var sports = new List<Sport> { football, basketball, tennis, swimming };

                var park = new Location
                {
                    Id = Guid.NewGuid(),
                    Name = "Greenfield Park",
                    Description = "A large open park perfect for football and outdoor sports.",
                    Latitude = 51.4416,
                    Longitude = 5.4697,
                    CurrentLobbyCount = 0,
                    MaxLobbyCount = 10,
                    LocationType = new LocationType
                    {
                        Id = Guid.NewGuid(),
                        Name = "Outdoor Field",
                        IsIndoor = false,
                        Surface = "Grass",
                        HasLights = true
                    },
                    Sports = new List<Sport> { football }
                };

                var arena = new Location
                {
                    Id = Guid.NewGuid(),
                    Name = "City Arena",
                    Description = "Indoor arena suitable for basketball games and tournaments.",
                    Latitude = 51.4415,
                    Longitude = 5.4785,
                    CurrentLobbyCount = 0,
                    MaxLobbyCount = 8,
                    LocationType = new LocationType
                    {
                        Id = Guid.NewGuid(),
                        Name = "Indoor Court",
                        IsIndoor = true,
                        Surface = "Hardwood",
                        HasLights = true
                    },
                    Sports = new List<Sport> { basketball }
                };

                var tennisClub = new Location
                {
                    Id = Guid.NewGuid(),
                    Name = "Ace Tennis Club",
                    Description = "High-quality outdoor and indoor tennis courts.",
                    Latitude = 51.4420,
                    Longitude = 5.4800,
                    CurrentLobbyCount = 0,
                    MaxLobbyCount = 6,
                    LocationType = new LocationType
                    {
                        Id = Guid.NewGuid(),
                        Name = "Clay Court",
                        IsIndoor = false,
                        Surface = "Clay",
                        HasLights = true
                    },
                    Sports = new List<Sport> { tennis }
                };

                var pool = new Location
                {
                    Id = Guid.NewGuid(),
                    Name = "BlueWave Swimming Complex",
                    Description = "Olympic-size indoor swimming pool with professional facilities.",
                    Latitude = 51.4430,
                    Longitude = 5.4820,
                    CurrentLobbyCount = 0,
                    MaxLobbyCount = 5,
                    LocationType = new LocationType
                    {
                        Id = Guid.NewGuid(),
                        Name = "Indoor Pool",
                        IsIndoor = true,
                        Surface = "Tile",
                        HasLights = true
                    },
                    Sports = new List<Sport> { swimming }
                };

                var locations = new List<Location> { park, arena, tennisClub, pool };

                _context.sports.AddRange(sports);
                _context.locations.AddRange(locations);
                _context.SaveChanges();
            }
        }
    }
}
