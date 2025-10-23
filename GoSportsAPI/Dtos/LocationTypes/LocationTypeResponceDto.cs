namespace GoSportsAPI.Dtos.LocationTypes
{
    /// <summary>
    /// Represents the data transfer object used to return location type information.
    /// </summary>
    public class LocationTypeResponceDto
    {
        /// <summary>
        /// The unique identifier of the location type.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// The identifier of the location associated with this location type.
        /// </summary>
        public Guid LocationId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// The name of the location type.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Indicates whether the location type is indoors.
        /// </summary>
        public bool IsIndoor { get; set; }

        /// <summary>
        /// The surface type of the location (e.g., grass, clay, hardwood).
        /// </summary>
        public string Surface { get; set; } = string.Empty;

        /// <summary>
        /// Indicates whether the location type has lighting available.
        /// </summary>
        public bool HasLights { get; set; }
    }

}
