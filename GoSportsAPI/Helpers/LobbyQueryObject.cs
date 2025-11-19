namespace GoSportsAPI.Helpers
{
    public class LobbyQueryObject
    {
        public string? LobbyName { get; set; } = null;
        public string? SportName { get; set; } = null;
        public string? LocationName { get; set; } = null;
        public string? SortBy { get; set; } = null;
        public bool IsDescending { get; set; } = false;
    }
}
