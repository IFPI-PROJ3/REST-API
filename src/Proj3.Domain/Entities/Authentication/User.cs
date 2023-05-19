using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proj3.Domain.Entities.Authentication
{
    [Table("Users")]
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public UserRole UserRole { get; set; }

        [MaxLength(300)]        
        public string UserName { get; set; } = null!;

        [MaxLength(100)]
        public string Email { get; set; } = null!;

        [Required]
        public string PasswordHash { get; set; } = null!;

        public byte[] Salt { get; set; } = null!;        

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [Required]
        public bool Active { get; set; } = false;

        public static User NewUserNgo(string name, string email)
        {
            User user = new();
            user.UserRole = UserRole.Ngo;
            user.UserName = name;
            user.Email = email;

            return user;
        }

        public static User NewUserVolunteer(string name, string email)
        {
            User user = new();
            user.UserRole = UserRole.Volunteer;
            user.UserName = name;
            user.Email = email;            

            return user;
        }
    }
}