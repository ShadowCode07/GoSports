namespace GoSportsAPI.Helpers
{
    public class LocationQueryObject
    {
        public string? LobbyName { get; set; } = null;
        public string? LocationName { get; set; } = null;
        public string? LocationTypeName { get; set; } = null;
        public string? SportName { get; set; } = null;
        public bool? IsIndoor { get; set; }
        public string? Surface { get; set; } = null;
        public bool? HasLights { get; set; }
        public string? SortBy { get; set; } = null;
        public bool IsDescending { get; set; } = false;

    }
}
