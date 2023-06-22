using Proj3.Domain.Entities.Authentication;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proj3.Domain.Entities.NGO
{
    [Table("Ngos")]
    public sealed class Ngo
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [ForeignKey("Users")]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(300)]
        public string Description { get; set; } = null!;        

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
        
        public Ngo(Guid userId, string name, string description, double latitude, double longitude)
        {    
            UserId = userId;
            Name = name;
            Description = description;
            Latitude = latitude;
            Longitude = longitude;
            CreatedAt = DateTime.UtcNow;
        }
    }
}