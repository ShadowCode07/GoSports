namespace GoSportsAPI.Helpers
{
    public class LocationTypeQueryObject
    {
        public string? Name { get; set; } = null;
        public bool IsIndoor { get; set; } = false;
        public string? Surface { get; set; } = null;
        public bool HasLights { get; set; } = false;
        public string? SortBy { get; set; } = null;
        public bool IsDescending { get; set; } = false;
    }
}
