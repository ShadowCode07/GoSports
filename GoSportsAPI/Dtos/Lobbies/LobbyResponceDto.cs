namespace GoSportsAPI.Dtos.Lobbies
{
    public class LobbyResponceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid LocationId { get; set; }
        public Guid SportId { get; set; }
    }
}
