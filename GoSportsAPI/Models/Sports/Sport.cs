namespace GoSportsAPI.Models.Sports
{
    public class Sport
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
    }
}
