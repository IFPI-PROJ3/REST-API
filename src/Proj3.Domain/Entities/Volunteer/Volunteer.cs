using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proj3.Domain.Entities.Volunteer
{
    [Table("Volunteers")]
    public sealed class Volunteer
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [ForeignKey("Users")]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(300)]
        public string Description { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public Volunteer(Guid userId, string name, string lastName, string description, double latitude, double longitude)
        {
            UserId = userId;
            Name = name;
            LastName = lastName;
            Description = description;
            Latitude = latitude;
            Longitude = longitude;
            CreatedAt = DateTime.UtcNow;
        }
    }
}