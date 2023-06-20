using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proj3.Domain.Entities.NGO
{
    [Table("EventVolunteers")]
    public sealed class EventVolunteer
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Events")]
        public Guid EventId { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Volunteers")]
        public Guid VolunteerId { get; set; }

        public EventVolunteer(Guid eventId, Guid volunteerId)
        {
            EventId = eventId;
            VolunteerId = volunteerId;
        }
    }
}
