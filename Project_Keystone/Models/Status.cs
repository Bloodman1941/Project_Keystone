using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Keystone.Models
{
    public class Status
    {
        [Key]
        public int StatusID { get; set; }

        [Required, MaxLength(50)]
        public string StatusName { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        public string StatusDesc { get; set; } = string.Empty;

        [Required, MaxLength(20)]
        public string StatusColor { get; set; } = string.Empty;

        public virtual ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
    }
}
