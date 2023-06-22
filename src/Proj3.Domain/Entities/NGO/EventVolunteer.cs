using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proj3.Domain.Entities.NGO
{
    [Table("EventVolunteers")]
    public sealed class EventVolunteer
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("Events")]
        public Guid EventId { get; set; }

        [ForeignKey("Volunteers")]
        public Guid VolunteerId { get; set; }

        public bool? Accepted { get; set; } = null;

        public EventVolunteer(Guid eventId, Guid volunteerId)
        {
            EventId = eventId;
            VolunteerId = volunteerId;            
        }
    }
}
