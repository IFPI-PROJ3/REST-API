using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proj3.Domain.Entities.Address
{
    [Table("Addresses")]
    public class Address
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [ForeignKey("Users")]
        public Guid UserId { get; set; }
        
        [ForeignKey("States")]
        public int StateId { get; set; }
        
        [ForeignKey("Cities")]
        public int CityId { get; set; }
        
        public Address()
        {            
        }
    }
}
