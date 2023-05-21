using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proj3.Domain.Entities.Volunteer
{
    [Table("VolunteerCategories")]
    public sealed class VolunteerCategory
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Volunteers")]
        public Guid VolunteerId { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Categories")]
        public int CategoryId { get; set; }

        public DateTime CreatedAt { get; set; }

        public VolunteerCategory(Guid volunteerId, int categoryId)
        {
            VolunteerId = volunteerId;
            CategoryId = categoryId;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
