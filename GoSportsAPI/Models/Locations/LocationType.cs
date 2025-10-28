namespace GoSportsAPI.Models.Locations
{
    public class LocationType
    {
        /// <summary>
        /// The unique identifier of this location type.
        /// </summary>
        public Guid LocationTypeId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// The identifier of the parent location.
        /// </summary>
        public Guid LocationId { get; set; }

        /// <summary>
        /// The descriptive name of the location type (e.g., "Football Field A").
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Indicates whether the location is indoors.
        /// </summary>
        public bool IsIndoor { get; set; }

        /// <summary>
        /// The type of playing surface (e.g., grass, turf, hardwood).
        /// </summary>
        public string Surface { get; set; } = string.Empty;

        /// <summary>
        /// Indicates whether the location is equipped with lighting for night use.
        /// </summary>
        public bool HasLights { get; set; }
    }
}
