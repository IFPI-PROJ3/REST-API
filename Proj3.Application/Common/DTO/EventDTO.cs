using Proj3.Domain.Entities.Common;
using Proj3.Domain.Entities.Volunteer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj3.Application.Common.DTO
{
    public class EventDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid NgoId { get; set; }
        public string Title { get; set; } = null!;
        public string Decription { get; set; } = null!;
        public bool QuickEvent { get; set; }
        public int VolunteersLimit { get; set; }
        public int VolunteerCandidateLimit { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Cancelled { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }


        public List<EventVolunteer> EventVolunteers { get; set; }

        public List<Volunteer> Comments { get; set; }
    }
}
