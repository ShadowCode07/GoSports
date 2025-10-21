namespace GoSportsAPI.Helpers
{
    public class SportQueryObject
    {
        public string? SportName { get; set; } = null;
        public string? SortBy { get; set; } = null;
        public bool IsDescending { get; set; } = false;
    }
}
