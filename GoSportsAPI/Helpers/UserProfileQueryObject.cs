namespace GoSportsAPI.Helpers
{
    public class UserProfileQueryObject
    {
        public string? UserName { get; set; } =null;
        public string? SortBy { get; set; } = null;
        public bool IsDescending { get; set; } = false;
    }
}
