using GoSportsAPI.Mdels.Locations;
using GoSportsAPI.Models.Sports;

namespace GoSportsAPI.Models.BridgeTables
{
    public class LocationSport
    {
        public Guid LocationId { get; set; }
        public Location Location { get; set; } = null!;

        public Guid SportId { get; set; }
        public Sport Sport { get; set; } = null!;
    }
}
