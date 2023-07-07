using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proj3.Domain.Entities.Volunteer
{
    [Table("Reviews")]
    public sealed class Review
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Events")]
        public Guid EventId { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Volunteers")]
        public Guid VolunteerId { get; set; }

        public float Stars { get; set; }

        [MaxLength(300)]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public Review(Guid eventId, Guid volunteerId, float stars, string content)
        {
            EventId = eventId;
            VolunteerId = volunteerId;
            Content = content;
            CreatedAt = DateTime.Now;
        }
    }
}
