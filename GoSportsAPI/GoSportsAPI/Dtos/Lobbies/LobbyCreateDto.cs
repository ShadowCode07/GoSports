namespace GoSportsAPI.Dtos.Lobbies
{
    public class LobbyCreateDto
    {
        public string Name { get; set; } = string.Empty;

        public Guid LocationId { get; set; }
    }
}
