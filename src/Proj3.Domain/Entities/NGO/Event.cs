using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proj3.Domain.Entities.NGO
{
    [Table("Events")]
    public sealed class Event
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [ForeignKey("Ngos")]
        public Guid NgoId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(300)]
        public string Decription { get; set; } = null!;

        [Required]
        public bool QuickEvent { get; set; }

        [Required]
        public int VolunteersLimit { get; set; }               

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public bool Cancelled { get; set; } = false;

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public static Event NewQuickEvent(Guid ngoId, string title, string decription, DateTime startDate, DateTime endDate)
        {
            Event event_ = new();
            event_.NgoId = ngoId;
            event_.Title = title;
            event_.Decription = decription;
            event_.QuickEvent = true;
            event_.VolunteersLimit = 0;            
            event_.StartDate = startDate;
            event_.EndDate = endDate;
            event_.CreatedAt = DateTime.UtcNow;

            return event_;
        }

        public static Event NewEvent(Guid ngoId, string title, string decription, int volunteersLimit, int volunteerCandidateLimit, DateTime startDate, DateTime endDate)
        {
            Event event_ = new();
            event_.NgoId = ngoId;
            event_.Title = title;
            event_.Decription = decription;
            event_.QuickEvent = false;
            event_.VolunteersLimit = volunteersLimit;            
            event_.StartDate = startDate;
            event_.EndDate = endDate;
            event_.CreatedAt = DateTime.UtcNow;

            return event_;
        }
    }
}
