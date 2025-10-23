namespace GoSportsAPI.Dtos.Sports
{
    /// <summary>
    /// Represents the data transfer object used to return sport information.
    /// </summary>
    public class SportResponceDto
    {
        /// <summary>
        /// The unique identifier of the sport.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// The name of the sport.
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }

}
