using Microsoft.EntityFrameworkCore;
using Proj3.Domain.Entities.Authentication;
using Proj3.Domain.Entities.Common;
using Proj3.Domain.Entities.NGO;
using Proj3.Domain.Entities.Volunteer;

namespace Proj3.DataFixture
{
    public class WeChangeDbContext : DbContext
    {
        // Authentication
        public DbSet<User>? Users { get; set; }
        public DbSet<UserValidationCode>? UserValidationCodes { get; set; }
        public DbSet<RefreshToken>? RefreshTokens { get; set; }

        // Common
        //public DbSet<Address>? Addresses { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Comment>? Comments { get; set; }
        public DbSet<EventVolunteer>? EventVolunteers { get; set; }

        // NGO
        public DbSet<Event>? Events { get; set; }
        public DbSet<EventImage>? EventImages { get; set; }
        public DbSet<Ngo>? Ngos { get; set; }
        public DbSet<NgoCategory>? NgoCategories { get; set; }

        // Volunteer
        public DbSet<Volunteer>? Volunteers { get; set; }
        public DbSet<VolunteerCategory>? VolunteerCategories { get; set; }
    }
}
