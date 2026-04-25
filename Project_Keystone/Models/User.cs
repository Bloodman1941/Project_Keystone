using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Keystone.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required, MaxLength(50)]
        public string UserFirstName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string UserLastName { get; set; } = string.Empty;

        [ForeignKey("UserRole")]
        public int UserRole { get; set; }

        [Required, MaxLength(100)]
        public string UserEmail { get; set; } = string.Empty;

        [Required, MaxLength(255)]
        public string UserPassword { get; set; } = string.Empty;

        [Required, MaxLength(20)]
        public string UserPhone { get; set; } = string.Empty;

        public byte[]? UserPFP { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Project> OwnedProjects { get; set; } = new List<Project>();
        public virtual ICollection<UserSkill> UserSkills { get; set; } = new List<UserSkill>();
    }
}
