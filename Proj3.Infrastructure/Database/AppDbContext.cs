using Microsoft.EntityFrameworkCore;
using Proj3.Domain.Entities.Authentication;

namespace Proj3.Infrastructure.Database
{
    public class AppDbContext : DbContext
    {
        // Authentication
        public DbSet<User>? Users { get; set; }
        public DbSet<UserValidationCode>? UserValidationCodes { get; set; }
        public DbSet<RefreshToken>? RefreshTokens { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // FOR APPLICATION RUNNING USE AppDirectories.getDatabasePath
            //
            //optionsBuilder.UseSqlite(connectionString: String.Format(@"DataSource={0}; Cache=Shared", AppDirectories.getDatabasePath));

            // FOR MIGRATIONS AND DATABASE UPDATES USE ABSOLUTE PATH
            //
            optionsBuilder.UseSqlite(connectionString: String.Format(@"DataSource={0}; Cache=Shared", @"C:\Users\Aroldo Jales\Documents\Code\Vscode\IFPI\PROJ3\Proj3Api\Proj3.Infrastructure\Database\SQLite\Database.db"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Authentication
            modelBuilder.Entity<UserValidationCode>().Property(u => u.Id).ValueGeneratedOnAdd();         
            modelBuilder.Entity<RefreshToken>().Property(r => r.Id).ValueGeneratedOnAdd();           
        }
    }
}

        /* - COMMANDS
                - MIGRATIONS
                    dotnet ef migrations add InitialMigration --project .\Proj3.Infrastructure\ -o Database/SQLite/AppMigrations

                - UPDATE DATABASE (AFTER EACH MIGRATION)
                    dotnet ef database update --project .\Proj3.Infrastructure\
        */
        
        // Authentication