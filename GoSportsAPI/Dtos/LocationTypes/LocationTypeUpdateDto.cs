namespace GoSportsAPI.Dtos.LocationTypes
{
    public class LocationTypeUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public bool IsIndoor { get; set; }
        public string Surface { get; set; } = string.Empty;
        public bool HasLights { get; set; }
    }
}
