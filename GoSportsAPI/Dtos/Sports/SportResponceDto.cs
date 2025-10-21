namespace GoSportsAPI.Dtos.Sports
{
    public class SportResponceDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
    }
}
