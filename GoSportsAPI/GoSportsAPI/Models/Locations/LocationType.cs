using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace GoSportsAPI.Mdels.Locations
{
    public class LocationType
    {
        public Guid Id { get; set; }
        public Guid LocationId { get; set; }

        public string Name { get; set; } = string.Empty;
        public bool IsIndoor { get; set; }
        public string Surface { get; set; } = string.Empty;
        public bool HasLights { get; set; }

    }
}
