using GoSportsAPI.Models.Locations;
using GoSportsAPI.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace GoSportsAPI.Models.Sports
{
    public class Sport
    {
        [Key]
        public Guid Id { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Location> Locations { get; set; } = new List<Location>();
        public ICollection<UserProfile> AppUsers { get; set; } = new List<UserProfile>();
    }
}
