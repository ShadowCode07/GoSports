namespace GoSportsAPI.Helpers
{
    public class LocationQueryObject
    {
        public string? LobbyName { get; set; } = null;
        public string? LocationName { get; set; } = null;
        public string? LocationTypeName { get; set; } = null;
        public bool IsIndoor { get; set; } = false;
        public string? Surface { get; set; } = null;
        public bool HasLights { get; set; } = false;
        public string? SortBy { get; set; } = null;
        public bool IsDescending { get; set; } = false;

    }
}
