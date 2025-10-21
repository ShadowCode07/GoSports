using GoSportsAPI.Mdels.Lobbies;
using GoSportsAPI.Models.BridgeTables;

namespace GoSportsAPI.Models.Sports
{
    public class Sport
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public ICollection<LocationSport> LocationSports { get; set; } = new List<LocationSport>();
    }
}
