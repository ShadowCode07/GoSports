using System.ComponentModel.DataAnnotations;

namespace GoSportsAPI.Models
{
    public abstract class Base
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Timestamp]
        public byte[] Version { get; set; }
    }
}
