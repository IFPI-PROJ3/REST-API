using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Proj3.Domain.Entities.Authentication;
using Proj3.Domain.Entities.Common;
using Proj3.Domain.Entities.NGO;
using Proj3.Domain.Entities.Volunteer;

namespace Proj3.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        // Authentication
        public DbSet<User>? Users { get; set; }
        public DbSet<UserValidationCode>? UserValidationCodes { get; set; }
        public DbSet<RefreshToken>? RefreshTokens { get; set; }

        // Common                
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // SQL Server Azure
            //var dbPassword = "P@sswd12345";
            //var connectionString = $"Server=tcp:sqlserverforazure.database.windows.net,1433;Initial Catalog=Proj3.SQL_SERVER_DB;Persist Security Info=False;User ID=dbo4;Password={dbPassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            //optionsBuilder.UseSqlServer(connectionString);

            // SQL Server Local DB
            //string sqlConnectionString = "Data Source=(localdb)\\LocalDB;Initial Catalog=WeChangeDB;Integrated Security=True;";
            //optionsBuilder.UseSqlServer(sqlConnectionString);

            // PostgreSQL Render
            //SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
            //sqlConnectionStringBuilder.DataSource = "dpg-ci46prmnqqlbd9mi1pg0-a\\WeChange, 5432";
            //sqlConnectionStringBuilder.InitialCatalog = "wechange_db";            
            //sqlConnectionStringBuilder.UserID = "wechange_db_user";
            //sqlConnectionStringBuilder.Password = "ZmB0VPjyBV4FscngKxz0rN5o6QEhB65L";

            //var stringss = sqlConnectionStringBuilder.ConnectionString;
            //optionsBuilder.UseNpgsql("Data Source= dpg-ci46prmnqqlbd9mi1pg0-a\\WeChange, 5432 ;Initial Catalog=wechange_db;User ID=wechange_db_user;Password=ZmB0VPjyBV4FscngKxz0rN5o6QEhB65L");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Authentication
            modelBuilder.Entity<UserValidationCode>().Property(u => u.Id).ValueGeneratedOnAdd();         
            modelBuilder.Entity<RefreshToken>().Property(r => r.Id).ValueGeneratedOnAdd();

            // Composite keys
            modelBuilder.Entity<Comment>().HasKey(c => new { c.EventId, c.VolunteerId });
            modelBuilder.Entity<EventVolunteer>().HasKey(e => new { e.EventId, e.VolunteerId });
            modelBuilder.Entity<NgoCategory>().HasKey(n => new { n.NgoId, n.CategoryId });
            modelBuilder.Entity<VolunteerCategory>().HasKey(v => new { v.VolunteerId, v.CategoryId });
        }
    }
}

//0001_initial
//0002_business_address_fields
//0003_business_owner_and_person
//0004_person_phone_number
//0005_person_email_and_opt_out
//0006_business_description_and_services

/* - COMMANDS
        - MIGRATIONS
            dotnet ef migrations add 0001_initial --project .\Proj3.Infrastructure\ -o Persistence\Migrations

        - UPDATE DATABASE (AFTER EACH MIGRATION)
            dotnet ef database update --project .\Proj3.Infrastructure\
*/       