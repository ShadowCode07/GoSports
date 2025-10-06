namespace GoSportsAPI.Mdels.Location
{
    public class Location
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public LocationType LocationType { get; set; } = null!;

        private Location()
        {
            
        }

        public Location(string name, string description, LocationType type)
        {
            Name = name;
            Description = description;
            LocationType = type;
        }
    }
}
