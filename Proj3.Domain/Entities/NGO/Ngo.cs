using Proj3.Domain.Entities.Authentication;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proj3.Domain.Entities.NGO
{
    [Table("Ngos")]
    public class Ngo
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public User User { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(300)]
        public string Description { get; set; } = null!;        

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }        
    }
}