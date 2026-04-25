using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Keystone.Models
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }

        [Required, MaxLength(50)]
        public string RoleName { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        public string RoleDesc { get; set; } = string.Empty;

        public bool RolePermCreate { get; set; }
        public bool RolePermEdit { get; set; }
        public bool RolePermDelete { get; set; }
        public bool RolePermInvite { get; set; }
        public bool RolePermEditRoles { get; set; }

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
