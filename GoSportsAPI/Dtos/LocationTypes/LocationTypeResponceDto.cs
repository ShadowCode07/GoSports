namespace GoSportsAPI.Dtos.LocationTypes
{
    public class LocationTypeResponceDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid LocationId { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public bool IsIndoor { get; set; }
        public string Surface { get; set; } = string.Empty;
        public bool HasLights { get; set; }
    }
}
