using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proj3.Domain.Entities.NGO
{
    [Table("EventImages")]
    public sealed class EventImage
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [ForeignKey("Events")]
        public Guid EventId { get; set; }

        public bool ImageThumb { get; set; }
        
        public static EventImage NewImage(Guid eventId)
        {
            return new EventImage { EventId = eventId, ImageThumb = false };
        }

        public static EventImage NewThumbImage(Guid eventId)
        {
            return new EventImage { EventId = eventId, ImageThumb = true };
        }
    }
}
