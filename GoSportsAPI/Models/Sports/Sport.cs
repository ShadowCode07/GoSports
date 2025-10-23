using GoSportsAPI.Models.Locations;

namespace GoSportsAPI.Models.Sports
{
    public class Sport
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public ICollection<Location> Locations { get; set; } = new List<Location>();
    }
}
