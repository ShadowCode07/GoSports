using System.ComponentModel.DataAnnotations;

namespace GoSportsAPI.Models
{
    public abstract class Base
    {
        [Key]
        public Guid Id { get; set; }

        [Timestamp]
        public byte[] Version { get; set; } = Array.Empty<byte>();
    }
}
